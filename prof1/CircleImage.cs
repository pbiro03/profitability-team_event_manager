using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace prof1
{
    public class CircleImage:Image
    {
        public CircleImage(string name,string fileName,string color,int column)
        {
            this.Color = color;
            this.Name=name;
            this.Source= new BitmapImage(new Uri("Images/Circles/" + fileName, UriKind.Relative));
            this.Column = column;
            
        }
        public CircleImage()
        {
             
        }



        public string Color {  get; set; }
        public int Column {  get; set; }
        
    }
}
