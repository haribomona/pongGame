﻿using System;
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
    public sealed partial class ComputerScreen : Page
    {
        Rectangle playerOne = new Rectangle();
        Rectangle playerTwo = new Rectangle();
        Ellipse ball = new Ellipse();
        Ball move_ball;
        Player POne;
        Player PTwo;

        bool PlOnemoveDown = false;
        bool PlOnemoveUp = false;
        bool PlTwomoveDown = false;
        bool PlTwomoveUp = false;

        int PlayerOnePoints = 0;
        int PlayerTwoPoints = 0;


        public ComputerScreen()
        {
            this.InitializeComponent();
        }
        private void StartGame(object sender, RoutedEventArgs e)
        {

            createGamefield(computerfield);
            moveBall();
            initGameLoop();
        }

        private async void moveBall()
        {

            while (true)
            {
                await Task.Delay(30);
                move_ball.moveIt(computerfield,ball,POne,PTwo);
                PlayerOne_Counter.Text = "" + PlayerOnePoints;
                PlayerTwo_Counter.Text = "" + PlayerTwoPoints;
                if (move_ball.getThatX() < 0)
                {
                    PlayerOnePoints++;
                    PlayerOne_Counter.Text = "" + PlayerOnePoints;
                    move_ball = new Ball(350, 250);

                }
                if (move_ball.getThatX() > computerfield.Width - ball.Width)
                {
                    PlayerTwoPoints++;
                    PlayerTwo_Counter.Text = "" + PlayerTwoPoints;
                    move_ball = new Ball(350, 250);

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
                POne.moveDownwards();
            }
            if (PlOnemoveUp)
            {
                POne.moveUpwards();
            }
            if (PlTwomoveDown)
            {
                PTwo.moveDownwards();
            }
            if (PlTwomoveUp)
            {
                PTwo.moveUpwards();
            }

            Canvas.SetLeft(playerOne, POne.getThatX());
            Canvas.SetTop(playerOne, POne.getThatY());
            Canvas.SetLeft(playerTwo, PTwo.getThatX());
            Canvas.SetTop(playerTwo, PTwo.getThatY());

            

            

            
        }

        private void createGamefield(Canvas c)
        {

          

            // right player = player one
            int height_rectangles = 120;

            POne = new Player((int)c.ActualWidth-50, (int)c.ActualHeight / 2-60);
            POne.setMin(0);
            POne.setMax((int)c.ActualHeight - height_rectangles);
            playerOne = new Rectangle();
            playerOne.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            playerOne.Width = 20;
            playerOne.Height = height_rectangles;

            // left player = player two
            PTwo = new Player(50,(int)c.ActualHeight/2-60);
            PTwo.setMin(0);
            PTwo.setMax((int)c.ActualHeight - height_rectangles);
            playerTwo = new Rectangle();
            playerTwo.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            playerTwo.Width = 20;
            playerTwo.Height = height_rectangles;

            // draw ´both player to canvas
            this.computerfield.Children.Add(playerTwo);
            this.computerfield.Children.Add(playerOne);

            // creats and draws ball to canvas
            ball.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255,255,255,0));
            ball.Width = 20;
            ball.Height = 20;
          //  ball.SetValue(Canvas.LeftProperty, 250);
          //  ball.SetValue(Canvas.TopProperty, 150);

            move_ball = new Ball(350,250);
            move_ball.setThatMax(500-20);
            move_ball.setThatMin(0);

            this.computerfield.Children.Add(ball);
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

        internal void setThatMax(int max)
        {
            this.max = max;
        }

        internal void setThatMin(int p)
        {
            this.min = p;
        }

        internal double getY()
        {
            return this.y;
        }

        internal double getThatX()
        {
            return this.x;
        }

    
        public void moveIt(Canvas gameField,Ellipse ball,Player POne,Player PTwo)
        {
 	        int ballspeed = 4;
            int height_canvas = 500;
            int width_canvas = 700;
            int height_rectangles = 120;
            

            if (ballmoveright==true)
            {
                this.x += ballspeed;
                Canvas.SetLeft(ball, getThatX());
                Canvas.SetTop(ball, getY());

            }
            else
            {
                this.x -= ballspeed;
                Canvas.SetLeft(ball, getThatX());
                Canvas.SetTop(ball, getY());
            }
            if (ballmoveup == true)
            {
                this.y -= ballspeed;
                Canvas.SetLeft(ball, getThatX());
                Canvas.SetTop(ball, getY());
            }
            else
            {
                this.y += ballspeed;
                Canvas.SetLeft(ball, getThatX());
                Canvas.SetTop(ball, getY());
            }


            if (this.y < 0) ballmoveup = false;
            if (this.y > height_canvas - ball.Height) ballmoveup = true;

            if (this.x < 0) ballmoveright = true;
            if (this.x > width_canvas - ball.Width) ballmoveright = false;



            var playerOne = POne.getThatY();
            var playerTwo = PTwo.getThatY();

            // POne = rechter Spieler!!!
            // PTwo = linker Spieler!!!!
            // Deshalb <80 & >420 vertauschen!!!

            if (this.y < POne.getThatY()+height_rectangles && this.y > POne.getThatY() && this.x> 630) ballmoveright= false;
            if (this.y < PTwo.getThatY()+height_rectangles && this.y > PTwo.getThatY() && this.x < 70) ballmoveright = true;
            
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

        public void moveUpwards()
        { 
            this.y -= this.speed;
            if(this.y < this.min){
                this.y = this.min;
            }
        }

        public void moveDownwards()
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

        public int getThatX()
        {
            return this.x;
        }

        public int getThatY()
        {
            return this.y;
        }
    }

    }
}