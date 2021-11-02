using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using toDo.Models;
using toDo.Services;

namespace toDo.ViewModels
{
    public class TodoItemViewModel : ViewModelBase
    {
        private readonly ITodoItemService _todoItemService;
        private readonly MainWindowViewModel _viewModel;
        private readonly IEnumerable<ToDoItem> _allTodos;
        
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
                /*_viewModel.TodoItems = new System.Collections.ObjectModel.ObservableCollection<TodoItemViewModel>
                    (_todoItemService.ReadItemsFromJsonFile().
                    OrderBy(todo => todo).
                    Select(todo => new TodoItemViewModel(todo, _todoItemService, _viewModel.TodoItems, _viewModel)));*/
                
                _viewModel.UpdateCounter();
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
            IEnumerable<TodoItemViewModel> allTodos,
            MainWindowViewModel viewModel)
        {
            TodoItem = todoItem;
            _todoItemService = todoItemService;
            _viewModel = viewModel;
            _allTodos = allTodos.Select(vm => vm.TodoItem);
        }
    }
}
