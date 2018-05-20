using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows;
using Newtonsoft.Json.Linq;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using ZXing;
using ZXing.Common;
using static OpenCvSharp.OpenCvSharpException;
using Rect = OpenCvSharp.Rect;
using Window = OpenCvSharp.Window;

namespace _20180319Sample
{
    public static class BarcodeCapture
    {
        private static readonly System.Timers.Timer Timer = new System.Timers.Timer() {Interval = 500};

        private static Mat _mat = new Mat();
        //private static string savePath = "capImage.png";

        public static string FoodName = "no item.";
        public static bool DoneDecode = false;

        public static void Capture()
        {
            FoodName = "no item.";
            DoneDecode = false;

            Timer.Elapsed += TimerOnElapsed;
            Timer.Start();
            using (var capture = new VideoCapture(0) {AutoFocus = true})
            {
                using (var window = new Window("capture"))
                {
                    while (true)
                    {
                        capture.Read(_mat);
                        window.ShowImage(_mat);

                        // ESC key is break.
                        if (Cv2.WaitKey(30) == 27)
                        {
                            break;
                        }

                        // decode done.
                        if (DoneDecode)
                        {
                            break;
                        }
                    }
                }
            }

            Timer.Stop();

            Cv2.DestroyWindow("capture");
        }

        private static void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            Timer.Stop();
            if (_mat == null)
            {
                Timer.Start();
                return;
            }

            var reader = new BarcodeReader()
            {
                AutoRotate = true,
                Options = new ZXing.Common.DecodingOptions()
                {
                    TryHarder = true,
                    PossibleFormats = new List<BarcodeFormat>() { BarcodeFormat.EAN_13},
                },
            };

            using (var tmp = new Mat(_mat, new Rect(0, 0, _mat.Width, _mat.Height)))
            {
                // 参考URL : http://www.moonmile.net/blog/archives/6258
                System.Drawing.Bitmap bitmap = _mat.ToBitmap();
                var result = reader.Decode(bitmap);
                if (result != null)
                {
                    var foodJson = ToFoodName(long.Parse(result.Text));
                    JObject jobj = JObject.Parse(foodJson);
                    var jarr = jobj.First.Last.Last.First.First.First["0"]["Name"];
                    //MessageBox.Show(jarr?.ToString() ?? "No item.");
                    // FoodAddWindow open 
                    if (jarr != null)
                    {
                        DoneDecode = true;
                        FoodName = jarr.ToString();
                    }
                    else
                    {
                        DoneDecode = false;
                    }
                }
            }

            Timer.Start();
        }

        private static string ToFoodName(long janCode)
        {
            // ..\..\PersonalKey\YahooAPIKey.txt in your yahoo api key
            // reference URL : http://tsujimotter.info/2012/12/14/yahoo-jancode/
            var apiKey = File.ReadLines(@"..\..\PersonalKey\YahooAPIKey.txt").First().Trim();
            string baseYahooUri = "http://shopping.yahooapis.jp/ShoppingWebService/V1/json/itemSearch";
            Uri uri = new Uri(baseYahooUri + "?appid=" + apiKey + "&jan=" + janCode);

            var client = new HttpClient();
            var response = client.GetStringAsync(uri).Result;
            return response;
        }
    }
}