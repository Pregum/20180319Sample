using System;
using System.Text;
using System.Threading;
using System.Timers;
using OpenCvSharp;
using ZXing;
using static OpenCvSharp.OpenCvSharpException;

namespace _20180319Sample
{
    public class BarcodeCapture
    {
        private static System.Timers.Timer _timer = new System.Timers.Timer() {Interval = 1000};
        private static Mat _mat = new Mat();
        private static string savePath = "capImage.png";

        public static void Capture()
        {
            //_timer.Elapsed += TimerOnElapsed;
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
                    }
                }
            }

            Cv2.DestroyWindow("capture");
        }

        private static void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
        }
    }
}