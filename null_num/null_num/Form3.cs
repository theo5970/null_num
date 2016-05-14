using System;
using System.Windows.Forms;

namespace null_num
{
    public partial class Form3 : Form
    {
        Timer exitTimer = new Timer();
        float countdown = 10;
        public Form3()
        {
            InitializeComponent();
            // 20 밀리초를 주기.
            exitTimer.Interval = 20;
            exitTimer.Tick += ExitTimer_Tick;
            exitTimer.Start();
        }

        private void ExitTimer_Tick(object sender, EventArgs e)
        {
            label2.Text = String.Format("{0}초 후에 창이 사라집니다.", Convert.ToInt32(countdown));
            // 사라지는 효과
            this.Opacity = (countdown * 0.1) + 0.3;
            this.Left -= 1;
            countdown-= 0.03f;
            if (countdown < 0)
            {
                this.Close();
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            exitTimer.Stop();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            exitTimer.Stop();
            this.Close();
        }
    }
}
