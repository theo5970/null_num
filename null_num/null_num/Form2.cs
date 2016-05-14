using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
// test commit
namespace null_num
{
    public partial class Form2 : Form
    {
        // 무슨 배열이 이렇게나 많을까
        ArrayList btnList = new ArrayList();
        ArrayList numList = new ArrayList();
        ArrayList locList = new ArrayList();
        // 한글 배열은 3개나 있고
        string[] lang_ko_hund = { "백", "이백", "삼백", "사백", "오백", "육백", "칠백", "팔백", "구백" };
        string[] lang_ko_ten = { "열", "스물", "서른", "마흔", "쉰", "예순", "일흔", "여든", "아흔" };
        string[] lang_ko_num = { "하나", "둘", "셋", "넷", "다섯", "여섯", "일곱", "여덟", "아홉" };
        // 여백
        int padding_x = 20;
        int padding_y = 20;
        // 최대 사이즈
        int max_sizex = 0;
        int max_sizey = 0;
        // 최대 글자 사이즈
        int max_font_size = 126;
        int count = 0;
        int count2 = 0;
        bool isAuto;
        Timer gameTimer;
        Random random = new Random();
        // 배열 초기화 후 랜덤으로 집어넣기
        private void init()
        {
            numList.Clear();
            locList.Clear();
            int now = 0;
            for (int i = 0; i < count2; i++)
            {
                if (numList.Count > 0)
                {
                    while (numList.Contains(now))
                    {
                        now = random.Next(count2) + 1;
                    }
                }
                else
                {
                    now = random.Next(count2) + 1;
                }
                numList.Add(now);
            }
            now = 0;
            for (int i = 0; i < count2; i++)
            {
                if (locList.Count > 0)
                {
                    while (locList.Contains(now))
                    {
                        now = random.Next(count2);
                    }
                }
                else
                {
                    now = random.Next(count2);
                }
                locList.Add(now);
            }
        }
        public Form2(int a, int t, bool auto)
        {
            InitializeComponent();
            max_sizex = this.Size.Width - (padding_x*3);
            max_sizey = this.Size.Height - (padding_y*3) - button1.Size.Height - 5;
            this.Text = "게임을 시작하지";
            count = a;
            count2 = count * count;
            isAuto = auto;
            if (!isAuto)
            {
                button1.Text = "다시하기";
            }
            int sizex = Convert.ToInt32(((float)max_sizex / count));
            int sizey = Convert.ToInt32(((float)max_sizey / count));
            for (int x=0;x<count;x++)
            {
                for (int y=0;y<count;y++)
                {
                    Button btn = new Button();
                    btn.Left = padding_x + (sizex * x);
                    btn.Top = padding_y + (sizey * y);
                    btn.Width = sizex;
                    btn.Height = sizey;
                    btn.Text = "";
                    btn.FlatStyle = FlatStyle.Popup;
                    btn.FlatAppearance.BorderColor = btn.BackColor = Color.LightCyan;
                    btn.Font = new Font("돋움", (float)(max_font_size / count), FontStyle.Bold);
                    if (!auto)
                    {
                        btn.Click += Btn_Click;
                    }
                    btnList.Add(btn);
                    this.Controls.Add(btn);
                }
            }
            init();
            if (auto)
            {
                gameTimer = new Timer();
                gameTimer.Interval = t * 1000;
                gameTimer.Tick += GameTimer_Tick;
            }
        }
        float p = 0;
        int index = 0;
        // 비자동모드
        private void Btn_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Text == string.Empty)
            {
                b.BackColor = Color.LightGreen;
                int k = (int)numList[index];
                int o = (int)locList[index];
                b.Text = (random.Next(3) == 0) ? int_to_korean(k) : k.ToString();
                index++;
                p = ((float)index / (float)count2) * 100;
                this.Text = String.Format("게임 진행률: {0:F2}%", p);
            }
        }
        // 숫자를 한국말로 바꾸기
        private string int_to_korean(int i)
        {
            string result = "";
            int hundred = (i / 100) % 10;
            if (hundred > 0)
                result += lang_ko_hund[hundred - 1];
            int ten = (i/10) % 10;
            if (ten>0)
                result += lang_ko_ten[ten - 1];
            int num = i % 10;
            if (num>0)
                result += lang_ko_num[num - 1];
            return result;
        }
        string cheer = "";
        Button currentButton;
        // 자동모드
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            // 진행률 퍼센트 구하기
            p = ((float)index / (float)(count2-1)) * 100;
            // 응원 메시지
            if (p >= 0 && p <= 30)
            {
                cheer = "아직 포기하면";
            }
            if (p >= 30 && p <= 50)
            {
                cheer = "어느정도 왔군";
            }
            if (p >= 50 && p <= 70)
            {
                cheer = "반을 넘다니";
            }
            if (p >= 70 && p <= 99)
            {
                cheer = "거의 다되감";
            }
            // 이스터에그: 창 타이틀
            this.Text = "게임은 시작됬다. 진행률: " + String.Format("{0:F2}",p) + "% - "+cheer;
            // 작동
            if (index < count2)
            {
                int k = (int)numList[index];
                int o = (int)locList[index];
                currentButton = (Button)btnList[o];
                currentButton.BackColor = Color.LightGreen;
                currentButton.Text = (random.Next(3) == 0) ? int_to_korean(k) : k.ToString();
                index++;
            } 
            // 게임 중단
            if (index >= count2)
            {
                this.Text = "게임 클리어. 굿잡!";
                button1.Text = "시작하기";
                index = 0;
                gameTimer.Stop();
            }
        }
        Button b;
        private void button1_Click(object sender, EventArgs e)
        {
            // 자동인지 확인
            if (isAuto)
            {
                if (gameTimer.Enabled)
                {
                    button1.Text = "시작하기";
                    // 배열 & 인덱스 넘버 초기화
                    index = 0;
                    init();
                    gameTimer.Stop();
                }
                else
                {
                    button1.Text = "정지하기";
                    gameTimer.Start();
                    // 버튼 색 & 내용 초기화
                    for (int i = 0; i < btnList.Count; i++)
                    {
                        b = (Button)btnList[i];
                        b.BackColor = Color.LightCyan;
                        b.Text = "";
                    }
                }
            } else
            {
                index = 0;
                // 배열 초기화
                init();
                // 버튼 색 & 내용 초기화
                for (int i = 0; i < btnList.Count; i++)
                {
                    b = (Button)btnList[i];
                    b.BackColor = Color.LightCyan;
                    b.Text = "";
                }
            }
        }
    }
}
