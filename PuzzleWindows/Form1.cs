using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuzzleWindows
{
    public partial class Form1 : Form
    {
        // 변수 선언
        PuzzleGameEngine pge;
        List<Image> imgList = new List<Image>();
        private int puzzleSize = 16;
        int imgWidth = 100;
        int imgHeight = 100;
        private Font theFont;
        private Brush theBrush;
        private int theTick;
        private int theGameTick;
        private Pen thePen;

        public Form1()
        {
            InitializeComponent();

            // image를 가져옴
            // image를 list에 넣는다
            for(int i = 0; i < puzzleSize; i++)
            {
                // string fileName = "pic_" + (char)('a' + i) + ".png";
                string fileName = string.Format("./images/pic_{0}.png",(char)('a'+i)); // bin/debug에서 실행되므로 image폴더 또한 그 곳에 만들어줘야한다.

                Image tmpi = Image.FromFile(fileName);
                imgList.Add(tmpi);
            }

            // PuzzleGameEngine 생성
            pge = new PuzzleGameEngine();

            // Timer
            theFont = new Font("굴림", 15);
            theBrush = new SolidBrush(Color.Green);
            thePen = new Pen(Color.Red);
            theTick = 0;
            theGameTick = 0;
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Time 표시
            int time = 5 - ((theTick - theGameTick) / (1000 / timer1.Interval));
            string timeString = string.Format("Time : {0:D3}", time);
            e.Graphics.DrawString(timeString, theFont, theBrush, 0, 10);
            //e.Graphics.DrawRectangle(thePen, 0, 0, time, 10);
            e.Graphics.FillRectangle(theBrush, 0, 0, time, 10);

            // time이 0이면 GAME OVER
            if (time == -1)
            {
                timer1.Stop();
                MessageBox.Show("Game Over");
                return;
            }

            // 4 x 4로 puzzle 그리기

            // 내가 짠 소스
            /*
            int k = 0;

            for(int i = 0; i < 400; i += imgHeight)
            {
                for(int j=0; j< 400; j += imgWidth)
                {
                    e.Graphics.DrawImage(imgList[k], j, i, imgWidth, imgHeight);
                    k++;
                }
            }
            */

            // 선생님이 짠 소스
            for (int i=0; i<4; i++)
            {
                for(int j=0; j<4; j++)
                {
                    if(pge.getViewIndex(i + j * 4) != puzzleSize -1)
                    {
                      e.Graphics.DrawImage(imgList[pge.getViewIndex(i + j * 4)], 50 + i * imgWidth, 50 + j * imgHeight, imgWidth, imgHeight);
                    }
                }
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // OnClick Event : click한 index와 빈 칸 index 교체
            int tmpX = e.X;
            int tmpY = e.Y;

            if(!(50 <= tmpX && tmpX <= 4 * imgWidth * 50 && 50<=tmpY && tmpY <= 4 * imgHeight + 50))
            {
                return;
            } 

            tmpX -= 50;
            tmpY -= 50;

            tmpX /= imgWidth;
            tmpY /= imgHeight;

            int index = tmpX + tmpY * 4;

            //MessageBox.Show(index.ToString());

            pge.Change(index);
            Invalidate(); // 지금 있는 값은 완전치가 않아.

            // 다 맞췄을 경우, 창 닫음.
            if (pge.isEnd())
            {
                MessageBox.Show("축하합니다.");
                Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Tick : 외국사람들의 시간이 지나는 소리
            theTick++;
            Invalidate();
        }
    }
}
