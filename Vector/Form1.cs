﻿using System;
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
            listBox1.Items.Clear();
            foreach (Shape p in this.Shapes)
            {
                p.DrawWith(e.Graphics);
                listBox1.Items.Add(p.Desc);
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

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = Convert.ToString(e.X) + " " + Convert.ToString(e.Y);

            if (Type == 0)
            {
                if (Shapes.Count() != 0 && l.Move == true)
                {
                    if (Shapes.Last().Temp == true)
                    {
                        Shapes.Remove(Shapes.Last());
                    }
                    Shapes.Add(new Line(l.s, new Point(e.X, e.Y), true));
                    this.Refresh();
                }
            }
            else if (Type == 1)
            {
                if (Shapes.Count() != 0 && c.Move == true)
                {
                    if (Shapes.Last().Temp == true)
                    {
                        Shapes.Remove(Shapes.Last());
                    }
                    Shapes.Add(new Circle(c.X, c.Y, e.X - c.X, e.Y - c.Y, true));
                    this.Refresh();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void сохранитькакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string curFile = "test.txt";
            StreamWriter sw = new StreamWriter(curFile);
            foreach (Shape p in this.Shapes)
            {
                p.SaveTo(sw);
            }
            sw.Close();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Shapes.Clear();
            string curFile = "test.txt";
            StreamReader sr = new StreamReader (curFile);
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
    }

    


    

}
