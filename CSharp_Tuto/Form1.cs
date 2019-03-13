using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp_Tuto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// BMI = 體重 / 身高^2 
        /// input : 
        ///     weight: 體重 (kg)
        ///     height: 身高 (m)
        /// return : BMI 
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private double GetBMI(double weight, double height)
        {
            double BMI = 0;
            BMI = weight / Math.Pow(height, 2);
            return BMI;
        }

        /// <summary>
        /// 正常範圍 18.5≦BMI＜24 
        /// </summary>
        /// <param name="BMI"></param>
        /// <returns></returns>
        private string Is_BMI_normal(double BMI)
        {
            if (BMI < 18.5)
            {
                return "過輕";
            }
            else if (BMI >= 24)
            {
                return "過重";
            }
            else
            {
                return "正常";
            }
        }

        private void button_calculate_Click(object sender, EventArgs e)
        {

            double weight = Convert.ToDouble(txtbox_weight.Text);
            double height = Convert.ToDouble(txtbox_height.Text);

            var bmi = GetBMI(weight, height);
            var is_over_weight = Is_BMI_normal(bmi);

            richTextBox_show_result.Text = "";
            richTextBox_show_result.Text += "BMI = " + bmi;
            richTextBox_show_result.Text += "\r"; // 換行 
            richTextBox_show_result.Text += is_over_weight;
            richTextBox_show_result.Text += "\r";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Human human_1 = new Human("Willson", "M", 87, 187);

            var bmi = human_1.GetBMI();

            richTextBox_show_result.Text = "";
            richTextBox_show_result.Text += human_1.Name;
            richTextBox_show_result.Text += "\r"; // 換行 
            richTextBox_show_result.Text += human_1.Gender;
            richTextBox_show_result.Text += "\r"; // 換行 
            richTextBox_show_result.Text += human_1.GetBMI();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dictionary<int, Human> People = new Dictionary<int, Human>();
            Random rnd = new Random();

            for (int i = 0; i < 100; i++)
            {
                People.Add(i, new Human("Willson" + i, "M", 60 + rnd.Next(30), 180 + rnd.Next(10)));
            }

            richTextBox_show_result.Text = "";

            foreach (var aa in People)
            {
                var number = aa.Key;
                var PERSON = aa.Value;

                richTextBox_show_result.Text += number;
                richTextBox_show_result.Text += "\r"; // 換行 
                richTextBox_show_result.Text += PERSON.Name;
                richTextBox_show_result.Text += "\r"; // 換行 
                richTextBox_show_result.Text += PERSON.GetBMI();
                richTextBox_show_result.Text += "\r"; // 換行 
                richTextBox_show_result.Text += Is_BMI_normal(PERSON.GetBMI());
                richTextBox_show_result.Text += "\r"; // 換行 
                richTextBox_show_result.Text += "======="; // 換行 
                richTextBox_show_result.Text += "\r"; // 換行 
            }
        }
    }
}
