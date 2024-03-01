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
    /// Interaction logic for DiceWindow.xaml
    /// </summary>
    public partial class DiceWindow : Window
    {
        public DiceWindow()
        {
            InitializeComponent();
        }
        public event EventHandler<int> NumberConfirmed;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tb_num.Text))
                {
                    throw new ArgumentNullException("Adj meg egy számot",nameof(tb_num));
                }
                else if (!int.TryParse(tb_num.Text, out _) || int.Parse(tb_num.Text.Substring(0,1))<1|| int.Parse(tb_num.Text.Substring(0, 1)) >6 || int.Parse(tb_num.Text.Substring(1, 1)) < 1 || int.Parse(tb_num.Text.Substring(1, 1)) > 6||tb_num.Text.Length>2)
                {
                    throw new FormatException("Rossz bemeneti formátum.\n A két kockával dobott számot egymás után space nélkül kell beírni");
                }
                NumberConfirmed?.Invoke(this, int.Parse(tb_num.Text));
                this.Close();
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
