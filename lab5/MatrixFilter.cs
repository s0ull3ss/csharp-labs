using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab5
{
    class MatrixFilter
    {
        private int radius;
        private double[,] kernel;
        private int _k;

        public MatrixFilter(int radius)
        {
            this.radius = radius;
            int k = this.radius * 2 + 1;
            this._k = k;
            kernel = new double[k, k];
            ComputateKernel(k);
        }
        private double Gaussian(double x, double y, double sigma)
        {
            return Math.Exp(-(Math.Pow(x, 2) + Math.Pow(y, 2)) / (Math.Pow(sigma, 2) * 2)) / (Math.PI * Math.Pow(sigma, 2) * 2);
        }
        private void ComputateKernel(int k)
        {
            double sigma = 1;
            double sum = 0.0;
            for(int x = 0; x < k; ++x)
            {
                for(int y = 0; y < k; ++y)
                {
                    kernel[x, y] = Gaussian(x-radius, y-radius, sigma);
                    sum += kernel[x, y];
                }
            }

            for(int x = 0; x < k; ++x)
            {
                for(int y = 0; y < k; ++y)
                {
                    kernel[x, y] /= sum;
                }
            }
        }

        private Color ApplyFilterToPixel(int x, int y, Bitmap img)
        {
            int width = img.Width;
            int height = img.Height;
            int R = 0, G = 0, B = 0;
            int n = 0, m = 0;
            for(int i = y - radius; i <= y + radius; ++i)
            {
                if (i < 0)
                {
                    m = 0;
                }
                else if (i >= height)
                {
                    m = height - 1;
                }
                else
                {
                    m = i;
                }

                for(int j = x - radius; j <= x + radius; ++j)
                {
                    if (j < 0)
                    {
                        n = 0;
                    }
                    else if (j >= width)
                    {
                        n = width - 1;
                    }
                    else
                    {
                        n = j;
                    }

                    Color pixel = img.GetPixel(n, m);
                    R += (byte) (pixel.R * kernel[i - y + radius, j - x + radius]);
                    G += (byte) (pixel.G * kernel[i - y + radius, j - x + radius]);
                    B += (byte) (pixel.B * kernel[i - y + radius, j - x + radius]);

                }
            }
            return Color.FromArgb(R, G, B);
        }

        public void ApplyFilterToBitmap(ref Bitmap img)
        {
            for(int y = 0; y < img.Height; ++y)
            {
                for(int x = 0; x < img.Width; ++x)
                {
                    Color newPixel = ApplyFilterToPixel(x, y, img);
                    img.SetPixel(x, y, newPixel);
                }
            }
        }


        private unsafe void ApplyFilterToPixelUnsafe(int x, int y, int bytesPerPixel, int heightInPixels, int widthInBytes, byte* firstPxlAdr, byte* curLine, int stride)
        {
            int width = widthInBytes;
            int height = heightInPixels;
            byte R = 0, G = 0, B = 0;
            int n = 0, m = 0;

            for(int i = y - radius; i <= y + radius; ++i)
            {
                if (i < 0)
                {
                    m = 0;
                }
                else if (i >= height)
                {
                    m = height - 1;
                }
                else
                {
                    m = i;
                }

                for (int j = x - radius*bytesPerPixel; j <= x + radius*bytesPerPixel; j += bytesPerPixel)
                {
                    if (j < 0)
                    {
                        n = 0;
                    }
                    else if (j >= width)
                    {
                        n = width - bytesPerPixel;
                    }
                    else
                    {
                        n = j;
                    }

                    Color pixel = Color.FromArgb(
                        firstPxlAdr[n + m * stride + 2],
                        firstPxlAdr[n + m * stride + 1],
                        firstPxlAdr[n + m * stride]);
                    R += (byte)(pixel.R * kernel[i - y + radius, (j - x) / bytesPerPixel + radius]);
                    G += (byte)(pixel.G * kernel[i - y + radius, (j - x) / bytesPerPixel + radius]);
                    B += (byte)(pixel.B * kernel[i - y + radius, (j - x) / bytesPerPixel + radius]);
                }
            }
            curLine[x] = B;
            curLine[x + 1] = G;
            curLine[x + 2] = R;
        }

        private unsafe void ApplyFilterToPixelUnsafe(int x, int y, int bytesPerPixel, int heightInPixels, int widthInBytes, byte* firstPxlAdr, byte* curLine, int stride, int maxHeight)
        {
            int width = widthInBytes;
            int height = heightInPixels;
            byte R = 0, G = 0, B = 0;
            int n = 0, m = 0;

            for(int i = 0; i < _k; ++i)
            {
                int res = y - radius + i;
                if (res < 0)
                {
                    m = 0;
                }
                else if (res >= maxHeight)
                {
                    m = height - 1;
                }
                else
                {
                    m = res;
                }

                for (int j = x - radius*bytesPerPixel; j <= x + radius*bytesPerPixel; j += bytesPerPixel)
                {
                    if (j < 0)
                    {
                        n = 0;
                    }
                    else if (j >= width)
                    {
                        n = width - bytesPerPixel;
                    }
                    else
                    {
                        n = j;
                    }

                    Color pixel = Color.FromArgb(
                        firstPxlAdr[n + 2],
                        firstPxlAdr[n + 1],
                        firstPxlAdr[n]);

                    R += (byte)(pixel.R * kernel[i, (j - x) / bytesPerPixel + radius]);
                    G += (byte)(pixel.G * kernel[i, (j - x) / bytesPerPixel + radius]);
                    B += (byte)(pixel.B * kernel[i, (j - x) / bytesPerPixel + radius]);
                }
            }
            curLine[x] = B;
            curLine[x + 1] = G;
            curLine[x + 2] = R;
        }
        
        public void ApplyFilterToBitmapUnsafe(ref Bitmap processedBitmap)
        {
            unsafe
            {
                BitmapData bitmapData = processedBitmap.LockBits(
                    new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height),
                    ImageLockMode.ReadWrite, processedBitmap.PixelFormat);
                int bytesPerPixel = Image.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
                int heightInPixel = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)bitmapData.Scan0;

                for(int y = 0; y < heightInPixel; ++y)
                {
                    byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                    for(int x = 0; x < widthInBytes; x += bytesPerPixel)
                    {
                        ApplyFilterToPixelUnsafe(x, y, bytesPerPixel, heightInPixel, widthInBytes, ptrFirstPixel, currentLine, bitmapData.Stride);
                    }
                }
                processedBitmap.UnlockBits(bitmapData);
            }
        }

        public void Run(int threads, Bitmap processedBitmap)
        {
            unsafe
            {
                int width = processedBitmap.Height;
                int size = (width + threads - 1) / threads;
                List<Task> tasks = new List<Task>();

                BitmapData bitmapData = LockBitmapBits(ref processedBitmap);
                int heightInPixel = processedBitmap.Height;

                for(int i = 0; i < width; i += size)
                {
                    var start = i;
                    var finish = i + size;
                    if (finish >= width)
                    {
                        finish = width;
                    }
                    Task task = new Task(() => Work(start, finish, ref processedBitmap, ref bitmapData, heightInPixel));
                    task.Start();
                    tasks.Add(task);
                }

                foreach(var worker in tasks)
                {
                    worker.Wait();
                }

                UnlockBitmapBits(ref processedBitmap, ref bitmapData);
            }
        }

        private BitmapData LockBitmapBits(ref Bitmap processedBitmap)
        {
            Rectangle rect = new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height);
            BitmapData bitmapData = processedBitmap.LockBits(
                rect, ImageLockMode.ReadWrite, processedBitmap.PixelFormat);
            return bitmapData;

        }

        private void UnlockBitmapBits(ref Bitmap processedBitmap, ref BitmapData bitmapData)
        {
            processedBitmap.UnlockBits(bitmapData);
        }

        private object locker = new object();
        unsafe public void Work(int start, int finish, ref Bitmap processedBitmap, ref BitmapData bitmapData, int heightInPixel)
        {
            //Thread currentThread = Thread.CurrentThread;
            //Console.WriteLine("Thread: {0}. width: {1}. height: {2}. start: {3}. finish: {4}",currentThread.ManagedThreadId,processedBitmap.Width, processedBitmap.Height, start, finish);
            int bytesPerPixel = Image.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
            int widthInBytes = bitmapData.Width * bytesPerPixel;
            byte* ptrFirstPixel = (byte*)bitmapData.Scan0;

            for(int y = start; y < finish; ++y)
            {
                byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                {
                    //ApplyFilterToPixelUnsafe(x, y, bytesPerPixel, heightInPixel, widthInBytes, ptrFirstPixel, currentLine, bitmapData.Stride);
                    ApplyFilterToPixelUnsafe(x, y, bytesPerPixel, heightInPixel, widthInBytes, currentLine, currentLine, bitmapData.Stride, heightInPixel);
                }
            }

        }
    }
}
