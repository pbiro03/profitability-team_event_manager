using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tb_honnan.IsEnabled = false;
            tb_hova.IsEnabled = false;
            cb_forgatok.IsEnabled = false;
            PopulateGridArrows();
            PopulateGridCircles();
        }

        public int sum { get; set; }
        ArrowImage[] gridArrows = new ArrowImage[12];
        CircleImage[] gridCircles = new CircleImage[4];
        DataManager data = new DataManager("elso_proba.txt");
        bool forgatok { get; set; }
        void MovingCircle()
        {

            string act_color = string.Empty;
            if (gridArrows[sum - 1].Color == "blue")
                act_color = "blue";
            else if (gridArrows[sum - 1].Color == "red")
                act_color = "red";
            else if (gridArrows[sum - 1].Color == "green")
                act_color = "green";
            else
                act_color = "yellow";
            int i = -1;
            do { i++; } while (!gridCircles[i].Color.Equals(act_color));
            if (gridCircles[i].Column == 4 && !gridArrows[sum - 1].IsMirrored)
            {
                gridArrows[sum - 1].MirrorArrow();
            }
            else if (gridCircles[i].Column == 0 && gridArrows[sum - 1].IsMirrored)
            {
                gridArrows[sum - 1].MirrorArrow();
            }
            else if (gridArrows[sum - 1].IsMirrored)
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
            if (image.Color.Equals("red") || image.Color.Equals("blue"))
            {
                gr_profit.Children.Remove(image);
                Grid.SetColumn(image, image.Column);
                gr_profit.Children.Add(image);
            }
            else
            {
                gr_profit1.Children.Remove(image);
                Grid.SetColumn(image, image.Column);
                gr_profit1.Children.Add(image);
            }

        }


        //private void Button_Click_ujkor(object sender, RoutedEventArgs e)
        //{
        //    tb_honnan.Text = string.Empty;
        //    tb_hova.Text = string.Empty;
        //    cb_forgatok.IsChecked = false;
        //    tb_honnan.IsEnabled = false;
        //    tb_hova.IsEnabled = false;
        //    cb_forgatok.IsEnabled = false;
        //}
        void PopulateGridArrows()
        {
            gridArrows[data.Green1ArrowColumn] = new ArrowImage("garrow1", data.Green1ArrowColumn, "g_arrow.png", false, "green");
            gridArrows[data.Green2ArrowColumn] = new ArrowImage("garrow2", data.Green2ArrowColumn, "g_arrow.png", true, "green");
            gridArrows[data.Red1ArrowColumn] = new ArrowImage("rarrow1", data.Red1ArrowColumn, "r_arrow.png", false, "red");
            gridArrows[data.Red2ArrowColumn] = new ArrowImage("rarrow2", data.Red2ArrowColumn, "r_arrow.png", true, "red");
            gridArrows[data.Yellow1ArrowColumn] = new ArrowImage("yarrow1", data.Yellow1ArrowColumn, "y_arrow.png", false, "yellow");
            gridArrows[data.Yellow2ArrowColumn] = new ArrowImage("yarrow2", data.Yellow2ArrowColumn, "y_arrow.png", true, "yellow");
            gridArrows[data.Blue1ArrowColumn] = new ArrowImage("barrow1", data.Blue1ArrowColumn, "b_arrow.png", false, "blue");
            gridArrows[data.Blue2ArrowColumn] = new ArrowImage("barrow2", data.Blue2ArrowColumn, "b_arrow.png", true, "blue");
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
            Grid.SetRow(image, 2);
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
                    Grid.SetRow(image, 1);
                    gr_profit.Children.Add(image);
                    break;
                case "blue":
                    Grid.SetRow(image, 3); gr_profit.Children.Add(image);
                    break;
                case "green":
                    Grid.SetRow(image, 2); gr_profit1.Children.Add(image);
                    break;

                case "yellow":
                    Grid.SetRow(image, 4); gr_profit1.Children.Add(image);
                    break;

            }
        }
        void MoveArrow(int sourceColumn, int destinationColumn)
        {
            sourceColumn = sourceColumn - 1;
            destinationColumn = destinationColumn - 1;
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
        int task_index = 0;
        private void New_Task()
        {
            if (task_index != data.Tasks.Length)
            {
                lb_0.Background = Brushes.Black;
                lb_1.Background = Brushes.Black;
                lb_2.Background = Brushes.Black;
                lb_3.Background = Brushes.Black;
                tbl_task.Text = data.Tasks[task_index];


                if (data.TruckColors[task_index].Substring(0, 1) == "1")
                    lb_0.Background = Brushes.Red;
                if (data.TruckColors[task_index].Substring(1, 1) == "1")
                    lb_1.Background = Brushes.Blue;
                if (data.TruckColors[task_index].Substring(2, 1) == "1")
                    lb_2.Background = Brushes.Yellow;
                if (data.TruckColors[task_index].Substring(3, 1) == "1")
                    lb_3.Background = Brushes.Green;

                task_index++;
            }
        }

        private void DiceRoll(object sender, RoutedEventArgs e)
        {
            DiceWindow diceWindow = new DiceWindow();
            diceWindow.Owner = this;

            // Set the startup location to center relative to the owner
            diceWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            diceWindow.NumberConfirmed += NewWindow_NumberConfirmed;
            diceWindow.Show();
            // Show the new window

        }
        TextBox tb_honnan = new TextBox();
        CheckBox cb_forgatok = new CheckBox();
        TextBox tb_hova = new TextBox();
        private void NewWindow_NumberConfirmed(object sender, int number)
        {
            string num = number.ToString();
            int num1 = int.Parse(num.Substring(0, 1));
            int num2 = int.Parse(num.Substring(1, 1));
            sum = num1 + num2;

            if (sum != 7 && gridArrows[sum - 1] != null)
            {
                MovingCircle();
            }
            New_Task();
        }
        bool IsEmpty { get; set; }
        CircleImage SelectArrow(string color)
        {
            string realcolor = string.Empty;
            switch (color)
            {
                case "piros": realcolor = "red"; break;
                case "kék": realcolor = "blue"; break;
                case "sárga": realcolor = "yellow"; break;
                case "zöld": realcolor = "green"; break;
            }
            int i = -1;
            do
            {
                i++;
            } while (!realcolor.Equals(gridCircles[i].Color));
            return gridCircles[i];
        }
        ActionWindow actionWindow;
        private void Action_Button_Click(object sender, RoutedEventArgs e)
        {
            if (gridArrows[sum - 1] == null && sum != 7)
            {
                IsEmpty = true;
            }
            actionWindow = new ActionWindow(sum, IsEmpty);
            if (actionWindow.ShowDialog() == true)
            {
                if (actionWindow.CardColor != null)
                {
                    CircleImage circle = SelectArrow(actionWindow.CardColor);
                    circle.Column = actionWindow.CardPosition-1;
                    MoveCircle(circle);
                }
            }

            if (IsEmpty)
            {
                forgatok = actionWindow.Forgatok;
                MoveArrow(actionWindow.Honnan, actionWindow.Hova);
            }
            else if (sum == 7)
            {
                forgatok = actionWindow.Forgatok;
                MoveArrow(actionWindow.Honnan, actionWindow.Hova);
                if (actionWindow.JokerColor!=null)
                {
                    CircleImage circle = SelectArrow(actionWindow.JokerColor);
                    if (actionWindow.IsPlus)
                    {
                        circle.Column++;
                    }
                    else
                    {
                        circle.Column--;
                    }
                    MoveCircle(circle);
                }
                
            }
        }
    }
}
