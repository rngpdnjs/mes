using System;

namespace SmartMES
{
    public class Machine
    {
        public String Name;
        public String State;
        public String EventName;
        public String EventTime;
        public String FilePath;
        private long lastMaxOffset;
        public String LastPress;

        public void viewInfo()
        {
            Console.WriteLine(" Machine Name : " + this.Name);
            Console.WriteLine(" Machine State : " + this.State);
            Console.WriteLine(" Machine EventName : " + this.EventName);
            Console.WriteLine(" Machine EventTime : " + this.EventTime);
            //Console.WriteLine(" Machine FilePath : " + this.FilePath);
            Console.WriteLine(" Machine LastPress : " + this.LastPress);
        }

        public void changeState(String newState)
        {
            if (this.State == "IDLE")
            {
                this.State = newState;
            }
            else if (newState == "IDLE")
            {
                this.State = newState;
            }
            else if(this.State == newState)
            { }
            else
            {
                throw new Exception("Error : Machine ChangeState(" + this.State + " -> " + newState + ")");
            }

            this.EventName = "Machine ChangeState";
            this.EventTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


        }

        public void setLastMaxOffset(long value)
        {
            this.lastMaxOffset = value;
        }

        public long getLastMaxOffset()
        {
            return this.lastMaxOffset;
        }

        public  void setLastPress(String value)
        {
            if (this.LastPress == null)
                this.LastPress = value;
            else if (double.Parse(this.LastPress) < double.Parse(value))
            {
                this.LastPress = value;

                MachineEventArgs machineEventArgs = new MachineEventArgs();
                machineEventArgs.Name = this.Name;
                machineEventArgs.Desc = "올랐어요 !!!!! ";
                OnMachineEvent(machineEventArgs);
            }

            this.LastPress = value;
        }

        public event EventHandler<MachineEventArgs> machineEventHandler;

        protected virtual void OnMachineEvent(MachineEventArgs e)
        {
            if (machineEventHandler != null) machineEventHandler(this, e);
        }
    }
}
