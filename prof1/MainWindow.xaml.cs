using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace prof1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //g_trendline.Visibility = Visibility.Collapsed;
            tb_honnan.IsEnabled = false;
            tb_hova.IsEnabled = false;
            cb_forgatok.IsEnabled = false;
            PopulateGridArrows();
            PopulateGridCircles();
        }
        public int sum { get; set; }
        ArrowImage[] gridArrows = new ArrowImage[11];
        CircleImage[] gridCircles = new CircleImage[4];
        bool forgatok { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int num1 = int.Parse(tb_num1.Text);
            int num2 = int.Parse(tb_num2.Text);
            sum = num1 + num2;
            
            //g_trendline.Visibility = Visibility.Visible;
            if (gridArrows[sum-2]==null)
            {
                tb_honnan.IsEnabled=true;
                cb_forgatok.IsEnabled=true;
                tb_hova.Text = Convert.ToString(sum);
            }
            else if (sum==7)
            {
                tb_honnan.IsEnabled = true;
                tb_hova.IsEnabled=true;
                cb_forgatok.IsEnabled = true;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int honnan = int.Parse(tb_honnan.Text);
            int hova;
            forgatok = false;
            if(cb_forgatok.IsChecked == true) 
            { 
                forgatok = true;
            }
            if (sum == 7)
            {
                hova = int.Parse(tb_hova.Text);

                MoveArrow(honnan, hova); //joker!!!
            }
            else if (gridArrows[sum-2]==null)
            {
                MoveArrow(honnan,sum);
            }
        }
        private void Button_Click_ujkor(object sender, RoutedEventArgs e)
        {
            tb_num1.Text = string.Empty;
            tb_num2.Text = string.Empty;
            tb_honnan.Text = string.Empty;
            tb_hova.Text = string.Empty;
            cb_forgatok.IsChecked = false;
            tb_honnan.IsEnabled = false;
            tb_hova.IsEnabled = false;
            cb_forgatok.IsEnabled = false;
            //g_trendline.Visibility = Visibility.Collapsed;
        }
        void PopulateGridArrows()
        {
            
            gridArrows[1] = new ArrowImage("garrow1", 1, "garrow.png", false, "green");
            gridArrows[2] = new ArrowImage ("rarrow1", 2, "rarrow.png", false,"red");
            gridArrows[3] = new ArrowImage("yarrow1", 3, "yarrow.png", false, "yellow");
            gridArrows[4] = new ArrowImage("barrow1", 4, "barrow.png", false, "blue");
            gridArrows[6] = new ArrowImage( "barrow2", 6, "barrow.png", true,"blue");
            gridArrows[7] = new ArrowImage( "garrow2", 7, "garrow.png", true, "green");
            gridArrows[8] = new ArrowImage( "rarrow2", 8, "rarrow.png",true,"red");
            gridArrows[9] = new ArrowImage("yarrow2", 9, "yarrow.png", true, "yellow");
            for (int i = 0; i < gridArrows.Length; i++)
            {
                if (gridArrows[i]!=null)
                {
                    SetupArrowImage(gridArrows[i]);
                }
            }
        }
        void PopulateGridCircles()
        {
            
            gridCircles[0] = new CircleImage("r_circle", "r_circle.png", "red",2);
            gridCircles[1] = new CircleImage("b_circle", "b_circle.png", "blue", 2);
            gridCircles[2] = new CircleImage("g_circle", "g_circle.png", "green", 2);
            gridCircles[3] = new CircleImage("y_circle", "y_circle.png", "yellow", 2);
            for (int i = 0; i < gridCircles.Length; i++)
            {
                newCircleImage(gridCircles[i]);
            }
        }
        void SetupArrowImage(ArrowImage image)
        {
            Grid.SetColumn(image, image.Column);
            Grid.SetRow(image, 1);
            if (image.IsMirrored)
            {
                ScaleTransform scaleTransform = new ScaleTransform(-1, 1);
                image.RenderTransformOrigin = new Point(0.5, 0.5);
                image.RenderTransform = scaleTransform;
            }
            gr_trendlinetable.Children.Add(image);
        }
        void newCircleImage(CircleImage image)
        {
            Grid.SetColumn(image, image.Column);
            switch (image.Color)
            {
                case "red":
                    Grid.SetRow(image, 5);
                    break;
                case "blue":
                    Grid.SetRow(image, 8); break;
                case "green":
                    Grid.SetRow(image, 7); break;
                case "yellow":
                    Grid.SetRow(image, 6); break;
            }
            gr_profit.Children.Add(image);
        }
        void MoveArrow(int sourceColumn, int destinationColumn)
        {
            sourceColumn = sourceColumn - 2;
            destinationColumn = destinationColumn - 2;
            ArrowImage sourceImage = gridArrows[sourceColumn];
            if (forgatok)
            {
                sourceImage.MirrorArrow();
            }

            gr_trendlinetable.Children.Remove(gridArrows[sourceColumn]);
            Grid.SetColumn(sourceImage, destinationColumn);
            gr_trendlinetable.Children.Add(sourceImage);
            gridArrows[sourceColumn] = null;
            gridArrows[destinationColumn] = sourceImage;
        }
        //void MoveCircle(int sourceColumn, int destinationColumn)
        //{
        //    gr_profit.Children.Remove(gridArrows[sourceColumn]);
        //    Grid.SetColumn(sourceImage, destinationColumn);
        //    gr_profit.Children.Add(sourceImage);
        //    gridArrows[sourceColumn] = null;
        //    gridArrows[destinationColumn] = sourceImage;
        //}
   
       
    }
}
