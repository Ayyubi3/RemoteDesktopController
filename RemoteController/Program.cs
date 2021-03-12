using System;
using System.Text;

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

        static void Draw(string data)
        {

            const int SENSITIVITY = 25;

            MouseOperations.MousePoint p = MouseOperations.GetCursorPosition();


            switch (data)
            {
                case "add1x":
                    MouseOperations.SetCursorPosition(p.X + SENSITIVITY, p.Y);
                    break;
                case "add1y":
                    MouseOperations.SetCursorPosition(p.X, p.Y + SENSITIVITY);
                    break;
                case "sub1x":
                    MouseOperations.SetCursorPosition(p.X - SENSITIVITY, p.Y);
                    break;
                case "sub1y":
                    MouseOperations.SetCursorPosition(p.X, p.Y - SENSITIVITY);
                    break;
                default:
                    Console.WriteLine("Something failed");
                    break;
                case "click":
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
                    break;
            }
        }
    }
}