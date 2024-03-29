﻿using System;
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
        string side = "",
            side_ghost1 = "right",
            side_ghost2 = "up";

        int time_min = 0,
            time_sec = 0,
            score = 0,
            count_health = 3;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            //حرکت پکمن
            switch (side)
            {
                case "up": pictureBox_pacman.Top--; break;
                case "down": pictureBox_pacman.Top++; break;
                case "right": pictureBox_pacman.Left++; break;
                case "left": pictureBox_pacman.Left--; break;
            }

            foreach (Control item in this.Controls)
            {   
                // برخورد پکمن با دیوار ها
                if (pictureBox_pacman.Bounds.IntersectsWith(item.Bounds))
                {
                    switch ((string)item.Tag)
                    {
                        case "wall":
                            switch(side)
                            {
                                //یک حرکت به عقب پس از برخورد به دیوار
                                case "up": pictureBox_pacman.Top++; break;
                                case "down": pictureBox_pacman.Top--; break;
                                case "right": pictureBox_pacman.Left--; break;
                                case "left": pictureBox_pacman.Left++; break;
                            }
                            side = "";
                            break;
                        case "food"://برخورد با غذا
                            if (item.Visible)
                            {
                                score += 10;
                                item.Visible = false;
                                item.Tag = "_food";
                            }
                            label_score.Text = "امتیاز:" + score.ToString();
                            break;
                        case "ghost"://برخورد پکمن با روح
                            if (item.Visible)
                            {
                                item.Tag = "_ghost";
                                score -= 20;
                                label_score.Text = "امتیاز:" + score.ToString();
                                pictureBox_pacman.Left = 191;
                                pictureBox_pacman.Top = 314;
                                pictureBox_pacman.Image = Properties.Resources.pacman_right;
                                side = "";
                                count_health--;
                                switch (count_health)
                                {
                                    case 2: picHealth3.Visible = false; break;
                                    case 1: picHealth2.Visible = false; break;
                                    case 0: picHealth1.Visible = false; side = ""; break;
                                }
                            }
                            break;
                    }
                }

                //برخود روح 1 به دیوار
                if (picGhost1.Bounds.IntersectsWith(item.Bounds) && (string)item.Tag == "wall")
                {
                    side_ghost1 = side_ghost1 == "right" ? side_ghost1 = "left" : side_ghost1 = "right";
                }

            }


            // حرکت روح 1
            if (picGhost1.Visible)
            {
                if (side_ghost1 == "right")
                {
                    picGhost1.Image = Properties.Resources.ghost_right;
                    picGhost1.Left++;
                }
                else
                {
                    picGhost1.Image = Properties.Resources.ghost_left;
                    picGhost1.Left--;
                }
            }
            // حرکت روح 2
            if (picGhost2.Visible)
            {

                switch (side_ghost2)
                {
                    case "up":
                        picGhost2.Top--;
                        break;
                    case "down":
                        picGhost2.Top++;
                        break;
                    case "right":
                        picGhost2.Left++;
                        picGhost2.Image = Properties.Resources.ghost_right;
                        break;
                    case "left":
                        picGhost2.Left--;
                        picGhost2.Image = Properties.Resources.ghost_left;
                        break;
                }

                if (picGhost2.Top == 314 && picGhost2.Left == 291)
                    side_ghost2 = "up";
                else if ((picGhost2.Top == 214 && picGhost2.Left == 291) || (picGhost2.Top == 214 && picGhost2.Left == 191))
                    side_ghost2 = "left";
                else if (picGhost2.Top == 214 && picGhost2.Left == 85)
                    side_ghost2 = "down";
                else if (picGhost2.Top == 314 && picGhost2.Left == 85)
                    side_ghost2 = "right";
            }


            //خارج شدن پکمن از محیط بازی
            if (pictureBox_pacman.Left >= 400)
                pictureBox_pacman.Left = -20;
            else if (pictureBox_pacman.Left < -20)
                pictureBox_pacman.Left = 400;

            //خارج شدن روح یک
            if (picGhost1.Left >= 400)
                picGhost1.Left = -20;
            else if (picGhost1.Left < -20)
                picGhost1.Left = 400;

        } //timer1 پایان 


        //ثانیه شمار
        private void timer2_Tick(object sender, EventArgs e)
        {
            time_sec++;

            if (time_sec == 60)
            {
                time_sec = 0;
                time_min++;
            }
            string time_sec_string = time_sec.ToString();
            string time_min_string = time_min.ToString();

            label_time_sec.Text = time_sec < 10 ? "0" + time_sec_string : time_sec_string;
            label_time_min.Text = time_min < 10 ? "0" + time_min_string : time_min_string;

            //اضافه کردن روح ها به بازی
            if (time_sec == 30 && picGhost1.Visible == false)
                picGhost1.Visible = true;
            if(time_sec == 1 && picGhost2.Visible == false)
                picGhost2.Visible = true;

            if ((string)picGhost1.Tag == "_ghost")
                picGhost1.Tag = "ghost";

            if ((string)picGhost2.Tag == "_ghost")
                picGhost2.Tag = "ghost";

            //چک کردن وجود غذا
            //اگر غذایی باقی نماند پایان بازی
            int count_food = 188;
            bool end_game = false;
            foreach (Control item in Controls)
            {
                if ((string)item.Tag == "_food")
                    count_food--;
            }

            end_game = count_food == 0 ? true : false;

            if (end_game || count_health < 1)
            {
                timer1.Enabled = false;
                timer2.Enabled = false;

                if(end_game)
                    MessageBox.Show("امتیاز شما = " + score.ToString() + "\nزمان = " + time_min.ToString() + ":" + time_sec.ToString(), "شما بردید", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("امتیاز شما = " + score.ToString() + "\nزمان = " + time_min.ToString() + ":" + time_sec.ToString(), "شما باختید", MessageBoxButtons.OK, MessageBoxIcon.Stop);


                if (MessageBox.Show("آیا دوباره بازی می کنید؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    score = 0;
                    side = "";
                    time_min = 0;
                    time_sec = 0;
                    count_health = 3;

                    pictureBox_pacman.Left = 191;
                    pictureBox_pacman.Top = 314;
                    pictureBox_pacman.Image = Properties.Resources.pacman_right;

                    picGhost2.Left = 191;
                    picGhost2.Top = 252;
                    side_ghost2 = "up";

                    foreach (Control item in this.Controls)
                    {
                        if (!item.Visible)
                            item.Visible = true;

                        if ((string)item.Tag == "_food")
                            item.Tag = "food";
                    }

                    picGhost1.Visible = false;
                    picGhost2.Visible = false;
                    end_game = false;

                    label_score.Text = "امتیاز: صفر";
                    label_time_min.Text = "00";
                    label_time_sec.Text = "00";
                }
                else
                    Application.Exit();
            }            
        }//timer2 پایان

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            lbl_guide1.Visible = false;
            lbl_guide2.Visible = false;
            timer1.Enabled = true;
            timer2.Enabled = true;
            
            switch (e.KeyCode)
            {
                case Keys.Up:
                    side = "up";
                    pictureBox_pacman.Image = Properties.Resources.pacman_up;
                    break;
                case Keys.Down:
                    side = "down";
                    pictureBox_pacman.Image = Properties.Resources.pacman_down;
                    break;
                case Keys.Right:
                    side = "right";
                    pictureBox_pacman.Image = Properties.Resources.pacman_right;
                    break;
                case Keys.Left:
                    side = "left";
                    pictureBox_pacman.Image = Properties.Resources.pacman_left;
                    break;
                case Keys.Space:
                    timer1.Enabled = false;
                    timer2.Enabled = false;
                    break;
                case Keys.Escape:
                    Application.Exit();
                    break;
            }
        }
    }
}
