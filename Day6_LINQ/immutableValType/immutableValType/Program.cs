using System;

namespace immutableValType
{
    interface IChange
    {
        void Change(int x, int y);
    }
    // Point  размерный тип.
    internal struct Point :IChange
    {
        private int _x, _y;
        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }
        public void Change(int x, int y)
        {
            _x = x; _y = y;
        }
        public override string ToString()
        {
            return string.Format("({0}, {1})", _x, _y);
        }
    }
    public sealed class Program
    {
        public static void Main()
        {
            Point p = new Point(1, 1);
            Console.WriteLine(p);
            p.Change(2, 2);
            Console.WriteLine(p);
            object o = p;
            Console.WriteLine(o);
            ((IChange)o).Change(3, 3);
            Console.WriteLine(o);
            Console.ReadLine();
        }
    }
}
