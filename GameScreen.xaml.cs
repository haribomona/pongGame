using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace Pong
{
    
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class GameScreen : Page
    {
        Rectangle playerOne = new Rectangle();
        Rectangle playerTwo = new Rectangle();
        Ellipse ball = new Ellipse();
        Ball moving_ball;
        Player POne;
        Player PTwo;

        public GameScreen()
        {
            this.InitializeComponent();
            createGamefield(gameField);


            moveBall();
            initGameLoop();

            
        }

        private async void moveBall()
        {
            bool ballmovesright = true;


            while (true)
            {
                await Task.Delay(30);
                moving_ball.move(gameField, ball,ballmovesright);
            }
           
        }

        private async void initGameLoop()
        {
            while(true){
                await Task.Delay(40);
                update();
            }
        }

        private void update()
        {
            Canvas.SetLeft(playerOne, POne.getX());
            Canvas.SetTop(playerOne, POne.getY());
            Canvas.SetLeft(playerTwo, PTwo.getX());
            Canvas.SetTop(playerTwo, PTwo.getY());

            

            

            
        }

        private void createGamefield(Canvas c)
        {
            // right player = player one

            POne = new Player(430, (int)c.ActualHeight / 2 - 40);
            POne.setMin(0);
            POne.setMax((int)c.ActualHeight - 80);
            playerOne = new Rectangle();
            playerOne.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            playerOne.Width = 20;
            playerOne.Height = 80;

            // left player = player two
            PTwo = new Player(50,(int)c.ActualHeight/2-40);
            PTwo.setMin(0);
            PTwo.setMax((int)c.ActualHeight - 80);
            playerTwo = new Rectangle();
            playerTwo.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            playerTwo.Width = 20;
            playerTwo.Height = 80;

            // draw ´both player to canvas
            this.gameField.Children.Add(playerTwo);
            this.gameField.Children.Add(playerOne);

            // creats and draws ball to canvas
            ball.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255,0,255,255));
            ball.Width = 20;
            ball.Height = 20;
          //  ball.SetValue(Canvas.LeftProperty, 250);
          //  ball.SetValue(Canvas.TopProperty, 150);

            moving_ball = new Ball(250,150);
            moving_ball.setMax(500-20);
            moving_ball.setMin(0);

            this.gameField.Children.Add(ball);
        }

        
        private void Key_Down(object sender, KeyRoutedEventArgs e)
        {

            switch (e.Key)  
            {

                case Windows.System.VirtualKey.Down:
                    POne.moveDown();
                    break;

                case Windows.System.VirtualKey.Up:
                    POne.moveUp();
                    break;

                case Windows.System.VirtualKey.Y:
                    PTwo.moveUp();
                    break;

                case Windows.System.VirtualKey.X:
                    PTwo.moveDown();
                    break;
            }

        }


    }

    class Ball
    {
        int x = 0;
        int y = 0;
        int min = 0;
        int max = 0;
        bool ballmoveup = false;
        bool ballmoveright = true;

        public Ball(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

       /* public void move()
        {

            while (this.x < this.max)
            {
                this.x += 1;
            }
         /*   this.x += 30;
            if (this.x < this.max)
            {
                this.x = this.max;
            }
            else if(this.x >= this.max){
                this.x -=1;
            }*/
   //     }

        internal void setMax(int max)
        {
            this.max = max;
        }

        internal void setMin(int p)
        {
            this.min = p;
        }

        internal double getY()
        {
            return this.y;
        }

        internal double getX()
        {
            return this.x;
        }

        

        internal void move(Canvas gameField, Ellipse ball, bool ballmovesright)
        {

            int ballspeed = 10;

            if (ballmoveright==true)
            {
                this.x += ballspeed;
                Canvas.SetLeft(ball, getX());
                Canvas.SetTop(ball, getY());

            }
            else
            {
                this.x -= ballspeed;
                Canvas.SetLeft(ball, getX());
                Canvas.SetTop(ball, getY());
            }
            if (ballmoveup == true)
            {
                this.y -= ballspeed;
                Canvas.SetLeft(ball, getX());
                Canvas.SetTop(ball, getY());
            }
            else
            {
                this.y += ballspeed;
                Canvas.SetLeft(ball, getX());
                Canvas.SetTop(ball, getY());
            }

            if (this.x <0) ballmoveright = true;
            if (this.x > 500-ball.Width) ballmoveright = false;

            if (this.y < 0) ballmoveup = false;
            if (this.y > 300-ball.Height) ballmoveup = true;
            
        }

        }
    class Player
    {

        int x = 0;
        int y = 0;
        int min = 0;
        int max = 0;

        int speed = 4;
        public Player(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void moveUp()
        { 
            this.y -= this.speed;
            if(this.y < this.min){
                this.y = this.min;
            }
        }

        public void moveDown()
        {
            this.y += this.speed;
            if (this.y > this.max)
            {
                this.y = this.max;
            }
           // this.y = this.y + this.speed > this.max ? this.max : this.y + this.speed;
        }

        public void setMax(int max)
        {
            this.max = max;
        }

        public void setMin(int min)
        {
            this.min = min;
        }

        public void increaseSpeed()
        {
            this.speed += 1;
        }

        public void decreaseSpeed()
        {
            this.speed -= 1;
        }

        public int getX()
        {
            return this.x;
        }

        public int getY()
        {
            return this.y;
        }
    }

}
