using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace Pong
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Options : Page
    {
        public Options()
        {
            this.InitializeComponent();
        }


        private void _1_gegen_Computer_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CompScreen));
        }

        private void Level_choosen(object sender, RoutedEventArgs e)
        {
            CompScreen computer = new CompScreen();
            int level;

            if (One.IsSelected == true)
            {
                level = 200;
                computer.calculateX(level);
            }
            if (Two.IsSelected == true)
            {
                level = 300;
                computer.calculateX(level);
            }
            if (Three.IsSelected == true)
            {
                level = 400;
                computer.calculateX(level);
            }
            if (Four.IsSelected == true)
            {
                level = 500;
                computer.calculateX(level);
            }

            this.Frame.Navigate(typeof(CompScreen));
        }
        
    }
}
