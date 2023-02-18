using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pacman
{
    public partial class Form1 : Form
    {
        int score = 0;
        string side = "";
        int time_min = 0, time_sec = 0;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // برخورد پکمن با دیوار ها
            if (pictureBox_pacman.Bounds.IntersectsWith(pictureBox1.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox2.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox3.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox4.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox5.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox6.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox7.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox8.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox9.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox11.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox12.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox13.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox14.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox15.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox16.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox17.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox18.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox19.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox20.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox21.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox22.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox23.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox24.Bounds) ||
                pictureBox_pacman.Bounds.IntersectsWith(pictureBox27.Bounds))
            {

                switch (side)
                {
                    //یک حرکت به عقب پس از برخورد به دیوار
                    case "up": pictureBox_pacman.Top++;     break;
                    case "down": pictureBox_pacman.Top--;   break;
                    case "right": pictureBox_pacman.Left--; break;
                    case "left": pictureBox_pacman.Left++;  break;
                    
                }
                side = "";
                
            }

            //
            if (pictureBox_pacman.Left >= 400)
                pictureBox_pacman.Left = -20;
            else if (pictureBox_pacman.Left < -20)
                pictureBox_pacman.Left = 400;

            //حرکت پکمن
            switch (side)
            {
                case "up":   pictureBox_pacman.Top--;  break;
                case "down": pictureBox_pacman.Top++;  break;
                case "right":pictureBox_pacman.Left++; break;
                case "left": pictureBox_pacman.Left--; break;
            }
            
        }
        //ثانیه شمار
        private void timer2_Tick(object sender, EventArgs e)
        {
            time_sec++;

            if (time_sec == 60)
            {
                time_sec = 0;
                time_min++;
            }

            if (time_sec < 10)
                label_time_sec.Text = "0" + time_sec.ToString();
            else
                label_time_sec.Text = time_sec.ToString();

            if (time_min < 10)
                label_time_min.Text = "0" + time_min.ToString();
            else
                label_time_min.Text = time_min.ToString();


        }

        private void button_up_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            button_stop.Visible = true;
            side = "up";
            pictureBox_pacman.Image = Properties.Resources.pacman_up;
            timer2.Enabled = true;
        }

        private void button_down_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            button_stop.Visible = true;
            side = "down";
            pictureBox_pacman.Image = Properties.Resources.pacman_down;
            timer2.Enabled = true;
        }

        private void button_right_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            button_stop.Visible = true;
            side = "right";
            pictureBox_pacman.Image = Properties.Resources.pacman_right;
            timer2.Enabled = true;
        }

        private void button_left_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            button_stop.Visible = true;
            side = "left";
            pictureBox_pacman.Image = Properties.Resources.pacman_left;
            timer2.Enabled = true;
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
            button_stop.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
