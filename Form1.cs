using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Runtime.InteropServices;
using System.IO.Ports;

namespace Tracker
{
    public partial class Form1 : Form
    {
        Capture cap;
        SerialPort Arduino = new SerialPort("COM5", 9600, Parity.None, 8, StopBits.One);
        private CascadeClassifier casc;
        private CascadeClassifier casce;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Loaded(object sender, EventArgs e)
        {
            cap = new Emgu.CV.Capture(0); 
            cap.Start();
            casc = new CascadeClassifier(@"c:\users\malik\documents\visual studio 2017\Projects\Tracker\Tracker\haarcascade_frontalface_default.xml");
            casce = new CascadeClassifier(@"C:\Users\Malik\Documents\Visual Studio 2017\Projects\Tracker\Tracker\haarcascade_eye.xml");
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (Arduino.IsOpen == true)
            {
                textBox5.Text = "Connected";
            }
            else
            {
                textBox5.Text = "Disconnected";
            }
            if (cap != null)
            {
                casc = new CascadeClassifier(@"c:\users\malik\documents\visual studio 2017\Projects\Tracker\Tracker\haarcascade_frontalface_default.xml");
                casce = new CascadeClassifier(@"C:\Users\Malik\Documents\Visual Studio 2017\Projects\Tracker\Tracker\haarcascade_eye.xml");
                Mat cf = new Mat();
                cap.Retrieve(cf);
                Image<Bgr, Byte> currentFrame = cf.ToImage<Bgr, byte>();
                pictureBox2.Image = cap.QueryFrame().Bitmap;

                if (cf != null & casc != null)
                {
                    int x = 0;int y = 0;int w = 0;int h = 0;

                    var grayFrame = currentFrame.Convert<Gray, byte>().Clone();
                    Rectangle[] Faces = casc.DetectMultiScale(grayFrame, 1.1, 4);

                    foreach (var face in Faces) {
                        currentFrame.Draw(face, new Bgr(0, 0, 255), 2);
                        x = face.X;
                        y = face.Y;
                        w = face.Width;
                        h = face.Height;
                    }

                    Rectangle[] Eyes = casce.DetectMultiScale(grayFrame, 1.1, 4);

                    foreach (var face in Eyes)
                    {
                        currentFrame.Draw(face, new Bgr(0, 255, 0), 2);
                    }

                    pictureBox3.Image = currentFrame.Bitmap;

                    string xs = x.ToString(); string ys = y.ToString(); string ws = w.ToString(); string hs = h.ToString();

                    textBox1.Text = xs;
                    textBox2.Text = ys;
                    textBox3.Text = ws;
                    textBox4.Text = hs;

                    int cx = (x + (x - w)) / 2;
                    int cy = (y + (y + h)) / 2;

                    textBox6.Text = cx + " , " + cy;

                    if (Arduino.IsOpen == true)
                    {
                        Arduino.Write("#" + cx + "!");
                        Arduino.Write("#" + cy + "!");
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cap = new Emgu.CV.Capture(0);
            cap.Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            String Com = "COM" + textBox8.Text;
            if (textBox8.TextLength >= 1 || Arduino.IsOpen == false)
            {
                SerialPort Arduino = new SerialPort(Com, 9600, Parity.None, 8, StopBits.One);
                Arduino.Open();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
