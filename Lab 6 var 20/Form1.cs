using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Lab_6_var_20
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        public static Thread myThread;
        public static string pi_string;
        public static double pi_count;
        public static bool first_symbol = false;

        public static void Start()
        {
            int count = 3;
            string after_dot;
            double number = 4 - 4 / count;
            pi_count = Math.Round(number);
            Thread.Sleep(100);

            //по спец. формуле высчитываем PI, добавляем значения в строку для вывода;
            for (int i = 0; i < 100; i++)
            {
                count += 2;
                number += 4 / count;
                after_dot = Convert.ToString(number);
                for (int j = 0; j < after_dot.Length; j++)
                {
                    if (j == after_dot.Length - 1)
                    {
                        number = Convert.ToChar(after_dot[j]);
                    }
                }
                pi_count = Math.Round(number);
                Thread.Sleep(100);
                count += 2;
                number -= 4 / count;
                after_dot = Convert.ToString(number);
                for (int j = 0; j < after_dot.Length; j++)
                {
                    if (j == after_dot.Length - 1)
                    {
                        number = Convert.ToChar(after_dot[j]);
                    }
                }
                pi_count = Math.Round(number);
                Thread.Sleep(100);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Empty;

            //запускаем второй поток для подсчета числа PI;
            myThread = new Thread(new ThreadStart(Start));
            myThread.Priority = ThreadPriority.Highest;
            myThread.Start();

            //цикл будет выводить значение PI на экран пока второй поток выполняется;
            while (myThread.IsAlive)
            {
                myThread.Join(100);
                pi_string = Convert.ToString(pi_count);
                richTextBox1.Text += pi_string;

                if (first_symbol == false)
                {
                    pi_string = ",";
                    first_symbol = true;
                    richTextBox1.Text += pi_string;
                }
            }

            first_symbol = false;
        }
    }
}