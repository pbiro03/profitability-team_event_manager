using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace prof1
{
    class ArrowImage:Image
    {
        public bool IsMirrored {  get; set; }
        public string Color { get; set; }
        public string FileName {  get; set; }
        public int Column {  get; set; }
        public ArrowImage(string name, int column, string fileName,  bool isMirrored,string color)
        {
            this.IsMirrored = isMirrored;
            this.Color = color;
            this.Name = name;
            this.FileName = fileName;
            this.Column = column;
            this.Source= new BitmapImage(new Uri("Images/Arrows/" + fileName, UriKind.Relative));
            this.Stretch= Stretch.Fill; 
        }
        public void MirrorArrow()
        {
            if (IsMirrored ) 
            {
                ScaleTransform scaleTransform = new ScaleTransform(1, -1);
                this.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
                this.RenderTransform = scaleTransform;
                IsMirrored = false; 
            }
            else 
            {
                ScaleTransform scaleTransform2 = new ScaleTransform(-1, 1);
                this.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
                this.RenderTransform = scaleTransform2;
                IsMirrored = true; 
            }
        }
       
    }
}
