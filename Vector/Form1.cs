using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace Vector
{
    public partial class Form1 : Form
    {
        List<Shape> Shapes = new List<Shape>();
        Line l = new Line(new Point(), new Point(), false);
        Circle c = new Circle(0, 0, 0, 0, false);

        int Type = 0; // Line;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //listBox1.Items.Clear();
            foreach (Shape p in this.Shapes)
            {
                p.DrawWith(e.Graphics);
                //listBox1.Items.Add(p.Desc);
            }
        }

        private void Form1_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (Type == 0)
            {
                l.SetCoord(new Point(e.X, e.Y), new Point(0, 0), true);
            }
            else if (Type == 1)
            {
                c.SetCoord(e.X, e.Y, 0, 0, true);
            }
        }

        private void Form1_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (Shapes.Count() != 0 && Shapes.Last().Temp == true)
            {
                Shapes.Remove(Shapes.Last());
            }
            if (l.s.X == e.X && l.s.Y == e.Y)
            {
                Shapes.Add(new Cross(e.X, e.Y, 3));
            }
            else
            {
                if (Type == 0)
                {
                    Shapes.Add(new Line(l.s, new Point(e.X, e.Y), false));
                }
                else if (Type == 1)
                {
                    Shapes.Add(new Circle(c.X, c.Y, e.X - c.X, e.Y - c.Y, false));
                }
            }
            l.Move = false;
            c.Move = false;
            this.Refresh();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Type = 0; // Line.
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Type = 1; // Circle.
        }

    }

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
