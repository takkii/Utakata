using System;
using System.IO;
using System.Net.Http;

using Newtonsoft.Json.Linq;

namespace Utakata
{
    class Program
    {
        static void Main()
        {
            try
            {
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

                // API取得先
                string baseUrl = "https://weather.tsukumijima.net/api/forecast";
                // IDの入力待ち
                var input_str = Console.ReadLine();

                // 石川県加賀のID
                // string cityname = "170010";

                string url = $"{baseUrl}?city={input_str}";
                string json = new HttpClient().GetStringAsync(url).Result;
                JObject jobj = JObject.Parse(json);

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
                const string reiwa_kanji = "令和";
                const string OneYear = "年";
                const string OneMonth = "月";
                const string Onedays = "日";
                const string OneHour = "時";
                const string OneMinutes = "分";
                const string Oneseconds = "秒";
                string reiwa = (reiwa_kanji + (dt.Year - 2018) + OneYear + dt.Month + OneMonth + dt.Day + Onedays + dt.Hour + OneHour + dt.Minute + OneMinutes + dt.Second + Oneseconds);

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
