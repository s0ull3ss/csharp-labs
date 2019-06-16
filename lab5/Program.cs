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

            int k = 1;
            MatrixFilter mf = new MatrixFilter(k);
            Bitmap img = new Bitmap(imgpath);
            Bitmap img2 = new Bitmap(imgpath);

            DateTime start = DateTime.Now;
            mf.ApplyFilterToBitmap(ref img);
            TimeSpan elapsed = DateTime.Now - start;
            Console.WriteLine("Elapsed time (safe): {0}", elapsed);

            start = DateTime.Now;
            mf.ApplyFilterToBitmapUnsafe(ref img2);
            elapsed = DateTime.Now - start;
            Console.WriteLine("Elapsed time (unsafe): {0}", elapsed);

            img.Save("Safe.jpeg", ImageFormat.Jpeg);
            img2.Save("Unsafe.jpeg", ImageFormat.Jpeg);
            Console.ReadKey();
        }
    }
}
