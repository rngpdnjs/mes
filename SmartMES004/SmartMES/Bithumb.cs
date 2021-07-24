using System;
using System.Collections.Generic;
using System.Text;

/*
 {"status":"0000",
   "data":{ 
            "opening_price":"2090000",
            "closing_price":"2080000",
            "min_price":"2078000",
            "max_price":"2092000",
            "units_traded":"766.74269334",
            "acc_trade_value":"1600057879.2999",
            "prev_closing_price":"2089000",
            "units_traded_24H":"191321.19638915",
            "acc_trade_value_24H":"399094157901.0137",
            "fluctate_24H":"86000",
            "fluctate_rate_24H":"4.31",
            "date":"1615302669561"
          }
 }
 */
namespace SmartMES
{
    public class BithumbResponse
    {
        public string status { get; set; }
        public BithumbData data { get; set; }
    }

    public class BithumbData
    {
        public string opening_price { get; set; }      //시가 00시 기준(오늘 시가)
        public string closing_price { get; set; }      //종가 00시 기준(오늘 종가)
        public string min_price { get; set; }          //저가 00시 기준(오늘 저가)
        public string max_price { get; set; }          //고가 00시 기준(오늘 고가)
        public string units_traded { get; set; }       //거래량 00시 기준(오늘 거래량)
        public string acc_trade_value { get; set; }     //거래금액 00시 기준(오늘 거래금액)
        public string prev_closing_price { get; set; }  //전일 종가
        public string units_traded_24H { get; set; }    //최근 24시간 거래량
        public string acc_trade_value_24H { get; set; } //최근 24시간 거래금액
        public string fluctate_24H { get; set; }        //최근 24시간 변동가
        public string fluctate_rate_24H { get; set; }   //최근 24시간 변동률
        public string date { get; set; }                //타임 스탬프
    }

  
}
