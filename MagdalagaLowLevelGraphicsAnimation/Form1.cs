using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MagdalagaLowLevelGraphicsAnimation
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer timer;
        private List<Balloon> balloons;
        private List<Cloud> clouds;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true; 

            this.MaximumSize = new Size(900, 600);

            this.MaximizeBox = false;
            
            this.ClientSize = new Size(900, 600);

            balloons = new List<Balloon>
            {
                new Balloon(100, 400, Color.Red),
                new Balloon(200, 350, Color.Blue),
                new Balloon(300, 450, Color.Green),
                new Balloon(400, 420, Color.Orange),
                new Balloon(500, 380, Color.Purple),
                new Balloon(600, 430, Color.Yellow),
                new Balloon(700, 390, Color.Pink),
                new Balloon(800, 410, Color.Cyan)
            };

            clouds = new List<Cloud>
            {
                new Cloud(50, 100),
                new Cloud(200, 150),
                new Cloud(400, 80),
                new Cloud(600, 130),
                new Cloud(750, 90),
                new Cloud(100, 50),
                new Cloud(300, 20)
            };

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 20;
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle gradientRect = new Rectangle(0, 0, this.Width, this.Height);
            using (LinearGradientBrush gradientBrush = new LinearGradientBrush(gradientRect, Color.LightSkyBlue, Color.DeepSkyBlue, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(gradientBrush, gradientRect);
            }

            DrawSun(e.Graphics, 50, 50, 80);

            Point[] mountainPoints = {
                new Point(0, this.Height - 50),
                new Point(300, this.Height - 200),
                new Point(900, this.Height - 50)
            };
            e.Graphics.FillPolygon(Brushes.Tan, mountainPoints);


            foreach (Cloud cloud in clouds)
            {
                cloud.Draw(e.Graphics);
            }

            foreach (Balloon balloon in balloons)
            {
                balloon.Draw(e.Graphics);
            }
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (Balloon balloon in balloons)
            {
                balloon.Move();
            }

            foreach (Cloud cloud in clouds)
            {
                cloud.Move();
            }

            this.Invalidate();
        }

        private void DrawSun(Graphics g, int x, int y, int radius)
        {
            g.FillEllipse(Brushes.Yellow, x, y, radius * 2, radius * 2);

        }

        public class Cloud
        {
            private int x;
            private int y;

            public Cloud(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public void Move()
            {
                x += 1;

                if (x > 900)
                {
                    x = -100;
                }
            }

            public void Draw(Graphics g)
            {
                g.FillEllipse(Brushes.White, x, y, 50, 30);
                g.FillEllipse(Brushes.White, x + 20, y - 10, 60, 40);
                g.FillEllipse(Brushes.White, x + 40, y, 50, 30);
            }
        }

        public class Balloon
        {
            private int x;
            private int y;
            private Color color;
            private int speed = 2;

            public Balloon(int x, int y, Color color)
            {
                this.x = x;
                this.y = y;
                this.color = color;
            }

            public void Move()
            {
                y -= speed;

                if (y < -70)
                {
                    y = 600;
                }
            }

            public void Draw(Graphics g)
            {
                g.FillEllipse(new SolidBrush(color), x, y - 50, 50, 70); 
                g.FillEllipse(Brushes.Black, x + 20, y - -16, 10, 14);
                g.FillRectangle(Brushes.Black, x + 24, y + 20, 2, 60); 
            }
        }
    }
}
