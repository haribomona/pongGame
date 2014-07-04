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
    public sealed partial class Level1 : Page
    {

        Rectangle playerOne = new Rectangle();
        Rectangle playerTwo = new Rectangle();
        Rectangle obstacle1 = new Rectangle();
        Rectangle obstacle2 = new Rectangle();
        Rectangle obstacle3 = new Rectangle();
        Ellipse ball = new Ellipse();
        Ball moving_ball;
        Player POne;
        Player PTwo;
        Obstacle obstacleOne;
        //Player obstacleTwo;
        //Player obstacleThree;

        bool PlOnemoveDown = false;
        bool PlOnemoveUp = false;


        int PlayerOnePoints = 0;
        int PlayerTwoPoints = 0;

        int height_rectangles = 140;
        int width_rectangles = 20;

        bool gameFlow = true;

        public Level1()
        {
            this.InitializeComponent();

            POne_up.AddHandler(PointerPressedEvent, new PointerEventHandler(this.POne_up_pointer_pressed), true);
            POne_up.AddHandler(PointerReleasedEvent, new PointerEventHandler(this.POne_up_pointer_released), true);
            POne_down.AddHandler(PointerPressedEvent, new PointerEventHandler(this.POne_down_pointer_pressed), true);
            POne_down.AddHandler(PointerReleasedEvent, new PointerEventHandler(this.POne_down_pointer_released), true);
        }

       

        private void StartGame(object sender, RoutedEventArgs e)
        {

            createGamefield(level1);
            moveBall();
            initGameLoop();
            
        }
        private void Back_To_Selection_Clicked(object sender, RoutedEventArgs e)
        {
            gameFlow = false;
            this.Frame.Navigate(typeof(MainPage));
        }


        private async void moveBall()
        {
            
            while (gameFlow == true)
            {
                await Task.Delay(30);

              /*  double currentBall_X = moving_ball.getX();
                double currentBall_Y = moving_ball.getY();
                double ballspeed_X = moving_ball.ballspeedX;
                double ballspeed_Y = moving_ball.ballspeedY;*/

                obstacleOne.checkCollision(moving_ball);

                moving_ball.move(level1,ball,POne,PTwo);
                //check if ball hits obstacle
                

                PlayerOne_Counter.Text = "" + PlayerOnePoints;
                PlayerTwo_Counter.Text = "" + PlayerTwoPoints;

                
                if (moving_ball.getX() < 0)
                {
                    PlayerOnePoints++;
                    PlayerOne_Counter.Text = "" + PlayerOnePoints;
                    moving_ball = new Ball(350, 250);

                }
                if (moving_ball.getX() > level1.Width - ball.Width)
                {
                    PlayerTwoPoints++;
                    PlayerTwo_Counter.Text = "" + PlayerTwoPoints;
                    moving_ball = new Ball(350, 250);

                }
                if (PlayerTwoPoints - 2 > PlayerOnePoints)
                {
                    gameFlow = false;
                    this.Frame.Navigate(typeof(LooseScreen));
                }
                if (PlayerOnePoints - 2 > PlayerTwoPoints)
                {
                    gameFlow = false;
                    this.Frame.Navigate(typeof(Level2));
                }
            }
           
        }



        private async void initGameLoop()
        {
            while(true){
                await Task.Delay(10);
              //  updateObstacle();
                update();
            }
        }

     /*   private void updateObstacle()
        {
            obstacleOne.moveObstacle();
        }
        */

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

          //  obstacle1.SetValue(Canvas.LeftProperty, 300);
          //  obstacle1.SetValue(Canvas.TopProperty, 300);

                 Canvas.SetLeft(obstacle1, obstacleOne.x);
                 Canvas.SetTop(obstacle1, obstacleOne.y);


            

            

            
        }

        private void calculateComputerPlayer()
        {
            double plTwo_Y = PTwo.getY() + (playerTwo.Height/ 2);

            
     /*       if (moving_ball.ballspeedX >0)
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
      * */
           
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
        //    int height_rectangles = 80;
          //  int width_rectangles = 20;

            POne = new Player((int)c.ActualWidth-(50 + width_rectangles), (int)c.ActualHeight / 2-60);
            POne.setMin(0);
            POne.setMax((int)c.ActualHeight - height_rectangles);
            playerOne = new Rectangle();
            playerOne.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0));
            playerOne.Width = width_rectangles;
            playerOne.Height = height_rectangles;

            // left player = player two
            PTwo = new Player(50,(int)c.ActualHeight/2-60);
            PTwo.setMin(0);
            PTwo.setMax((int)c.ActualHeight - height_rectangles);
            playerTwo = new Rectangle();
            playerTwo.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0));
            playerTwo.Width = width_rectangles;
            playerTwo.Height = height_rectangles;


            // draw obstacles
            obstacleOne = new Obstacle((int)c.ActualWidth / 2 - 10, (int)c.ActualHeight / 2 - 40);
            obstacleOne.min=0;
            obstacleOne.max=(int)c.ActualHeight - height_rectangles;
            obstacle1 = new Rectangle();
            obstacle1.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 123, 23, 123));
            obstacle1.Width = width_rectangles;
            obstacle1.Height = height_rectangles;

       //     Canvas.SetLeft(obstacle1, 300);
       //     Canvas.SetTop(obstacle1, 200);

       //     obstacle1.SetValue(Canvas.LeftProperty, 300);
       //     obstacle1.SetValue(Canvas.TopProperty, 300);

            this.level1.Children.Add(obstacle1);


            // draw ´both player to canvas
            this.level1.Children.Add(playerTwo);
            this.level1.Children.Add(playerOne);

            // creats and draws ball to canvas
            ball.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255,255,255,0));
            ball.Width = 20;
            ball.Height = 20;
          //  ball.SetValue(Canvas.LeftProperty, 250);
          //  ball.SetValue(Canvas.TopProperty, 150);

            moving_ball = new Ball(400,250);
            moving_ball.setMax(500-20);
            moving_ball.setMin(0);

            this.level1.Children.Add(ball);
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

        private void POne_up_pointer_pressed(object sender, PointerRoutedEventArgs e)
        {
            PlOnemoveUp = true; ;
        }

        private void POne_up_pointer_released(object sender, PointerRoutedEventArgs e)
        {
            PlOnemoveUp = false;
        }

        private void POne_down_pointer_pressed(object sender, PointerRoutedEventArgs e)
        {
            PlOnemoveDown = true;
        }

        private void POne_down_pointer_released(object sender, PointerRoutedEventArgs e)
        {
            PlOnemoveDown = false;
        }

    }

    
    }   

