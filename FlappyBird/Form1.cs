using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class gameWindow : Form
    {
        int basePipeSpeed = 8;
        int pipeSpeed = 8;
        int cloudSpeed = 4;
        int gravity = 6;
        int fall = 6;
        int rise = -10;
        int score = 0;
        Random random = new Random();
        int gameOver = 0;
        int flap = 0;
        

        public gameWindow()
        {
            InitializeComponent();
        }


        private void gameTimerEvent(object sender, EventArgs e)
        {
            flappyBird.Top += gravity;
            pipeBtm.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            cloud.Left -= cloudSpeed;
            scoreText.Text = "Score: " + score.ToString();
            int speedIncrease = (int)score / 3;
            pipeSpeed = basePipeSpeed + speedIncrease ;

            if(cloud.Left < cloud.Width * (-1))
            {
                cloud.Left = this.Width + 100;
                cloud.Top = random.Next(cloud.Height, Convert.ToInt32(this.Height / 2));
            }
            
            if(pipeBtm.Left < pipeBtm.Width*(-1))
            {
                int pipeBtmTopLimit = Convert.ToInt32(this.Height*0.7);
                int pipeBtmNewTop = random.Next(ground.Height + 50,pipeBtmTopLimit);
                int pipeChange = pipeBtm.Top - pipeBtmNewTop;
                pipeBtm.Top = pipeBtmNewTop;

                pipeTop.Top -= pipeChange;

                pipeBtm.Left = this.Width;
                pipeTop.Left = this.Width;
                score++;
            }
            if(flappyBird.Bounds.IntersectsWith(pipeBtm.Bounds) ||
               flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) ||
               flappyBird.Bounds.IntersectsWith(ground.Bounds)  ||
               flappyBird.Top < flappyBird.Height*(-1)
               )
            {
                endGame();
            }
        }

        private void gameKeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {
                if (gameOver == 1)
                {
                    pipeBtm.Left = Convert.ToInt32(this.Width * 0.8);
                    pipeTop.Left = Convert.ToInt32(this.Width * 0.8);
                    flappyBird.Top = Convert.ToInt32(0.5 * (this.Height - ground.Height));
                    score = 0;
                    gameOver = 0;
                    endGameText.Visible = false;
                    gameTimer.Start();
                }
                
                gravity = rise;

                if(flap == 0)
                {
                    flappyBird.Image = FlappyBird.Properties.Resources.bird_flap;
                    flap = 1;
                }
            }
        }

        private void gameKeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity =fall;
                if (flap == 1)
                {
                    flappyBird.Image = FlappyBird.Properties.Resources.bird;
                    flap = 0;
                }
                
            }
        }
        private void endGame()
        {
            gameTimer.Stop();
            endGameText.Visible = true;
            endGameText.Text = "Final score: " + score;
            scoreText.Text = "Game over!";
            gameOver = 1;
        }
    }
}
