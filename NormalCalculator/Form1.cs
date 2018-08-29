using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NormalCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            updateResult();
        }

        private int RED = 128;
        private int GREEN = 128;
        private int BLUE = 128;

        //Update Color Preview
        private void updateResult()
        {
            RED = Int32.Parse(textBox2.Text);
            GREEN = Int32.Parse(textBox3.Text);
            BLUE = Int32.Parse(textBox4.Text);

            pictureBox1.BackColor = Color.FromArgb(RED, GREEN, BLUE);

            string redHex = RED.ToString("X2");
            string greenHex = GREEN.ToString("X2");
            string blueHex = BLUE.ToString("X2");

            textBox1.Text = redHex + greenHex + blueHex;

            if(autoClipBoard)
                Clipboard.SetText(textBox1.Text);
        }

        //Invert Green Channel
        private bool greenReversed = false;
        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            string tmp = label6.Text;

            label6.Text = label7.Text;
            label7.Text = tmp;

            tmp = button4.Text;
            button4.Text = button5.Text;
            button5.Text = tmp;

            greenReversed = !greenReversed;

            trackBar2.Value = trackBar2.Maximum - trackBar2.Value;
        }

        //Change Red Divisions
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            trackBar1.Maximum = (int)numericUpDown1.Value;

            trackBar1_ValueChanged(sender, e);
        }

        //Change Green Divisions
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            trackBar2.Maximum = (int)numericUpDown2.Value;

            trackBar2_ValueChanged(sender, e);
        }

        //Change Blue Divisions
        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            trackBar3.Maximum = (int)numericUpDown3.Value;

            trackBar3_ValueChanged(sender, e);
        }

        //Red Change
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            textBox2.Text = (Math.Ceiling((double)trackBar1.Value * 255 / trackBar1.Maximum)).ToString();

            textBox7.Text = trackBar1.Value.ToString();

            updateResult();
        }

        //Green Change
        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            if(greenReversed)
                textBox3.Text = (255 - Math.Floor((double)trackBar2.Value * 255 / trackBar2.Maximum)).ToString();
            else
                textBox3.Text = (Math.Ceiling((double)trackBar2.Value * 255 / trackBar2.Maximum)).ToString();

            textBox6.Text = trackBar2.Value.ToString();

            updateResult();
        }

        //Blue Change
        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            textBox4.Text = (Math.Ceiling((double)trackBar3.Value * 255 / trackBar3.Maximum)).ToString();

            textBox5.Text = trackBar3.Value.ToString();

            updateResult();
        }

        //Increment Red
        private void button3_Click(object sender, EventArgs e)
        {
            if(trackBar1.Maximum > trackBar1.Value)
                trackBar1.Value++;
        }

        //Decrement Red
        private void button2_Click(object sender, EventArgs e)
        {
            if (trackBar1.Minimum < trackBar1.Value)
                trackBar1.Value--;
        }

        //Increment Green
        private void button5_Click(object sender, EventArgs e)
        {
            if (trackBar2.Maximum > trackBar2.Value)
                trackBar2.Value++;
        }

        //Decrement Green
        private void button4_Click(object sender, EventArgs e)
        {
            if (trackBar2.Minimum < trackBar2.Value)
                trackBar2.Value--;
        }

        //Increment Blue
        private void button7_Click(object sender, EventArgs e)
        {
            if (trackBar3.Maximum > trackBar3.Value)
                trackBar3.Value++;
        }

        //Decrement Blue
        private void button6_Click(object sender, EventArgs e)
        {
            if (trackBar3.Minimum < trackBar3.Value)
                trackBar3.Value--;
        }

        //Toggle auto clipboard
        bool autoClipBoard = true;
        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            autoClipBoard = !autoClipBoard;
        }

        //Copy to Clipboard
        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }

        //Toggle keep on top
        private void checkBox3_CheckStateChanged(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
        }

        //Draw lighting line
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 4);
            pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            int lineSize = 40;
            int endX = (Int32.Parse(textBox3.Text) - 128) * lineSize / 128;
            int endY = (Int32.Parse(textBox2.Text) - 128) * lineSize / 128;
            e.Graphics.DrawLine(pen, pictureBox1.Width/2, pictureBox1.Height / 2, pictureBox1.Width / 2 - endX, pictureBox1.Height / 2 - endY);
        }
    }
}
