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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace toDo.Controls
{
    /// <summary>
    /// Interaktionslogik für SampleControl.xaml
    /// </summary>
    public partial class SampleControl : UserControl
    {
        public static readonly DependencyProperty HeaderTextboxProperty =
            DependencyProperty.Register("HeaderTextbox", typeof(string), typeof(SampleControl), 
                                        new PropertyMetadata("Überschrift Default"));

        public static readonly DependencyProperty AddButtonContentProperty =
            DependencyProperty.Register("AddButtonContent", typeof(string), typeof(SampleControl), 
                                        new PropertyMetadata("Addbutton Default"));

        public static readonly DependencyProperty DeleteButtonContentProperty =
            DependencyProperty.Register("DeleteButtonContent", typeof(string), typeof(SampleControl), 
                                        new PropertyMetadata("Deletebutton default"));

        public string HeaderTextbox
        {
            get { return (string)GetValue(HeaderTextboxProperty); }
            set { SetValue(HeaderTextboxProperty, value); }
        }

        public string AddButtonContent
        {
            get { return (string)GetValue(AddButtonContentProperty); }
            set { SetValue(AddButtonContentProperty, value); }
        }

        public string DeleteButtonContent
        {
            get { return (string)GetValue(DeleteButtonContentProperty); }
            set { SetValue(DeleteButtonContentProperty, value); }
        }

        public SampleControl()
        {
            InitializeComponent();
        }
    }
}
