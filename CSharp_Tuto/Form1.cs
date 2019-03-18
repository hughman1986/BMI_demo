using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        private void button_readCSV_Click(object sender, EventArgs e)
        {
            var Data = OpenCSV("./list.csv");

            List<Human> People = new List<Human>();

            foreach (DataRow row in Data.Rows)
            {
                var name = row[0];
                var gender = row[1];
                var height = row[2];
                var weight = row[3];

                Human person = new Human((string)name, (string)gender, Convert.ToDouble(weight), Convert.ToDouble(height));

                People.Add(person);
            }

            foreach (var PERSON in People)
            { 
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



        /// <summary>
        /// 將CSV文件的數據讀取到DataTable中
        /// </summary>
        /// <param name="fileName">CSV文件路徑</param>
        /// <returns>返回讀取了CSV數據的DataTable</returns>
        public static DataTable OpenCSV(string filePath)
        {
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            //StreamReader sr = new StreamReader(fs, encoding);
            //string fileContent = sr.ReadToEnd();
            //記錄每次讀取的一行記錄
            string strLine = "";
            //記錄每行記錄中的各字段內容
            string[] aryLine = null;
            string[] tableHead = null;
            //標示列數
            int columnCount = 0;
            //標示是否是讀取的第一行
            bool IsFirst = true;
            //逐行讀取CSV中的數據
            while ((strLine = sr.ReadLine()) != null)
            {
                if (IsFirst == true)
                {
                    tableHead = strLine.Split(',');
                    IsFirst = false;
                    columnCount = tableHead.Length;
                    //創建列
                    for (int i = 0; i < columnCount; i++)
                    {
                        tableHead[i] = tableHead[i].Replace("\"", "");
                        DataColumn dc = new DataColumn(tableHead[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    aryLine = strLine.Split(',');
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j].Replace("\"", "");
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (aryLine != null && aryLine.Length > 0)
            {
                dt.DefaultView.Sort = tableHead[2] + " " + "DESC";
            }
            sr.Close();
            fs.Close();
            return dt;
        }
    }

}
