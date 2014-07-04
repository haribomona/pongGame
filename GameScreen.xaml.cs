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

        bool PlOnemoveDown = false;
        bool PlOnemoveUp = false;
        bool PlTwomoveDown = false;
        bool PlTwomoveUp = false;

        int PlayerOnePoints = 0;
        int PlayerTwoPoints = 0;

        public GameScreen()
        {
            this.InitializeComponent();
        }

       

        private void StartGame(object sender, RoutedEventArgs e)
        {

            createGamefield(gameField);
            moveBall();
            initGameLoop();
        }

        private async void moveBall()
        {

            while (true)
            {
                await Task.Delay(30);
                moving_ball.move(gameField,ball,POne,PTwo);
                PlayerOne_Counter.Text = "" + PlayerOnePoints;
                PlayerTwo_Counter.Text = "" + PlayerTwoPoints;
                if (moving_ball.getX() < 0)
                {
                    PlayerOnePoints++;
                    PlayerOne_Counter.Text = "" + PlayerOnePoints;
                    moving_ball = new Ball(350, 250);

                }
                if (moving_ball.getX() > gameField.Width - ball.Width)
                {
                    PlayerTwoPoints++;
                    PlayerTwo_Counter.Text = "" + PlayerTwoPoints;
                    moving_ball = new Ball(350, 250);

                }
            }
           
        }

        private async void initGameLoop()
        {
            while(true){
                await Task.Delay(10);
                update();
            }
        }

        private void update()
        {
            if (PlOnemoveDown)
            {
                POne.moveDown();
            }
            if (PlOnemoveUp)
            {
                POne.moveUp();
            }
            if (PlTwomoveDown)
            {
                PTwo.moveDown();
            }
            if (PlTwomoveUp)
            {
                PTwo.moveUp();
            }

            Canvas.SetLeft(playerOne, POne.getX());
            Canvas.SetTop(playerOne, POne.getY());
            Canvas.SetLeft(playerTwo, PTwo.getX());
            Canvas.SetTop(playerTwo, PTwo.getY());

            

            

            
        }

        private void createGamefield(Canvas c)
        {

          

            // right player = player one
            int height_rectangles = 120;
            int width_rectangles = 20;

            POne = new Player((int)c.ActualWidth-(50 + width_rectangles), (int)c.ActualHeight / 2-60);
            POne.setMin(0);
            POne.setMax((int)c.ActualHeight - height_rectangles);
            playerOne = new Rectangle();
            playerOne.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            playerOne.Width = width_rectangles;
            playerOne.Height = height_rectangles;

            // left player = player two
            PTwo = new Player(50,(int)c.ActualHeight/2-60);
            PTwo.setMin(0);
            PTwo.setMax((int)c.ActualHeight - height_rectangles);
            playerTwo = new Rectangle();
            playerTwo.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            playerTwo.Width = width_rectangles;
            playerTwo.Height = height_rectangles;

            // draw ´both player to canvas
            this.gameField.Children.Add(playerTwo);
            this.gameField.Children.Add(playerOne);

            // creats and draws ball to canvas
            ball.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255,255,255,0));
            ball.Width = 20;
            ball.Height = 20;
          //  ball.SetValue(Canvas.LeftProperty, 250);
          //  ball.SetValue(Canvas.TopProperty, 150);

            moving_ball = new Ball(350,250);
            moving_ball.setMax(500-20);
            moving_ball.setMin(0);

            this.gameField.Children.Add(ball);
        }

        
        private void Key_Down(object sender, KeyRoutedEventArgs e)
        {

            switch (e.Key)  
            {

                case Windows.System.VirtualKey.Down:
                    PlOnemoveDown = true;
                    break;

                case Windows.System.VirtualKey.Up:
                    PlOnemoveUp = true;
                    break;

                case Windows.System.VirtualKey.Y:
                    PlTwomoveDown = true;
                    break;

                case Windows.System.VirtualKey.X:
                    PlTwomoveUp = true;
                    break;
            }

        }

        private void Key_up(object sender, KeyRoutedEventArgs e)
        {
            switch (e.Key)
            {

                case Windows.System.VirtualKey.Down:
                    PlOnemoveDown = false;
                    break;

                case Windows.System.VirtualKey.Up:
                    PlOnemoveUp = false;
                    break;

                case Windows.System.VirtualKey.Y:
                    PlTwomoveDown = false;
                    break;

                case Windows.System.VirtualKey.X:
                    PlTwomoveUp = false;
                    break;
            }
        }

        

    }

    class Ball
    {


        double x = 0;
        double y = 0;
        int min = 0;
        int max = 0;
        public double ballspeedX = 4;
        public double ballspeedY = 9;
        const double ballspeed = 15;
        int LastHitWIthPaddle = 0;


        public Ball(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

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

    
        public void move(Canvas gameField,Ellipse ball,Player POne,Player PTwo)
        {
 	      
            int height_canvas = 500;
            int width_canvas = 700;
            int height_rectangles = 120;

            LastHitWIthPaddle++;

            
                this.x += ballspeedX;
                this.y += ballspeedY;
                Canvas.SetLeft(ball, getX());
                Canvas.SetTop(ball, getY());

                if (this.y < 0 || this.y > height_canvas - ball.Height)
                {
                    ballspeedY *= -1;
                }

                if (this.x < 0 || this.x > width_canvas - ball.Width)
                {
                    ballspeedX *= -1;
                }
       
            var playerOne = POne.getY();
            var playerTwo = PTwo.getY();

            // POne = rechter Spieler!!!
            // PTwo = linker Spieler!!!!
            // Deshalb <80 & >420 vertauschen!!!

            if(LastHitWIthPaddle < 10){
                return;
            }

            if (this.x > POne.getX()+20 || this.x < PTwo.getX())
            {

                return;
            }
            
            double distanceToPaddleOneMiddle = this.y+10 - (POne.getY() + (height_rectangles/2));
            double distanceToPaddleTwoMiddle = this.y+10 - (PTwo.getY() + (height_rectangles/2));

            if (this.y < POne.getY()+height_rectangles && this.y+20 > POne.getY())
            {
                var x = ballspeedY <0 ? this.x : this.x + 20;

                if(x >610 && x<630){
                    
                   ballspeedY *= (distanceToPaddleOneMiddle / 100);
                   System.Diagnostics.Debug.WriteLine(ballspeedY);
                   this.ballspeedX = ballspeed - Math.Abs(ballspeedY);
                   this.ballspeedX = ballspeedX > 0 ? ballspeedX : ballspeedX * -1;
                   var tempballspeedX = ballspeed + Math.Abs(ballspeedX);
                   ballspeedX *= -1;
                 
                    
                    LastHitWIthPaddle = 0;
                }
            }

             if (this.y < PTwo.getY()+height_rectangles && this.y+20 > PTwo.getY()){


                 if(x<70 && x>50){

                     ballspeedY *= (distanceToPaddleTwoMiddle / 100);
                     //var tempspeedX = ballspeed + Math.Abs(ballspeedY);
                     this.ballspeedX = ballspeed - Math.Abs(ballspeedY);
                     this.ballspeedX = ballspeedX > 0 ? ballspeedX : ballspeedX * -1;
                    // System.Diagnostics.Debug.WriteLine(Math.Abs(ballspeedY));
                     
                     LastHitWIthPaddle = 0;
                 }
             }

           
            
            
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
