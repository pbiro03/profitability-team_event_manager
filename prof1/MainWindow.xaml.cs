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
//using static System.Net.Mime.MediaTypeNames;

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
            b_action.IsEnabled = false;
            tbl_teamnumber.Text = $"{data.WhichTeamsRound[roundIndex]}";
            img_dice1.Source = new BitmapImage(new Uri($"Images/Dices/dice.png", UriKind.Relative));
            img_dice2.Source = new BitmapImage(new Uri($"Images/Dices/dice.png", UriKind.Relative));
            downtriangle.Source = new BitmapImage(new Uri("Images/triangle3.png", UriKind.Relative)); 
            triangle.Source = new BitmapImage(new Uri("Images/triangle2.png", UriKind.Relative));
            tbl_placeanddate.Text = data.PlaceNameandDate;
            tbl_round.Text = "1. kör";
            PopulateGridArrows();
            PopulateGridCircles();
            ArrowCopy();
        }
        int roundIndex = 0;
        public int sum { get; set; }
        ArrowImage[] gridArrows = new ArrowImage[12];
        CircleImage[] gridCircles = new CircleImage[4];
        DataManager data = new DataManager("elso_proba.txt");
        ArrowImage[] previousRoundArrows=new ArrowImage[12];
        CircleImage[] previousRoundCircles =new CircleImage[4];
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
            if (gridCircles[i].Column == 5 && !gridArrows[sum - 1].IsMirrored)
            {
                Grid.SetColumn(downtriangle, 11);
                Grid.SetRow(downtriangle, 0);
                gr_trendlinetable.Children.Add(downtriangle);
                gridArrows[sum - 1].MirrorArrow();
            }
            else if (gridCircles[i].Column == 1 && gridArrows[sum - 1].IsMirrored)
            {
                Grid.SetColumn(downtriangle, 1);
                Grid.SetRow(downtriangle, 0);
                gr_trendlinetable.Children.Add(downtriangle);
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
            //if (image.Color.Equals("red") || image.Color.Equals("blue"))
            //{
                gr_profit.Children.Remove(image);
                Grid.SetColumn(image, image.Column);
                gr_profit.Children.Add(image);
            //}
            //else
            //{
            //    gr_profit1.Children.Remove(image);
            //    Grid.SetColumn(image, image.Column);
            //    gr_profit1.Children.Add(image);
            //}

        }



        void PopulateGridArrows()
        {
            for (int i = 0; i < data.arrowImages.Length; i++)
            {
                if (data.arrowImages[i] != null)
                    gridArrows[data.arrowImages[i].Column] = new ArrowImage($"{data.arrowImages[i].Color.Substring(0, 1)}arrow{i}", data.arrowImages[i].Column, $"{data.arrowImages[i].Color.Substring(0, 1)}_arrow.png", data.arrowImages[i].IsMirrored, data.arrowImages[i].Color);
            }
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
            gridCircles[3] = new CircleImage("g_circle", "g_circle.png", "green", data.GreenProfit_Column);
            gridCircles[2] = new CircleImage("y_circle", "y_circle.png", "yellow", data.YellowProfit_Column);
            for (int i = 0; i < gridCircles.Length; i++)
            {
                SetupCircleImage(gridCircles[i]);
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
        void SetupCircleImage(CircleImage image)
        {
            Grid.SetColumn(image, image.Column);
            switch (image.Color)
            {
                case "red":
                    Grid.SetRow(image, 1);
                    image.HorizontalAlignment = HorizontalAlignment.Left;
                    image.VerticalAlignment = VerticalAlignment.Top;
                    gr_profit.Children.Add(image);
                    break;
                case "blue":
                    Grid.SetRow(image, 2);
                    image.HorizontalAlignment = HorizontalAlignment.Left;
                    image.VerticalAlignment = VerticalAlignment.Top;
                    gr_profit.Children.Add(image);
                    break;
                case "green":
                    Grid.SetRow(image, 1);
                    image.HorizontalAlignment = HorizontalAlignment.Right;
                    image.VerticalAlignment = VerticalAlignment.Center;
                    gr_profit.Children.Add(image);
                    break;

                case "yellow":
                    Grid.SetRow(image, 2);
                    image.HorizontalAlignment = HorizontalAlignment.Right;
                    image.VerticalAlignment = VerticalAlignment.Center;
                    gr_profit.Children.Add(image);
                    break;

            }
        }
        Image triangle = new Image();
        Image downtriangle = new Image();


        void MoveArrow(int sourceColumn, int destinationColumn)
        {
            sourceColumn = sourceColumn - 1;
            destinationColumn = destinationColumn - 1;
            ArrowImage sourceImage = gridArrows[sourceColumn];
            if (forgatok)
            {
                sourceImage.MirrorArrow();
            }

            //add triangle
            
            Grid.SetColumn(triangle, sourceColumn);
            Grid.SetRow(triangle, 0);
            gr_trendlinetable.Children.Add(triangle);
            
            Grid.SetColumn(downtriangle, destinationColumn);
            Grid.SetRow(downtriangle, 0);
            gr_trendlinetable.Children.Add(downtriangle);
            //end 
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
                
                tbl_task.Text = data.Tasks[task_index];

                if (data.TruckColors[task_index].Substring(0, 1) == "1")
                    im_truck0.Source = new BitmapImage(new Uri("Images/Trucks/r_truck.png", UriKind.Relative));
                else
                    im_truck0.Source = null;
                if (data.TruckColors[task_index].Substring(1, 1) == "1")
                    im_truck1.Source = new BitmapImage(new Uri("Images/Trucks/b_truck.png", UriKind.Relative));
                else
                    im_truck1.Source = null;
                if (data.TruckColors[task_index].Substring(2, 1) == "1")
                    im_truck2.Source = new BitmapImage(new Uri("Images/Trucks/g_truck.png", UriKind.Relative));
                else
                    im_truck2.Source = null;
                if (data.TruckColors[task_index].Substring(3, 1) == "1")
                    im_truck3.Source = new BitmapImage(new Uri("Images/Trucks/y_truck.png", UriKind.Relative));
                else
                    im_truck3.Source = null;
                task_index++;
            }
        }

        private void DiceRoll(object sender, RoutedEventArgs e)
        {
            DiceWindow diceWindow = new DiceWindow();
            diceWindow.Owner = this;
            diceWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            diceWindow.NumberConfirmed += NewWindow_NumberConfirmed;
            diceWindow.Show();
        }
        TextBox tb_honnan = new TextBox();
        CheckBox cb_forgatok = new CheckBox();
        TextBox tb_hova = new TextBox();
        
        private void NewWindow_NumberConfirmed(object sender, int number)
        {
            string num = number.ToString();
            int num1 = int.Parse(num.Substring(0, 1));
            int num2 = int.Parse(num.Substring(1, 1));
            //DicePictureChanger(num1);
            sum = num1 + num2;
            //if (sum != 7 && gridArrows[sum - 1] != null)
            //{
            //    MovingCircle();
            //}
            New_Task();
            
            img_dice1.Source = new BitmapImage(new Uri($"Images/Dices/dice_{num1}.png", UriKind.Relative));
            img_dice2.Source = new BitmapImage(new Uri($"Images/Dices/dice_{num2}.png", UriKind.Relative));
            b_diceroll.IsEnabled = false;
            b_action.IsEnabled = true;
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
            actionWindow = new ActionWindow(sum, IsEmpty, gridArrows);
            if (actionWindow.ShowDialog() == true)
            {
                if (actionWindow.CardColor != null)
                {
                    CircleImage circle = SelectArrow(actionWindow.CardColor);
                    circle.Column = actionWindow.CardPosition;
                    MoveCircle(circle);
                }
                
            }
            if (IsEmpty && actionWindow.Honnan != 0)
            {
                forgatok = actionWindow.Forgatok;
                MoveArrow(actionWindow.Honnan, actionWindow.Hova);
            }
            else if (sum == 7 && actionWindow.Honnan != 0 && actionWindow.Hova != 0)
            {
                forgatok = actionWindow.Forgatok;
                MoveArrow(actionWindow.Honnan, actionWindow.Hova);
                if (actionWindow.JokerColor != null)
                {
                    CircleImage circle = SelectArrow(actionWindow.JokerColor);
                    try
                    {
                        if (actionWindow.IsPlus)
                        {
                            circle.Column++;
                            if (circle.Column > 5)
                            {
                                throw new FormatException("Már nem növelheted tovább a jokerprofit értékét");
                            }
                        }
                        else
                        {
                            circle.Column--;
                            if (circle.Column < 1)
                            {
                                throw new FormatException("Már nem csökkentheted tovább a jokerprofit értékét");
                            }
                        }
                        MoveCircle(circle);
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
                }
            }
            else
            {
                if (sum != 7 && gridArrows[sum - 1] != null)
                {
                    MovingCircle();
                }
            }
        }
        private void New_Round_Button(object sender, RoutedEventArgs e)
        {
            b_diceroll.IsEnabled = true;
            b_action.IsEnabled = false;
            img_dice1.Source = new BitmapImage(new Uri($"Images/Dices/dice.png", UriKind.Relative));
            img_dice2.Source = new BitmapImage(new Uri($"Images/Dices/dice.png", UriKind.Relative));
            gr_trendlinetable.Children.Remove(triangle);
            gr_trendlinetable.Children.Remove(downtriangle);
            im_truck0.Source = null;
            im_truck1.Source = null;
            im_truck2.Source = null;
            im_truck3.Source = null;

            ArrowCopy();
            tbl_task.Text = "";
            roundIndex++;
            if (roundIndex<data.Tasks.Length)
            {
                tbl_teamnumber.Text = $"{data.WhichTeamsRound[roundIndex]}";
                tbl_round.Text = $"{roundIndex + 1}. kör";
            }
            else
            {
                MessageBox.Show("Elfogytak a kártyák");
            }
        }
        void ArrowCopy()
        {
            int i = 0;
            foreach (var arrow in gridArrows)
            {

                if (arrow != null)
                {
                    ArrowImage newImage = new ArrowImage(arrow.Name, arrow.Column, arrow.Color.Substring(0, 1) + "_arrow.png", arrow.IsMirrored, arrow.Color);
                    previousRoundArrows[i] = newImage;
                }
                else
                {
                    previousRoundArrows[i] = null;
                }
                i++;
            }
            i = 0;
            foreach (var circle in gridCircles)
            {
                CircleImage newImage = new CircleImage(circle.Name, circle.Color.Substring(0, 1) + "_circle.png", circle.Color, circle.Column);
                previousRoundCircles[i] = newImage;
                i++;
            }
        }

        private void b_admin_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow aw = new AdminWindow(gridArrows,gridCircles,previousRoundArrows,previousRoundCircles);
            //aw.Show();
            if (aw.ShowDialog()==true)
            {
                for (int i = 0; i < aw.gridArrows.Length; i++)
                {
                    gr_trendlinetable.Children.Remove(gridArrows[i]);
                    if (aw.gridArrows[i] != null)
                    {
                        SetupArrowImage(aw.gridArrows[i]);
                    }
                    gridArrows[i] = aw.gridArrows[i];
                }
                for (int i = 0; i < aw.gridCircles.Length; i++)
                {
                    gr_profit.Children.Remove(gridCircles[i]);
                    
                    SetupCircleImage(aw.gridCircles[i]);
                                       
                    gridCircles[i] = aw.gridCircles[i];
                }

            }
        }

       
    }
}
