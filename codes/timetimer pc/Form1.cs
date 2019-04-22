using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.IO;
using MetroFramework.Forms;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace timetimer_pc
{
    public partial class Form1 : Form
    {
        string readjson = File.ReadAllText(@"data\setting.json");
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
                metroButton1.Text = "시작 화면";
                metroButton2.Text = "설정";
                metroCheckBox1.Text = "디지털 타이머 표시";
                metroComboBox1.Text = "한글 (Korean)";
                label1.Text = "분";
                label2.Text = "초";
            }
            else if (set["language"].ToString() == "English")
            {
                metroButton1.Text = "Main page";
                metroButton2.Text = "Setting";
                metroCheckBox1.Text = "Show digital timer";
                metroComboBox1.Text = "영어 (English)";
                label1.Text = "M";
                label2.Text = "S";
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

        private void formsize(object sender, EventArgs e)
        {
            decimal 
        }
    }
}
