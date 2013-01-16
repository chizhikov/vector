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
        List<Shape> tempShapes = new List<Shape>();

        Line l = new Line(new Point(), new Point());
        Circle c = new Circle(0, 0, 0, 0);

        int Type = 0; // Line;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Shape p in this.Shapes)
            {
                p.DrawWith(e.Graphics);
            }
            if (tempShapes.Count() != 0)
            {
                foreach (Shape p in this.tempShapes)
                {
                    p.DrawWith(e.Graphics);
                }
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
            if (l.s.X == e.X && l.s.Y == e.Y)
            {
                Shapes.Add(new Cross(e.X, e.Y, 3));
            }
            else
            {
                if (tempShapes.Count() != 0)
                {
                    Shapes.Add(tempShapes.Last());
                    tempShapes.Remove(tempShapes.Last());
                }
            }

            l.Move = false;
            c.Move = false;
            this.Refresh();
            listBox1.Items.Clear();

            foreach (Shape p in this.Shapes)
            {
                listBox1.Items.Add(p.Desc);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Type = 0; // Line.
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Type = 1; // Circle.
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = Convert.ToString(e.X) + " " + Convert.ToString(e.Y);

            if (l.Move == true || c.Move == true)
            {
                if (tempShapes.Count() != 0)
                {
                    tempShapes.Remove(tempShapes.Last());
                }

                if (Type == 0)
                {
                    tempShapes.Add(new Line(l.s, new Point(e.X, e.Y)));
                }
                else if (Type == 1)
                {
                    tempShapes.Add(new Circle(c.X, c.Y, e.X - c.X, e.Y - c.Y));
                }
                this.Refresh();
            }
        }

        private void сохранитькакToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string curFile = "test.txt";
            StreamWriter sw = new StreamWriter(curFile);
            foreach (Shape p in this.Shapes)
            {
                p.SaveTo(sw);
            }
            sw.Close();
        }

        private void открытьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Shapes.Clear();
            string curFile = "test.txt";
            StreamReader sr = new StreamReader(curFile);
            while (!sr.EndOfStream)
            {
                string type = sr.ReadLine();
                switch (type)
                {
                    case "Cross":
                        Shapes.Add(new Cross(sr));
                        break;
                    case "Line":
                        Shapes.Add(new Line(sr));
                        break;
                    case "Circle":
                        Shapes.Add(new Circle(sr));
                        break;
                }
            }
            sr.Close();
            this.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
