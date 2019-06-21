using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            var imgpath = "";
            if (args.Length == 0) {
                Console.WriteLine("Path to img:");
                imgpath = Console.ReadLine();
            }
            else
            {
                imgpath = args[0];
                Console.WriteLine(args[0]);
            }

            int k = 5;
            MatrixFilter mf = new MatrixFilter(k);
            Bitmap img = new Bitmap(imgpath);
            Bitmap img2 = new Bitmap(imgpath);

            int[] thread_cnts = new int[] { 1, 2, 4, 8 };
            List<long> times = new List<long>();
            foreach (var cnt in thread_cnts)
            {
                img = new Bitmap(img2);
                var watch = System.Diagnostics.Stopwatch.StartNew();
                mf.Run(cnt, img);
                var elapsed = watch.ElapsedMilliseconds;
                times.Add(elapsed);
                Console.WriteLine("Elapsed time(ms): {0}; thread count {1}", elapsed, cnt);
                img.Save("thread" + cnt + ".jpeg", ImageFormat.Jpeg);
            }
            
            foreach(var el in times)
            {
                double tmp = Convert.ToDouble(times[0]) / Convert.ToDouble(el);
                Console.WriteLine("Ускорение: {0}", tmp);
            }
        
            Console.WriteLine("Completed. Press any key for exit");
            Console.ReadKey();
        }
    }
}
