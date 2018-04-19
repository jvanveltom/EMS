using System.Windows;
using System.Windows.Controls;

namespace EMS_System.Resource.Control
{
    public class SearchTextBox : TextBox
    {
        // Textbox die je gebruikt bij search
        // Wordt op pagina aangeroepen bij search
        // Custom control 
        public static readonly DependencyProperty BackgroundTextProperty = DependencyProperty.Register(nameof(BackgroundText), typeof(string), typeof(SearchTextBox),
            new FrameworkPropertyMetadata(string.Empty));

        // Custom control
        static SearchTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchTextBox), new FrameworkPropertyMetadata(typeof(SearchTextBox)));
        }

        // Custom control
        public string BackgroundText
        {
            get { return (string)GetValue(BackgroundTextProperty); }
            set { SetValue(BackgroundTextProperty, value); }
        }
    }
}
