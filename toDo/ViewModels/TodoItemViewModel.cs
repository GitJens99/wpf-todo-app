using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using toDo.Models;
using toDo.Services;

namespace toDo.ViewModels
{
    public class TodoItemViewModel
    {
        private ITodoItemService _todoItemService;
        private IEnumerable<ToDoItem> _allTodos;

        public string Name 
        { 
            get { return TodoItem.Name; }
            set
            {
                TodoItem.Name = value;
            }
        }

        public bool IsDone
        {
            get { return TodoItem.IsDone; }
            set
            {
                TodoItem.IsDone = value;
                _todoItemService.SerializeAllItems(_allTodos);
            }
        }

        public DateTime TimeStamp 
        {
            get { return TodoItem.TimeStamp; }
            set
            {
                TodoItem.TimeStamp = value;
            }
        }

        public ToDoItem TodoItem { get; }

        public TodoItemViewModel(
            ToDoItem todoItem,
            ITodoItemService todoItemService,
            IEnumerable<TodoItemViewModel> allTodos)
        {
            TodoItem = todoItem;
            _todoItemService = todoItemService;
            _allTodos = allTodos.Select(vm => vm.TodoItem);
        }
    }
}
