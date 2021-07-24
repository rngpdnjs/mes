using System;
using System.IO;
using System.Collections.Generic;
using System.Timers;

namespace SmartMES
{
    class Program
    {
        
        static Factory factory = new Factory();

        static long lastMaxOffset;

        static Timer timer = new System.Timers.Timer();

        static void Main(string[] args)
        {
            Console.WriteLine("SmartMES V0.3");
            String[] inputMSG;

            do
            {
                Console.Write("MES:>");
                inputMSG = Console.ReadLine().ToUpper().Split(' ');

                try
                {
                    // Machine 등록
                    if (inputMSG[0] == "NEW")
                    {
                        foreach (Machine machine in factory.MachineList)
                        {
                            if (machine.Name == inputMSG[1])
                            {
                                throw new Exception("Error : 이미 등록된 설비입니다 (" + inputMSG[1] + ")");
                            }
                        }

                        Machine newMachine = new Machine();
                        newMachine.Name = inputMSG[1];
                        newMachine.State = "DOWN";
                        newMachine.EventName = "New Machine";
                        newMachine.EventTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        newMachine.machineEventHandler += OnMachineEvent;

                        factory.MachineList.Add(newMachine);
                    }
                    // Machine 상태 변경
                    else if (inputMSG[0] == "CHANGE")
                    {
                        foreach (Machine machine in factory.MachineList)
                        {
                            if (machine.Name == inputMSG[1])
                            {
                                machine.changeState(inputMSG[2]);
                                break;
                            }
                        }
                    }
                    // Machine 정보 View
                    else if (inputMSG[0] == "INFO")
                    {
                        foreach (Machine machine in factory.MachineList)
                        {
                            if (machine.Name == inputMSG[1])
                            {
                                machine.viewInfo();
                                break;
                            }
                        }
                    }
                    // Machine List 저장
                    else if (inputMSG[0] == "SAVE")
                    {
                        String sFileName = $"..\\{inputMSG[1]}.xml";

                        if (sFileName.Length > 0)
                        {
                            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

                            if (inputMSG[1] == "FACTORY")
                                doc.Load(new System.IO.StringReader(Formatter.XmlSerialize(factory)));
                            else if(inputMSG[1] == "MACHINELIST")
                                doc.Load(new System.IO.StringReader(Formatter.XmlSerialize(factory.MachineList)));

                            doc.Save(sFileName);
                        }
                    }
                    // Machine List 불러오기
                    else if (inputMSG[0] == "LOAD")
                    {
                        String sFileName = $"..\\{inputMSG[1]}.xml";

                        if (sFileName.Length > 0)
                        {
                            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                            doc.Load(sFileName);

                            if (inputMSG[1] == "FACTORY")
                                factory = Formatter.XmlDeserialize<Factory>(doc.OuterXml);
                            else if(inputMSG[1] == "MACHINELIST")
                                factory.MachineList = Formatter.XmlDeserialize<List<Machine>>(doc.OuterXml);

                            //Event 등록
                            foreach (Machine machine in factory.MachineList)
                            {
                                machine.machineEventHandler += OnMachineEvent;
                            }
                        }
                    }
                    // 파일감시
                    else if (inputMSG[0] == "WATCHING")
                    {
                        Machine machine = factory.MachineList.Find(x=> x.Name == inputMSG[1]);

                        if (!string.IsNullOrEmpty(machine.FilePath))
                        {
                            FileInfo fileInfo = new FileInfo(machine.FilePath);
                            if (fileInfo.Exists)
                            {
                                FileCheck(machine.FilePath);
                            }
                            else
                            {
                                throw new Exception("Error : 파일이 존재하지 않습니다. (" + machine.FilePath + ")");
                            }
                        }
                    }

                    else if (inputMSG[0] == "TIMER")
                    {
                        if (inputMSG[1] == "START")
                        {
                            // 타이머 생성 및 시작
                            //System.Timers.Timer timer = new System.Timers.Timer();
                            timer.Interval = 1000 * int.Parse(inputMSG[2]); // 1 시간
                            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                            timer.Start();
                        }
                        else if (inputMSG[1] == "END")
                        {
                            timer.Stop();
                        }
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

            } while (inputMSG[0] != "EXIT");
        }

        private static void FileCheck(string sFileFullPath)
        {
            do
            {
                using (StreamReader reader = new StreamReader(new FileStream(sFileFullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    try
                    {
                        //seek to the last max offset
                        reader.BaseStream.Seek(lastMaxOffset, SeekOrigin.Begin);

                        //read out of the file until the EOF
                        string line = "";
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                        }

                        //update the last max offset
                        lastMaxOffset = reader.BaseStream.Position;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        break;
                    }
                }
            } while (false);
        }

        static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            //FileCheck(factory.MachineList.Find(x=> x.Name == "ETH").FilePath);

            foreach (var machine in factory.MachineList)
	        {
                if (string.IsNullOrEmpty(machine.FilePath) == false)
                {
                    LogFileCheck(machine);
                }
	        }

        }

        static void LogFileCheck(Machine machine)
        {
            using (StreamReader reader = new StreamReader(new FileStream(machine.FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                try
                {

                    String sText = "";
                    //if (machine.getLastMaxOffset() > 0 )
                    //{
                        //seek to the last max offset
                        reader.BaseStream.Seek(machine.getLastMaxOffset(), SeekOrigin.Begin);
                        //read out of the file until the EOF
                        string line = "";
                        while ((line=reader.ReadLine()) != null)
                        {
                            if (line.Contains("<?xml") == true)
                            {
                                sText = line.Substring(line.IndexOf("<?xml"));
                            }
                            else
                                sText += line;
                        }

                        BithumbResponse bithumb = Formatter.XmlDeserialize< BithumbResponse>(sText);
                       //machine.LastPress = bithumb.data.closing_price;
                       machine.setLastPress(bithumb.data.closing_price);

                       machine.viewInfo();
                    //}

                    //update the last max offset
                    machine.setLastMaxOffset(reader.BaseStream.Position);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    //break;
                }
            }
        }

        static void OnMachineEvent(Object sender, MachineEventArgs e)
        {
            Console.WriteLine(string.Format("{0} [{1}] {2} {3}", e.Time, e.Name, e.Desc, e.User));
        }
    }
}
