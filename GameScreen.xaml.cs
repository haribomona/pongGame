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
        Rectangle player = new Rectangle();
        Player POne;
      //  public static  int MOVE_UP = 38; // arrow up
	  //  public static  int MOVE_DOWN = 40; // arrow down

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
            Canvas.SetLeft(player, POne.getX());
            Canvas.SetTop(player, POne.getY());
        }

        private void createPlayer(Canvas c)
        {

            POne = new Player(50,(int)c.ActualHeight/2-40);
            POne.setMin(0);
            POne.setMax((int)c.ActualHeight-80);
            player = new Rectangle();
            player.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            player.Width = 20;
            player.Height = 80;
            //player.Margin = new Thickness(50);
            //this.gameField.Children.Add(player);
            this.gameField.Children.Add(player);
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
            }

        }


    }

    class Player
    {

        int x = 0;
        int y = 0;
        int min = 0;
        int max = 0;

        int speed = 2;
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
