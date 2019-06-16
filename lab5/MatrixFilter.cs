using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    class MatrixFilter
    {
        private int radius;
        private double[,] kernel;

        public MatrixFilter(int radius)
        {
            this.radius = radius;
            int k = this.radius * 2 + 1;
            kernel = new double[k, k];
            ComputateKernel(k);
        }
        private double Gaussian(double x, double y, double sigma)
        {
            return Math.Exp(-(Math.Pow(x, 2) + Math.Pow(y, 2)) / Math.Pow(sigma, 2) * 2 / Math.PI * Math.Pow(sigma, 2) * 2);

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
            for(int x = 0; x < img.Width; ++x)
            {
                for(int y = 0; y < img.Height; ++y)
                {
                    Color newPixel = ApplyFilterToPixel(x, y, img);
                    img.SetPixel(x, y, newPixel);
                }
            }
        }
    }
}
