using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace prof1
{
    public class OptionalRadioButton : RadioButton
    {
        #region IsOptional Dependency Property
        public static readonly DependencyProperty IsOptionalProperty =
            DependencyProperty.Register(
                "IsOptional",
                typeof(bool),
                typeof(OptionalRadioButton),
                new PropertyMetadata((bool)true, (obj, args) =>
                {
                    ((OptionalRadioButton)obj).OnIsOptionalChanged(args);
                }));

        public bool IsOptional
        {
            get { return (bool)GetValue(IsOptionalProperty); }
            set { SetValue(IsOptionalProperty, value); }
        }

        private void OnIsOptionalChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        protected override void OnClick()
        {
            bool? wasChecked = this.IsChecked;
            base.OnClick();
            if (this.IsOptional && wasChecked == true)
                this.IsChecked = false;
        }
    }
}
