using Newtonsoft.Json.Linq;

using System;
using System.IO;
using System.Net.Http;

namespace Utakata
{
    class Weathertype
    {
        // 石川県加賀のID
        // string city = "170010";
        // string url = $"{baseUrl}?city={city}";

        public string baseUrl;
        public string city;
        public string url;
        public string json;
        public string reiwa_kanji;
        public string OneYear;
        public string OneMonth;
        public string Onedays;
        public string OneHour;
        public string OneMinutes;
        public string Oneseconds;
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Weathertype t = new();
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("");
                Console.Write("IDの詳細はこちら → ");
                Console.WriteLine("https://github.com/takkii/Utakata/wiki/Utakata-wiki");
                Console.WriteLine("");
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("");
                Console.WriteLine("日本全国のIDから自分の地域にあったIDを入力！");
                Console.WriteLine("");
                Console.Write("> ");

                // 天気予報の取得 (JSON形式)
                t.baseUrl = "https://weather.tsukumijima.net/api/forecast";
                t.city = Console.ReadLine();
                t.url = $"{t.baseUrl}?city={t.city}";
                t.json = new HttpClient().GetStringAsync(t.url).Result;
                JObject jobj = JObject.Parse(t.json);

                Console.WriteLine("--------------------------天気予報--------------------------");
                Console.WriteLine("");
                // 今日の天気
                string today_weather = (string)((jobj["forecasts"][0]["telop"] as JValue).Value);
                Console.WriteLine("今日の天気は、" + today_weather + "でしょう");
                // 明日の天気
                string tomorrow_weather = (string)((jobj["forecasts"][1]["telop"] as JValue).Value);
                Console.WriteLine("明日の天気は、" + tomorrow_weather + "でしょう");
                // 最低気温
                string min_tem = (string)((jobj["forecasts"][1]["temperature"]["min"]["celsius"] as JValue).Value);
                Console.WriteLine("最低気温は、" + min_tem + "℃でしょう");
                // 最高気温
                string max_tem = (string)((jobj["forecasts"][1]["temperature"]["max"]["celsius"] as JValue).Value);
                Console.WriteLine("最高気温は、" + max_tem + "℃でしょう");
                Console.WriteLine("");
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("");
                Console.WriteLine("-------------------------天気概況文-------------------------");
                Console.WriteLine("");
                // 気象台
                string des = (string)((jobj["description"]["text"] as JValue).Value);
                Console.WriteLine(des);
                Console.WriteLine("");
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("");
                // 発表時刻
                DateTime des_time = (DateTime)((jobj["description"]["publicTime"] as JValue).Value);
                DateTime dt = des_time;

                t.reiwa_kanji = "令和"; t.OneYear = "年"; t.OneMonth = "月"; t.Onedays = "日";
                t.OneHour = "時"; t.OneMinutes = "分"; t.Oneseconds = "秒";
                
                string reiwa = (t.reiwa_kanji + (dt.Year - 2018) + t.OneYear + dt.Month + t.OneMonth + dt.Day + t.Onedays 
                    + dt.Hour + t.OneHour + dt.Minute + t.OneMinutes + dt.Second + t.Oneseconds);

                Console.WriteLine("天気概況文の発表時刻 : " + reiwa);
                Console.WriteLine("");
                Console.WriteLine("------------------------------------------------------------");
            
            } catch (IOException e) {
                Console.WriteLine($"IOException Handler: {e.Message}");
            } catch (Exception e) {
                Console.WriteLine($"Generic Exception Handler: {e.Message}");
            } finally {
                GC.Collect();
            }
        }
    }
}
