using System;
using System.Text;
using System.IO;

namespace lab1
{
    enum StatusFiles
    {
        FirstEof,
        SecondEof,
        BothEof,
        NoneEof
    };

    class Program
    {
        private const int EOF = -1;
        private static long N { get; set; }

        public static long SkipSame(ref FileStream stream1, ref FileStream stream2, out int b1, out int b2, ref StatusFiles status)
        {
            do
            {
                b1 = stream1.ReadByte();
                b2 = stream2.ReadByte();
            } while (b1 == b2 && b1 != EOF && b2 != EOF);

            if(b1 == EOF && b2 == EOF)
            {
                status = StatusFiles.BothEof;
            }
            else if (b1 == EOF)
            {
                status = StatusFiles.FirstEof;
                return stream2.Position;
            }
            else if (b2 == EOF)
            {
                status = StatusFiles.SecondEof;
                return stream1.Position;
            }
            return stream1.Position;
        }

        public static bool CheckEqual(ref FileStream stream1, ref FileStream stream2, out int b1, out int b2, ref StatusFiles status)
        {
            b1 = stream1.ReadByte();
            b2 = stream2.ReadByte();

            if(b1 == EOF && b2 == EOF)
            {
                status = StatusFiles.BothEof;
            }
            else if (b1 == EOF)
            {
                status = StatusFiles.FirstEof;
            }
            else if (b2 == EOF)
            {
                status = StatusFiles.SecondEof;
            }
            return b1 == b2 ? true : false;
        }

        public static void PrintRest(ref FileStream stream, string pattern, int b, ref long count, ref StatusFiles status)
        {
            count++;
            while(b != EOF && count < N)
            {
                b = stream.ReadByte();
                if (b != EOF)
                {
                    Console.Write(pattern, b);
                }
                count++;
            }
            status = StatusFiles.BothEof;
        }

        static void Main(string[] args)
        {
            if(args.Length < 2)
            {
                Console.WriteLine("Program requires at least 2 args");
                return;
            }

            Console.WriteLine(args[0]);
            Console.WriteLine(args[1]);
            FileStream stream1 = new FileStream(args[0], FileMode.Open);
            FileStream stream2 = new FileStream(args[1], FileMode.Open);

            if(args.Length > 2)
            {
                N = Convert.ToInt64(args[2]);
            }
            else
            {
                N = stream1.Length;
                if(stream2.Length > N)
                {
                    N = stream2.Length;
                }
            }

            StatusFiles status = StatusFiles.NoneEof;
            long position = 0, count = 0;
            int b1 = 0, b2 = 0;

            while(status != StatusFiles.BothEof && count < N)
            {
                bool offset = false;
                position = SkipSame(ref stream1, ref stream2, out b1, out b2, ref status);

                if(status == StatusFiles.NoneEof)
                {
                    offset = true;
                    Console.Write("0x{0:X8}: ", position - 1);
                    
                    do
                    {
                        Console.Write("0x{0:X2}(0x{1:X2}) ", b1, b2);
                        position++;
                        count++;
                    } while (!CheckEqual(ref stream1, ref stream2, out b1, out b2, ref status) && status == StatusFiles.NoneEof);
                }

                if (!offset && status != StatusFiles.BothEof)
                {
                    Console.Write("0x{0:X8}: ", position - 1);
                }

                if(status == StatusFiles.FirstEof)
                {
                    Console.Write("<EOF>(0x{0:X2}) ", b2);
                    PrintRest(ref stream2, "(0x{0:X2}) ", b2, ref count, ref status);
                }

                if(status == StatusFiles.SecondEof)
                {
                    Console.Write("0x{0:X2}(<EOF>) ", b1);
                    PrintRest(ref stream1, "0x{0:X2} ", b1, ref count, ref status);
                }

                Console.WriteLine();
            }

            if(count == 0)
            {
                Console.WriteLine("Files Identical");
            }
            Console.ReadKey();
        }
    }
}
