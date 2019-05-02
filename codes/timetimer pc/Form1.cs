using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Windows.Forms.Design;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace timetimer_pc
{
    public partial class Form1 : Form
    {
        private string readjson = File.ReadAllText(@"data\setting.json");
        private float[] button1 = new float[4] { };
        private float[] button2 = new float[4] { };

        public Form1()
        {
            InitializeComponent();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            panel1.Show();
            panel2.Hide();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            panel2.Show();
        }

        private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            JObject set = JObject.Parse(readjson);
            if (metroCheckBox1.Checked)
            {
                set["showwatch"] = true;
                File.WriteAllText(@"data\setting.json", set.ToString());
                label3.Show();
            }
            else
            {
                set["showwatch"] = false;
                File.WriteAllText(@"data\setting.json", set.ToString());
                label3.Hide();
            }
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            JObject set = JObject.Parse(readjson);
            if (metroComboBox1.Text == "한글 (Korean)")
            {
                metroButton1.Text = "시작 화면";
                metroButton2.Text = "설정";
                metroCheckBox1.Text = "디지털 타이머 표시";
                label1.Text = "분";
                label2.Text = "초";
                metroButton3.Text = "시작";
                int a = metroComboBox2.SelectedIndex;
                metroComboBox2.Items.Clear();
                string[] korean = new string[] {"빨강","노랑","초록","파랑","검정","하양" };
                metroComboBox2.Items.AddRange(korean);
                metroComboBox2.SelectedIndex = a;
                set["language"] = "Korean";
                File.WriteAllText(@"data\setting.json",set.ToString());
                readjson = File.ReadAllText(@"data\setting.json");
            }
            else if (metroComboBox1.Text == "영어 (English)")
            {
                metroButton1.Text = "Main page";
                metroButton2.Text = "Setting";
                metroCheckBox1.Text = "Show digital timer";
                label1.Text = "M";
                label2.Text = "S";
                metroButton3.Text = "Start";
                int a = metroComboBox2.SelectedIndex;
                metroComboBox2.Items.Clear();
                string[] english = new string[] { "Red", "Yellow", "Green", "Blue", "Black", "White" };
                metroComboBox2.Items.AddRange(english);
                metroComboBox2.SelectedIndex = a;
                set["language"] = "English";
                File.WriteAllText(@"data\setting.json", set.ToString());
                readjson = File.ReadAllText(@"data\setting.json");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.SetSelected(1, true);
            listBox2.SetSelected(0, true);
            listBox1.SetSelected(0, true);
            JObject set = JObject.Parse(readjson);
            if (set["language"].ToString() == "Korean")
            {
                metroComboBox1.Text = "한글 (Korean)";

                switch (set["color"].ToString())
                {
                    case "red":
                        metroComboBox2.SelectedIndex = 0;
                        break;
                    case "yellow":
                        metroComboBox2.SelectedIndex = 1;
                        break;
                    case "green":
                        metroComboBox2.SelectedIndex = 2;
                        break;
                    case "blue":
                        metroComboBox2.SelectedIndex = 3;
                        break;
                    case "black":
                        metroComboBox2.SelectedIndex = 4;
                        break;
                    case "white":
                        metroComboBox2.SelectedIndex = 5;
                        break;
                }
            }
            if (set["language"].ToString() == "English")
            {
                metroButton1.Text = "Main page";
                metroButton2.Text = "Setting";
                metroCheckBox1.Text = "Show digital timer";
                metroComboBox1.Text = "영어 (English)";
                label1.Text = "M";
                label2.Text = "S";
                metroComboBox2.Items.Clear();
                string[] english = new string[] { "Red", "Yellow", "Green", "Blue", "Black", "White" };
                metroComboBox2.Items.AddRange(english);
                switch (set["color"].ToString())
                {
                    case "red":
                        metroComboBox2.SelectedIndex = 0;
                        break;
                    case "yellow":
                        metroComboBox2.SelectedIndex = 1;
                        break;
                    case "green":
                        metroComboBox2.SelectedIndex = 2;
                        break;
                    case "blue":
                        metroComboBox2.SelectedIndex = 3;
                        break;
                    case "black":
                        metroComboBox2.SelectedIndex = 4;
                        break;
                    case "white":
                        metroComboBox2.SelectedIndex = 5;
                        break;
                }
            }
            if ((bool)set["showwatch"])
            {
                metroCheckBox1.Checked = true;
            }
            else
            {
                metroCheckBox1.Checked = false;
                label3.Hide();
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            JObject set = JObject.Parse(readjson);
            int a = listBox1.SelectedIndex * 60;
            int b = listBox2.SelectedIndex;
            if (a + b > 3600)
            {
                if (set["language"].ToString() == "Korean")
                {
                    MessageBox.Show("이 타이머는 최대 60분 0초까지 설정 가능합니다.","범위 초과");
                    listBox2.SetSelected(0, true);
                }
                else if (set["language"].ToString() == "English")
                {
                    MessageBox.Show("This timer can set 60h 0s", "overflow");
                    listBox2.SetSelected(0, true);
                }
            }
            else
            {
                circularProgressBar1.Value = a + b;
                label3.Text = $"{a / 60}:{b}";
            }
        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            JObject set = JObject.Parse(readjson);
            int a = listBox1.SelectedIndex * 60;
            int b = listBox2.SelectedIndex ;
            if (a + b > 3600)
            {
                if (set["language"].ToString() == "Korean")
                {
                    MessageBox.Show("이 타이머는 최대 60분 0초까지 설정 가능합니다.", "범위 초과");
                    listBox2.SetSelected(0, true);
                }
                else if (set["language"].ToString() == "English")
                {
                    MessageBox.Show("This timer can set 60h 0s", "overflow");
                    listBox2.SetSelected(0, true);
                }
            }
            else
            {
                circularProgressBar1.Value = a + b;
                label3.Text = $"{a / 60}:{b}";
            }
        }

        private async void metroButton3_Click(object sender, EventArgs e)
        {
            metroButton3.Enabled = false;
            int a = listBox1.SelectedIndex * 60 + listBox2.SelectedIndex;
            int b = 0;
            int c = 0;
            listBox1.Enabled = false;
            listBox2.Enabled = false;
            for (;a >= 0; --a)
            {
                circularProgressBar1.Value = a;
                label3.Text = $"{a / 60}:{a % 60}";
                await Task.Delay(1000);
            }
            metroButton3.Enabled = true;
            listBox1.Enabled = true;
            listBox2.Enabled = true;
            JObject set = JObject.Parse(readjson);
            if (set["language"].ToString() == "Korean")
            {
                MessageBox.Show("타이머가 종료되었습니다.", "끝");
            }
            else if (set["language"].ToString() == "English")
            {
                MessageBox.Show("Time out", "Finish");
            }
        }

        private void MetroComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            JObject color = JObject.Parse(readjson);
            int a = metroComboBox2.SelectedIndex;
            switch(a)
            {
                case 0:
                    circularProgressBar1.ProgressColor = Color.Red;
                    color["color"] = "red";
                    break;
                case 1:
                    circularProgressBar1.ProgressColor = Color.Yellow;
                    color["color"] = "yellow";
                    break;
                case 2:
                    circularProgressBar1.ProgressColor = Color.Green;
                    color["color"] = "green";
                    break;
                case 3:
                    circularProgressBar1.ProgressColor = Color.Blue;
                    color["color"] = "blue";
                    break;
                case 4:
                    circularProgressBar1.ProgressColor = Color.Black;
                    color["color"] = "black";
                    break;
                case 5:
                    circularProgressBar1.ProgressColor = Color.White;
                    color["color"] = "white";
                    break;
            }
            File.WriteAllText(@"data\setting.json", color.ToString());
        }
        private void formresize(object sender, EventArgs e)
        {

        }
    }
}
