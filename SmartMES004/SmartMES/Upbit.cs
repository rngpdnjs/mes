using System;
using System.Collections.Generic;
using System.Text;

/*
[
    {   "market":"KRW-ETH",
        "trade_date":"20210316",
        "trade_time":"143950",
        "trade_date_kst":"20210316",
        "trade_time_kst":"233950",
        "trade_timestamp":1615905590000,
        "opening_price":2076000.00000000,
        "high_price":2116000.0,
        "low_price":1983000.0,
        "trade_price":2075000.0,
        "prev_closing_price":2076000.00000000,
        "change":"FALL",
        "change_price":1000.00000000,
        "change_rate":0.0004816956,
        "signed_change_price":-1000.00000000,
        "signed_change_rate":-0.0004816956,
        "trade_volume":0.6261,
        "acc_trade_price":170738123356.339815000,
        "acc_trade_price_24h":216275200168.526785,
        "acc_trade_volume":83003.84140914,
        "acc_trade_volume_24h":105029.21651008,
        "highest_52_week_price":2353000.00000000,
        "highest_52_week_date":"2021-02-20",
        "lowest_52_week_price":136400.00000000,
        "lowest_52_week_date":"2020-03-16",
        "timestamp":1615905590661
    }
]
 */
namespace SmartMES
{
    public class UpbitResponse
    {
        public string market { get; set; } // 종목 구분 코드

        public string trade_date { get; set; } // 최근 거래 일자(UTC)

        public string trade_time { get; set; } // 최근 거래 시각(UTC)

        public string trade_date_kst { get; set; } // 최근 거래 일자(KST)

        public string trade_time_kst { get; set; } // 최근 거래 시각(KST)
        public double trade_timestamp { get; set; }
        
        public double opening_price { get; set; } // 시가

        public double high_price { get; set; } // 고가

        public double low_price { get; set; } // 저가

        public double trade_price { get; set; } // 종가

        public double prev_closing_price { get; set; } // 전일 종가

        public string change { get; set; } // EVEN : 보합, RISE : 상승, FALL : 하락

        public double change_price { get; set; } // 변화액의 절대값

        public double change_rate { get; set; } // 변화율의 절대값

        public double signed_change_price { get; set; } // 부호가 있는 변화액

        public double signed_change_rate { get; set; } // 부호가 있는 변화율

        public double trade_volume { get; set; } // 가장 최근 거래량

        public double acc_trade_price { get; set; } // 누적 거래대금(UTC 0시 기준)

        public double acc_trade_price_24h { get; set; } // 24시간 누적 거래대금

        public double acc_trade_volume { get; set; } // 누적 거래량(UTC 0시 기준)

        public double acc_trade_volume_24h { get; set; } // 24시간 누적 거래대금

        public double highest_52_week_price { get; set; } // 52주 신고가

        public string highest_52_week_date { get; set; } // 52주 신고가 달성일

        public double lowest_52_week_price { get; set; } // 52주 신저가

        public string lowest_52_week_date { get; set; } // 52주 신저가 달성일

        public long timestamp { get; set; } // 타임스탬프
        
    }
}
