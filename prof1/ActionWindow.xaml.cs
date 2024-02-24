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
    /// Interaction logic for ActionWindow.xaml
    /// </summary>
    public partial class ActionWindow : Window
    {
        private int sum;
        private bool empty;
        public int CardPosition { get; set; }
        public string CardColor { get; set; }
        public ActionWindow(int sum, bool isEmpty)
        {
            InitializeComponent();
            this.sum = sum;
            this.empty = isEmpty;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (tb_cardvalue!=null && !cb_cardcolor.Text.Equals("Válassz színt!"))
            {
                CardPosition = int.Parse(tb_cardvalue.Text);
                CardColor = cb_cardcolor.Text;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (sum!=7)
            {
                tb_hova.IsEnabled = false;
                if (empty)
                {
                    tb_hova.Text = Convert.ToString(sum);
                }
                else
                {
                    tb_honnan.IsEnabled = false;
                    cb_forgatok.IsEnabled = false;
                }
                

                //Joker
                cb_profitcolor.IsEnabled = false;
                radioButton1.IsEnabled = false;
                radioButton2.IsEnabled = false;
            }
        }
    }
}
