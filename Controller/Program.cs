using System;
using System.Runtime.InteropServices;
using System.Text;
class Program
{
    static void Main(string[] args)
    {
        //Listen for messages from the Webserver and pass them to the "Evaluate" function
        var input = Console.OpenStandardInput();

        var buffer = new byte[1024];
        int length;

        while (input.CanRead && (length = input.Read(buffer, 0, buffer.Length)) > 0)
        {
            var payload = new byte[length];
            Buffer.BlockCopy(buffer, 0, payload, 0, length);

            string data = Encoding.UTF8.GetString(payload);
            Evaluate(data);

            Console.Out.Flush();
        }
    }
    const int SENSITIVITY = 5;

    static void Evaluate(string data)
    {
        MouseOperations.MousePoint p = MouseOperations.GetCursorPosition();

        switch (data)
        {
            case "add1x":
                MouseOperations.SetCursorPosition(p.X + SENSITIVITY, p.Y);
                Console.WriteLine("Executed add1x");
                break;
            case "add1y":
                MouseOperations.SetCursorPosition(p.X, p.Y + SENSITIVITY);
                Console.WriteLine("Executed add1y");
                break;
            case "sub1x":
                MouseOperations.SetCursorPosition(p.X - SENSITIVITY, p.Y);
                Console.WriteLine("Executed sub1x");
                break;
            case "sub1y":
                MouseOperations.SetCursorPosition(p.X, p.Y - SENSITIVITY);
                Console.WriteLine("Executed sub1y");
                break;
            case "click":
                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
                Console.WriteLine("Executed click");
                break;
            default:
                Console.WriteLine("Something failed");
                break;
        }
    }
}


public class MouseOperations
{
    [Flags]
    public enum MouseEventFlags
    {
        LeftDown = 0x00000002,
        LeftUp = 0x00000004,
        MiddleDown = 0x00000020,
        MiddleUp = 0x00000040,
        Move = 0x00000001,
        Absolute = 0x00008000,
        RightDown = 0x00000008,
        RightUp = 0x00000010
    }

    [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetCursorPos(out MousePoint lpMousePoint);

    [DllImport("user32.dll")]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    public static void SetCursorPosition(int x, int y)
    {
        SetCursorPos(x, y);
    }

    public static void SetCursorPosition(MousePoint point)
    {
        SetCursorPos(point.X, point.Y);
    }

    public static MousePoint GetCursorPosition()
    {
        MousePoint currentMousePoint;
        var gotPoint = GetCursorPos(out currentMousePoint);
        if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
        return currentMousePoint;
    }

    public static void MouseEvent(MouseEventFlags value)
    {
        MousePoint position = GetCursorPosition();

        mouse_event
            ((int)value,
             position.X,
             position.Y,
             0,
             0)
            ;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MousePoint
    {
        public int X;
        public int Y;

        public MousePoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}