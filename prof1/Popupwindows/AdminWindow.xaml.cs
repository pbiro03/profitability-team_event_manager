using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace prof1
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow(ArrowImage[] maingridArrows, CircleImage[] maingridCircles)
        {
            this.mainArrows = maingridArrows;
            this.mainCircles = maingridCircles;
            InitializeComponent();
        }
        OptionalRadioButton[,] trendlinebuttons;
        CheckBox[] rotatecheckbox;
        RadioButton[,] profitbuttons;

        public ArrowImage[] gridArrows;
        public CircleImage[] gridCircles;
        public ArrowImage[] mainArrows;
        public CircleImage[] mainCircles;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            trendlinebuttons = new OptionalRadioButton[4, 10];
            rotatecheckbox = new CheckBox[10];

            int mainarrowind = 1;
            for (int i = 0; i < 10; i++)
            {
                rotatecheckbox[i] = new CheckBox();
                rotatecheckbox[i].Name = $"cb_{i}";
                Grid.SetRow(rotatecheckbox[i], 6);
                Grid.SetColumn(rotatecheckbox[i], i + 1);
                rotatecheckbox[i].HorizontalAlignment = HorizontalAlignment.Center;
                rotatecheckbox[i].VerticalAlignment = VerticalAlignment.Center;
                gr_admin.Children.Add(rotatecheckbox[i]);
                if (i == 5)
                { mainarrowind++; }

                for (int j = 0; j < 4; j++)
                {
                    trendlinebuttons[j, i] = new OptionalRadioButton();
                    trendlinebuttons[j, i].Name = $"rb_{i}{j}";
                    trendlinebuttons[j, i].HorizontalAlignment = HorizontalAlignment.Center;
                    trendlinebuttons[j, i].VerticalAlignment = VerticalAlignment.Center;
                    Grid.SetColumn(trendlinebuttons[j, i], i + 1);
                    Grid.SetRow(trendlinebuttons[j, i], j + 2);                   
                    trendlinebuttons[j, i].GroupName = $"r_{i}";
                    gr_admin.Children.Add(trendlinebuttons[j, i]);

                   
                }
                if (mainArrows[mainarrowind] != null)
                {
                    switch (mainArrows[mainarrowind].Color)
                    {
                        case "red":
                            trendlinebuttons[0, i].IsChecked = true;
                            break;
                        case "blue":
                            trendlinebuttons[1, i].IsChecked = true;
                            break;
                        case "green":
                            trendlinebuttons[3, i].IsChecked = true;
                            break;
                        case "yellow":
                            trendlinebuttons[2, i].IsChecked = true;
                            break;
                    }
                    if (mainArrows[mainarrowind].IsMirrored == true)
                    {
                        rotatecheckbox[i].IsChecked = true;
                    }
                }               
                mainarrowind++;
            }
            profitbuttons = new RadioButton[4, 5];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    profitbuttons[i, j] = new RadioButton();
                    profitbuttons[i, j].VerticalAlignment = VerticalAlignment.Center;
                    profitbuttons[i, j].Name = $"prb_{i}{j}";
                    profitbuttons[i, j].HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetColumn(profitbuttons[i, j], j + 1);
                    Grid.SetRow(profitbuttons[i, j], i + 2);
                    if (mainCircles[i].Column == j)
                    {
                        profitbuttons[i, j].IsChecked = true;
                    }
                    profitbuttons[i, j].GroupName = $"pr_{i}";
                    gr_profit.Children.Add(profitbuttons[i, j]);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gridArrows = new ArrowImage[12];
            for (int i = 0; i < trendlinebuttons.GetLength(1); i++)
            {
                for (int j = 0; j < trendlinebuttons.GetLength(0); j++)
                {
                    if (trendlinebuttons[j, i].IsChecked == true)
                    {
                        int arrindex = 0;
                        if (i < 5)
                            arrindex = i + 1;
                        else
                            arrindex = i + 2;
                        gridArrows[arrindex] = new ArrowImage();
                        gridArrows[arrindex].Column = arrindex;
                        switch (j)
                        {
                            case 0:
                                gridArrows[arrindex].Color = "red";
                                gridArrows[arrindex].Source = new BitmapImage(new Uri("Images/Arrows/r_arrow.png", UriKind.Relative));
                                break;
                            case 1:
                                gridArrows[arrindex].Color = "blue";
                                gridArrows[arrindex].Source = new BitmapImage(new Uri("Images/Arrows/b_arrow.png", UriKind.Relative));
                                break;
                            case 2:
                                gridArrows[arrindex].Color = "yellow";
                                gridArrows[arrindex].Source = new BitmapImage(new Uri("Images/Arrows/y_arrow.png", UriKind.Relative));
                                break;
                            case 3:
                                gridArrows[arrindex].Color = "green";
                                gridArrows[arrindex].Source = new BitmapImage(new Uri("Images/Arrows/g_arrow.png", UriKind.Relative));
                                break;
                        }
                        if (rotatecheckbox[i].IsChecked == true)
                        {
                            gridArrows[arrindex].IsMirrored = true;
                        }
                    }
                }

            }
            gridCircles = new CircleImage[4];
            for (int i = 0; i < profitbuttons.GetLength(0); i++)
            {
                for (int j = 0; j < profitbuttons.GetLength(1); j++)
                {
                    if (profitbuttons[i, j].IsChecked == true)
                    {
                        gridCircles[i] = new CircleImage();
                        gridCircles[i].Column = j;
                        switch (i)
                        {
                            case 0:
                                gridCircles[i].Color = "red";
                                gridCircles[i].Source = new BitmapImage(new Uri("Images/Circles/r_circle.png", UriKind.Relative));
                                break;
                            case 1:
                                gridCircles[i].Color = "blue";
                                gridCircles[i].Source = new BitmapImage(new Uri("Images/Circles/b_circle.png", UriKind.Relative));
                                break;
                            case 2:
                                gridCircles[i].Color = "yellow";
                                gridCircles[i].Source = new BitmapImage(new Uri("Images/Circles/y_circle.png", UriKind.Relative));
                                break;
                            case 3:
                                gridCircles[i].Color = "green";
                                gridCircles[i].Source = new BitmapImage(new Uri("Images/Circles/g_circle.png", UriKind.Relative));
                                break;
                        }
                    }
                }
            }
            this.DialogResult = true;
            this.Close();
        }


    }
}
