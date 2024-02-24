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

            if (int.TryParse(tb_num.Text, out int enteredNumber))
            {
                // Fire the event to pass the Number back to the main window
                NumberConfirmed?.Invoke(this, enteredNumber);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid Number.");
            }
        }
    }
}
