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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

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
        DataManager data = new DataManager("elso_proba.txt");
        bool forgatok { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int num1 = int.Parse(tb_num1.Text);
            int num2 = int.Parse(tb_num2.Text);
            sum = num1 + num2;

            if (sum != 7 && gridArrows[sum-2]!=null)
            {
                MovingCircle();
            }

            //g_trendline.Visibility = Visibility.Visible;
            if (gridArrows[sum - 2] == null &&sum!=7)
            {
                tb_honnan.IsEnabled = true;
                cb_forgatok.IsEnabled = true;
                tb_hova.Text = Convert.ToString(sum);
            }
            else if (sum == 7)
            {
                tb_honnan.IsEnabled = true;
                tb_hova.IsEnabled = true;
                cb_forgatok.IsEnabled = true;
            }

        }
        void MovingCircle()
        {
            
            string act_color = string.Empty;
            if (gridArrows[sum - 2].Color == "blue")
                act_color = "blue";
            else if (gridArrows[sum - 2].Color == "red")
                act_color = "red";
            else if (gridArrows[sum - 2].Color == "green")
                act_color = "green";
            else
                act_color = "yellow";
            int i = -1;
            do { i++; } while (!gridCircles[i].Color.Equals(act_color));
            if (gridCircles[i].Column==4 && !gridArrows[sum - 2].IsMirrored)
            {
                gridArrows[sum - 2].MirrorArrow();
            }
            else if(gridCircles[i].Column == 0 && gridArrows[sum - 2].IsMirrored)
            {
                gridArrows[sum - 2].MirrorArrow();
            }
            else if (gridArrows[sum - 2].IsMirrored)
            {
                gridCircles[i].Column--;
                MoveCircle(gridCircles[i]);
            }
            else
            {
                gridCircles[i].Column++;
                MoveCircle(gridCircles[i]);
            }
        }
        void MoveCircle(CircleImage image)
        {

            gr_profit.Children.Remove(image);
            Grid.SetColumn(image, image.Column);
            gr_profit.Children.Add(image);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int honnan = int.Parse(tb_honnan.Text);
            int hova;
            forgatok = false;
            if (cb_forgatok.IsChecked == true)
            {
                forgatok = true;
            }
            if (sum == 7)
            {
                hova = int.Parse(tb_hova.Text);

                MoveArrow(honnan, hova); //joker!!!
            }
            else if (gridArrows[sum - 2] == null)
            {
                MoveArrow(honnan, sum);
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
            gridArrows[data.Green1ArrowColumn] = new ArrowImage("garrow1", data.Green1ArrowColumn, "garrow.png", false, "green");
            gridArrows[data.Green2ArrowColumn] = new ArrowImage("garrow2", data.Green2ArrowColumn, "garrow.png", true, "green");
            gridArrows[data.Red1ArrowColumn] = new ArrowImage("rarrow1", data.Red1ArrowColumn, "rarrow.png", false, "red");
            gridArrows[data.Red2ArrowColumn] = new ArrowImage("rarrow2", data.Red2ArrowColumn, "rarrow.png", true, "red");
            gridArrows[data.Yellow1ArrowColumn] = new ArrowImage("yarrow1", data.Yellow1ArrowColumn, "yarrow.png", false, "yellow");
            gridArrows[data.Yellow2ArrowColumn] = new ArrowImage("yarrow2", data.Yellow2ArrowColumn, "yarrow.png", true, "yellow");
            gridArrows[data.Blue1ArrowColumn] = new ArrowImage("barrow1", data.Blue1ArrowColumn, "barrow.png", false, "blue");
            gridArrows[data.Blue2ArrowColumn] = new ArrowImage("barrow2", data.Blue2ArrowColumn, "barrow.png", true, "blue");
            for (int i = 0; i < gridArrows.Length; i++)
            {
                if (gridArrows[i] != null)
                {
                    SetupArrowImage(gridArrows[i]);
                }
            }
        }
        void PopulateGridCircles()
        {

            gridCircles[0] = new CircleImage("r_circle", "r_circle.png", "red", data.RedProfit_Column);
            gridCircles[1] = new CircleImage("b_circle", "b_circle.png", "blue", data.BlueProfit_Column);
            gridCircles[2] = new CircleImage("g_circle", "g_circle.png", "green", data.GreenProfit_Column);
            gridCircles[3] = new CircleImage("y_circle", "y_circle.png", "yellow", data.YellowProfit_Column);
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



    }
}
