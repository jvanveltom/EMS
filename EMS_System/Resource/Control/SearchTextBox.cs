using System.Windows;
using System.Windows.Controls;

namespace EMS_System.Resource.Control
{
    public class SearchTextBox : TextBox
    {
        public static readonly DependencyProperty BackgroundTextProperty = DependencyProperty.Register(nameof(BackgroundText), typeof(string), typeof(SearchTextBox),
            new FrameworkPropertyMetadata(string.Empty));

        static SearchTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchTextBox), new FrameworkPropertyMetadata(typeof(SearchTextBox)));
        }

        public string BackgroundText
        {
            get { return (string)GetValue(BackgroundTextProperty); }
            set { SetValue(BackgroundTextProperty, value); }
        }
    }
}
