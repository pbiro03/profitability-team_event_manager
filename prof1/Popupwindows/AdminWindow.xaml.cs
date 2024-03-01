using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        public AdminWindow(ArrowImage[] gridArrows)
        {
            InitializeComponent();
        }
        RadioButton[,] buttons;
        CheckBox[] rotatecheckbox;

        public  ArrowImage[] gridArrows; //ne felejtsük el, hogy ez a gridArrow a másikkal ellentétben nem veszi magába a hetes oszlop értékét
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            buttons = new RadioButton[4,10];
            rotatecheckbox= new CheckBox[10];
            int buttonindex = 0;
            int checkboxindex = 0;
            for (int i = 0; i < 10; i++)
            {
                rotatecheckbox[checkboxindex]=new CheckBox();
                rotatecheckbox[checkboxindex].Name=$"cb_{i}";
                Grid.SetRow(rotatecheckbox[checkboxindex], 6);
                Grid.SetColumn(rotatecheckbox[checkboxindex], i+1);
                rotatecheckbox[checkboxindex].HorizontalAlignment=HorizontalAlignment.Center;
                rotatecheckbox[checkboxindex].VerticalAlignment=VerticalAlignment.Center;
                gr_admin.Children.Add(rotatecheckbox[checkboxindex]);
                for (int j = 0; j < 4; j++)
                {
                    buttons[j,i] = new RadioButton();
                    buttons[j,i].Name = $"rb_{i}{j}";
                    buttons[j, i].HorizontalAlignment = HorizontalAlignment.Center;
                    buttons[j, i].VerticalAlignment = VerticalAlignment.Center;
                    Grid.SetColumn(buttons[j, i], i+1);
                    Grid.SetRow(buttons[j, i], j+2);
                    buttons[j, i].GroupName= $"r_{i}";
                    gr_admin.Children.Add(buttons[j, i]);
                    buttonindex++;
                }
                checkboxindex++;
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gridArrows = new ArrowImage[12];           
            for (int i = 0; i < buttons.GetLength(1); i++)
            {
                for (int j = 0; j < buttons.GetLength(0); j++)
                {
                    if (buttons[j, i].IsChecked == true)
                    {
                        int arrindex = 0;
                        if (i < 5)
                            arrindex = i + 1;
                        else
                            arrindex = i + 2;
                        gridArrows[arrindex] = new ArrowImage();
                        gridArrows[arrindex].Column = arrindex;//nem biztos!!
                        switch (j)
                        {
                            case 0:
                                gridArrows[arrindex].Color = "red";
                                gridArrows[arrindex].Source= new BitmapImage(new Uri("Images/Arrows/r_arrow.png", UriKind.Relative));
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
            this.DialogResult = true;
            this.Close();
        }
    }
}
