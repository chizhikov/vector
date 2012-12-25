using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace Vector
{
    public abstract class Shape
    {
        public bool Move = false, Temp = false;
        public abstract string Desc
        {
            get;
        }
        public abstract void DrawWith(Graphics g);
        public abstract void SaveTo(StreamWriter sw);
    }

    public class Cross : Shape
    {
        int X, Y, Size;
        Pen p = new Pen(Color.Black);
        public override string Desc
        {
            get { return "Точка"; }
        }

        public Cross(int _X, int _Y, int _Size)
        {
            SetCoord(_X, _Y);
            SetSize(_Size);
        }
        public Cross(StreamReader sr)
        {
            X = Convert.ToInt32(sr.ReadLine());
            Y = Convert.ToInt32(sr.ReadLine());
            Size = Convert.ToInt32(sr.ReadLine());
        }
        public void SetCoord(int _X, int _Y)
        {
            X = _X;
            Y = _Y;
        }
        public void SetSize(int _Size)
        {
            Size = _Size;
        }
        public override void DrawWith(Graphics g)
        {
            g.DrawLine(p, X - Size, Y - Size, X + Size, Y + Size);
            g.DrawLine(p, X - Size, Y + Size, X + Size, Y - Size);
        }
        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Cross");
            sw.WriteLine(Convert.ToString(X));
            sw.WriteLine(Convert.ToString(Y));
            sw.WriteLine(Convert.ToString(Size));
        }
    }

    public class Line : Shape
    {
        public Point s, f;
        Pen p = new Pen(Color.Black);
        public override string Desc
        {
            get { return "Линия"; }
        }
        public Line(Point _s, Point _f, bool _Temp)
        {
            Temp = _Temp;
            SetCoord(_s, _f, Move);
        }
        public Line(StreamReader sr)
        {
            s.X = Convert.ToInt32(sr.ReadLine());
            s.Y = Convert.ToInt32(sr.ReadLine());
            f.X = Convert.ToInt32(sr.ReadLine());
            f.Y = Convert.ToInt32(sr.ReadLine());
        }
        public void SetCoord(Point _s, Point _f, bool _Move)
        {
            s = _s;
            f = _f;
            Move = _Move;
        }
        public override void DrawWith(Graphics g)
        {
            g.DrawLine(p, s, f);
        }
        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Line");
            sw.WriteLine(Convert.ToString(s.X));
            sw.WriteLine(Convert.ToString(s.Y));
            sw.WriteLine(Convert.ToString(f.X));
            sw.WriteLine(Convert.ToString(f.Y));
        }
    }

    public class Circle : Shape
    {
        public int X, Y, Rx, Ry;

        Pen p = new Pen(Color.Black);
        public override string Desc
        {
            get { return "Окружность"; }
        }

        public Circle(int _X, int _Y, int _Rx, int _Ry, bool _Temp)
        {
            Temp = _Temp;
            SetCoord(_X, _Y, _Rx, _Ry, Move);
        }
        public Circle(StreamReader sr)
        {
            X = Convert.ToInt32(sr.ReadLine());
            Y = Convert.ToInt32(sr.ReadLine());
            Rx = Convert.ToInt32(sr.ReadLine());
            Ry = Convert.ToInt32(sr.ReadLine());
        }
        public void SetCoord(int _X, int _Y, int _Rx, int _Ry, bool _Move)
        {
            X = _X;
            Y = _Y;
            Rx = _Rx;
            Ry = _Ry;
            Move = _Move;
        }
        public override void DrawWith(Graphics g)
        {
            g.DrawEllipse(p, X, Y, Rx, Rx);
        }
        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Circle");
            sw.WriteLine(Convert.ToString(X));
            sw.WriteLine(Convert.ToString(Y));
            sw.WriteLine(Convert.ToString(Rx));
            sw.WriteLine(Convert.ToString(Ry));
        }
    }
}
