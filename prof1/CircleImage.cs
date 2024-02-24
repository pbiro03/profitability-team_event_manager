using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace prof1
{
    internal class CircleImage:Image
    {
        public CircleImage(string name,string fileName,string color,int column)
        {
            this.Color = color;
            this.Name=name;
            this.Source= new BitmapImage(new Uri("Images/Circles/" + fileName, UriKind.Relative));
            this.Stretch = System.Windows.Media.Stretch.Fill;
            this.Column = column;
            this.Margin= CalculateMargin();
        }

        System.Windows.Thickness CalculateMargin()
        {
            
            int leftMargin = 20; 
            int topMargin = 0; 
            int rightMargin = 20; 
            int bottomMargin = 0;

            return new System.Windows.Thickness(leftMargin, topMargin, rightMargin, bottomMargin);
        }


        public string Color {  get; set; }
        public int Column {  get; set; }
        
    }
}
