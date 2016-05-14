﻿using System;
using System.Windows.Forms;

namespace null_num
{
    public partial class Form1 : Form
    {
        Form3 infoform = new Form3();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int val = Convert.ToInt32(numericUpDown1.Value);
            bool canStart = true;
            /*if (val == 1)
            {
                canStart = false;
                MessageBox.Show("1단계는 너무 쉬워요. 2단계부터 하시기 바랍니다.", "뷁", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }*/
            if (val == 7)
            {
                MessageBox.Show("럭키 세븐! 행운을 빌어요~", "올ㅋ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (canStart)
            {
                new Form2(val, Convert.ToInt32(numericUpDown2.Value), checkBox2.Checked).ShowDialog();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown2.Enabled = checkBox2.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            infoform = new Form3();
            infoform.StartPosition = FormStartPosition.CenterScreen;
            infoform.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
