using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using toDo.Services;
using toDo.ViewModels;
using toDo.Views;

namespace toDo.Models
{
    public class ToDoItem 
    {
        public string Name { get; set; }

        public bool IsDone { get; set; }

        public DateTime TimeStamp { get; set; }

    }
}
