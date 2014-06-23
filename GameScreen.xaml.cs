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
        Player POne;
        Player PTwo;

        public GameScreen()
        {
            this.InitializeComponent();
            createPlayer(gameField);

            initGameLoop();
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

        private void createPlayer(Canvas c)
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
