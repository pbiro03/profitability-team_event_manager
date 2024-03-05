using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace prof1
{
    internal class TextWrappingButton:Button
    {
        public TextWrappingButton()
        {
            this.DefaultStyleKey = typeof(Button);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var contentPresenter = GetTemplateChild("contentPresenter") as ContentPresenter;
            if (contentPresenter != null)
            {
                contentPresenter.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);
            }
        }
    }
}
