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
    public class ToDoItem : IComparable<ToDoItem>
    {
        public string Name { get; set; }

        public bool IsDone { get; set; }

        public DateTime TimeStamp { get; set; }

        /*      this            obj
         * -1         vor >
         * 0          gleich ==
         * 1          nach < 
         */
        public int CompareTo(ToDoItem other)
        {
            if(this.IsDone == false && other.IsDone == true)
            {
                return -1;
            }
            else if(this.IsDone == true && other.IsDone == false)
            {
                return 1;
            }
            else
            {
                if(this.TimeStamp.Date < other.TimeStamp.Date)
                {
                    return -1;
                } 
                else if(this.TimeStamp.Date > other.TimeStamp.Date)
                {
                    return 1;
                }
                else
                {
                    return this.Name.CompareTo(other.Name);
                }
            }
        }
    }
}
