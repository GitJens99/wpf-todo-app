using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using toDo.Models;

namespace toDo.Services
{
    public interface ITodoItemService
    {
        IEnumerable<ToDoItem> ReadItemsFromJsonFile();

        void SerializeAllItems(IEnumerable<ToDoItem> todoItems);
    }
}
