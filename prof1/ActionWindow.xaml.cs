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
        public string JokerColor { get; set; }
        public bool IsPlus { get; set; }
        public int Honnan {  get; set; }
        public int Hova {  get; set; }
        public bool Forgatok {  get; set; }
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
                DialogResult = true;
            }
            if(cb_forgatok.IsChecked == true)
            {
                Forgatok = true;
            }
            if (sum==7)
            {
                Honnan = int.Parse(tb_honnan.Text);
                Hova = int.Parse(tb_hova.Text);
                if (!cb_profitcolor.Text.Equals("Válassz színt!"))
                {
                    JokerColor = cb_profitcolor.Text;
                    IsPlus = radioButton1.IsChecked == true;
                }             
            }
            else if(empty && sum!=7)
            {
                Honnan = int.Parse(tb_honnan.Text);
                Hova = sum;
            }
            Close();

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
