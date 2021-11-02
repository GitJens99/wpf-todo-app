using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using toDo.Commands;

namespace toDo.Controls
{
    class SampleCustomControl : Control
    {
        public static readonly DependencyProperty HeaderTextboxProperty =
            DependencyProperty.Register("HeaderTextbox", typeof(string), typeof(SampleCustomControl),
                                        new PropertyMetadata("Custom Default Überschrift "));

        public static readonly DependencyProperty AddButtonContentProperty =
            DependencyProperty.Register("AddButtonContent", typeof(string), typeof(SampleCustomControl),
                                        new PropertyMetadata("Custom Default Addbutton"));

        public static readonly DependencyProperty DeleteButtonContentProperty =
            DependencyProperty.Register("DeleteButtonContent", typeof(string), typeof(SampleCustomControl),
                                        new PropertyMetadata("Custom Default Deletebutton"));

        public static readonly DependencyProperty NewTodoNameCustomProperty =
            DependencyProperty.Register("NewTodoNameCustom", typeof(string), typeof(SampleCustomControl), new PropertyMetadata("Custom Default New Todo"));

        public static readonly DependencyProperty AddButtonCommandCustomProperty =
            DependencyProperty.Register("AddButtonCommandCustom", typeof(RelayCommand), typeof(SampleCustomControl), new PropertyMetadata(null));

        public static readonly DependencyProperty DeleteButtonCommandCustomProperty =
            DependencyProperty.Register("DeleteButtonCommandCustom", typeof(RelayCommand), typeof(SampleCustomControl), new PropertyMetadata(null));

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

        public string NewTodoNameCustom
        {
            get { return (string)GetValue(NewTodoNameCustomProperty); }
            set { SetValue(NewTodoNameCustomProperty, value); }
        }

        public RelayCommand AddButtonCommandCustom
        {
            get { return (RelayCommand)GetValue(AddButtonCommandCustomProperty); }
            set { SetValue(AddButtonCommandCustomProperty, value); }
        }

        public RelayCommand DeleteButtonCommandCustom
        {
            get { return (RelayCommand)GetValue(DeleteButtonCommandCustomProperty); }
            set { SetValue(DeleteButtonCommandCustomProperty, value); }
        }
    }
}
