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
    public sealed partial class CompScreen : Page
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

        public CompScreen()
        {
            this.InitializeComponent();
        }

       

        private void StartGame(object sender, RoutedEventArgs e)
        {

            createGamefield(playField);
            moveBall();
            initGameLoop();
        }

        private async void moveBall()
        {

            while (true)
            {
                await Task.Delay(30);
                moving_ball.move(playField,ball,POne,PTwo);
                PlayerOne_Counter.Text = "" + PlayerOnePoints;
                PlayerTwo_Counter.Text = "" + PlayerTwoPoints;
                if (moving_ball.getX() < 0)
                {
                    PlayerOnePoints++;
                    PlayerOne_Counter.Text = "" + PlayerOnePoints;
                    moving_ball = new Ball(350, 250);

                }
                if (moving_ball.getX() > playField.Width - ball.Width)
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
            /*
            if (PlTwomoveDown)
            {
                PTwo.moveDown();
            }
            if (PlTwomoveUp)
            {
                PTwo.moveUp();
            }
            */

            calculateComputerPlayer();

            Canvas.SetLeft(playerOne, POne.getX());
            Canvas.SetTop(playerOne, POne.getY());
            Canvas.SetLeft(playerTwo, PTwo.getX());
            Canvas.SetTop(playerTwo, PTwo.getY());

            

            

            
        }

        private void calculateComputerPlayer()
        {
            double plTwo_Y = PTwo.getY() + (playerTwo.Height/ 2);

            
            if (moving_ball.ballspeedX >0)
            {
                if (plTwo_Y > 250)
                {
                    PTwo.moveUp();
                }
                else
                {
                    PTwo.moveDown();
                }
            }
           
            if (moving_ball.ballspeedX <0)
            {
                if (moving_ball.getY() != PTwo.getY())
                {
                    if (moving_ball.getY() < plTwo_Y)
                    {
                        PTwo.moveUp();
                    }
                    else if (moving_ball.getY() > PTwo.getY())
                    {
                        PTwo.moveDown();
                    }
                }
            }
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
            this.playField.Children.Add(playerTwo);
            this.playField.Children.Add(playerOne);

            // creats and draws ball to canvas
            ball.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255,255,255,0));
            ball.Width = 20;
            ball.Height = 20;
          //  ball.SetValue(Canvas.LeftProperty, 250);
          //  ball.SetValue(Canvas.TopProperty, 150);

            moving_ball = new Ball(350,250);
            moving_ball.setMax(500-20);
            moving_ball.setMin(0);

            this.playField.Children.Add(ball);
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

             /*   case Windows.System.VirtualKey.Y:
                    PlTwomoveDown = true;
                    break;

                case Windows.System.VirtualKey.X:
                    PlTwomoveUp = true;
                    break;
              * */
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
/*
                case Windows.System.VirtualKey.Y:
                    PlTwomoveDown = false;
                    break;

                case Windows.System.VirtualKey.X:
                    PlTwomoveUp = false;
                    break;
 * */
            }
        }

        

    }

    

}
