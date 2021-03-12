using System;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;

namespace RemoteController
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.OpenStandardInput();

            var buffer = new byte[1024];
            int length;

            while (input.CanRead && (length = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                
                var payload = new byte[length];

                Buffer.BlockCopy(buffer, 0, payload, 0, length);

                string data = Encoding.UTF8.GetString(payload);

                Console.WriteLine($"received: {data}");

                Draw(data);

                Console.Out.Flush();
            }
        }

        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Point lpPoint);

        static void Draw(string data)
        {
            Point p = new Point();
            GetCursorPos(ref p);


            switch (data)
            {
                case "add1x":
                SetCursorPos(p.X + 100, p.Y);
                break;
                case "add1y":
                SetCursorPos(p.X, p.Y + 1);
                break;
                case "sub1x":
                SetCursorPos(p.X - 1, p.Y);                
                break;
                case "sub1y":
                SetCursorPos(p.X, p.Y - 1);
                break;
                default:
                Console.WriteLine("Something failed");
                break;
            }
        }
    }
}