﻿using Raptor.Properties;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using IronPython.Hosting;
using System.IO;

namespace Raptor
{
    public partial class Form1 : Form
    {
        int raptor_money = Int32.Parse(Settings.Default["Raptor"].ToString());
        int raptor2_money = Int32.Parse(Settings.Default["Raptor2"].ToString());
        int summ = Int32.Parse(Settings.Default["Всего"].ToString());
        int sold = Int32.Parse(Settings.Default["sold"].ToString());
        int sold2 = Int32.Parse(Settings.Default["sold2"].ToString());
        int ad_bool = Int32.Parse(Settings.Default["ad_bool"].ToString());



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label10.Text = "$" + Settings.Default["Raptor"].ToString();
            label11.Text = "$" + Settings.Default["Raptor2"].ToString();
            label7.Text = "$" + Settings.Default["Всего"].ToString();
            if (sold == 0)
            {
                raptor_status.Text = "Свободен";
            }
            else
            {
                DateTime dt = DateTime.Parse(Settings.Default["raptor2_time"].ToString());
                raptor_status.Text = "Занят до " + dt.ToShortTimeString();
            }
            if (sold2 == 0)
            {
                raptor2_status.Text = "Свободен";
            }
            else
            {
                DateTime dt = DateTime.Parse(Settings.Default["raptor2_time"].ToString());
                raptor2_status.Text = "Занят до " + dt.ToShortTimeString();
            }
            if (ad_bool == 0)
            {
                label14.Text = "Статус объявления: Занято!";
            }
            else label14.Text = "Статус объявления: Свободно!";
            
        }

        private void rent_raptor(int a, int b, int c)
        {
            DateTime dt = DateTime.Now.AddMinutes(a).AddHours(b);
            raptor_status.Text = "Занят до " + dt.ToShortTimeString();
            textBox3.Visible = true;
            label8.Visible = false;
            label8.Text = textBox3.Text;
            raptor_money = raptor_money + c;
            label10.Text = "$" + raptor_money.ToString();
            sold = 1;
            Settings.Default["sold"] = sold;
            Settings.Default["raptor_time"] = dt;
            Settings.Default["Raptor"] = raptor_money;
            Settings.Default.Save();
        }

        private void rent_raptor2(int a, int b, int c)
        {
            DateTime dt = DateTime.Now.AddMinutes(a).AddHours(b);
            raptor2_status.Text = "Занят до " + dt.ToShortTimeString();
            textBox4.Visible = true;
            label12.Visible = false;
            label12.Text = textBox3.Text;
            raptor2_money = raptor2_money + c;
            label11.Text = "$" + raptor2_money.ToString();
            Settings.Default["raptor2_time"] = dt;
            Settings.Default["Raptor2"] = raptor2_money;
            Settings.Default.Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rent_raptor(30, 1, 5000);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rent_raptor(0, 2, 6000);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rent_raptor(0, 3, 9000);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            rent_raptor(0, 4, 11000);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            label8.Text = textBox3.Text;
            label8.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            rent_raptor2(30, 1, 5000);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            rent_raptor2(0, 2, 6000);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            rent_raptor2(0, 3, 9000);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            rent_raptor2(0, 4, 11000);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            label12.Text = textBox4.Text;
            label12.Visible = false;
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox4.Visible = false;
                label12.Visible = true;
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox3.Visible = false;
                label8.Visible = true;

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            raptor_status.Text = "Занят до рестарта";
            textBox3.Visible = true;
            raptor_money = raptor_money + Int32.Parse(textBox2.Text);
            label10.Text = "$" + raptor_money.ToString();
            Settings.Default["Raptor"] = raptor_money;
            Settings.Default.Save();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            raptor2_status.Text = "Занят до рестарта";
            textBox4.Visible = true;
            raptor2_money = raptor2_money + Int32.Parse(textBox5.Text);
            label11.Text = "$" + raptor2_money.ToString();
            Settings.Default["Raptor2"] = raptor2_money;
            Settings.Default.Save();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            raptor_status.Text = "Свободен";
            raptor2_status.Text = "Свободен";
            raptor_money = 0;
            raptor2_money = 0;
            label10.Text = "$" + raptor_money.ToString();
            label11.Text = "$" + raptor2_money.ToString();
            Settings.Default["Raptor"] = raptor_money;
            Settings.Default["Raptor2"] = raptor2_money;
            Settings.Default.Save();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            summ = summ + raptor_money + raptor2_money;
            raptor_money = 0;
            raptor2_money = 0;
            label7.Text = "$" + summ.ToString();
            label10.Text = "$" + raptor_money.ToString();
            label11.Text = "$" + raptor2_money.ToString();
            Settings.Default["Всего"] = summ;
            Settings.Default["Raptor"] = raptor_money;
            Settings.Default["Raptor2"] = raptor2_money;
            Settings.Default.Save();
        }

        private void start_script()
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("cd..");
            cmd.StandardInput.WriteLine("cd..");
            cmd.StandardInput.WriteLine("cd..");
            cmd.StandardInput.WriteLine("cd..");
            cmd.StandardInput.WriteLine("cd..");
            cmd.StandardInput.WriteLine("cd..");
            cmd.StandardInput.WriteLine("cd C:\\Users\\Puxlozadiy\\PycharmProjects\\bot.py");
            cmd.StandardInput.WriteLine("python main.py");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            
            cmd.Close();
        }
        private void button17_Click(object sender, EventArgs e)
        {
            if (ad_bool == 1)
            {
                start_script();
                ad_bool = 0;
                label14.Text = "Статус объявления: Занято!";
            }
            else
            {
                DateTime dt = DateTime.Parse(Settings.Default["cooldown"].ToString());
                int compare = DateTime.Now.CompareTo(dt);
                if (compare > 0)
                {
                    start_script();
                    ad_bool = 1;
                    label14.Text = "Статус объявления: Свободно!";
                    Settings.Default["cooldown"] = DateTime.Now.AddMinutes(30);
                    Settings.Default.Save();
                }
                else
                {
                    label14.Text = "Сейчас нельзя отправить объявление!";
                    timer1.Interval = 5000;
                    timer1.Start();
                }
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ad_bool == 0)
            {
                label14.Text = "Статус объявления: Занято!";
            }
            else label14.Text = "Статус объявления: Свободно!";
            timer1.Stop();
        }
    }
}
