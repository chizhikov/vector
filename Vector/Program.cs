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
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
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
}
