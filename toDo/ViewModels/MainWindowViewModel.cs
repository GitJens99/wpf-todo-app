using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using toDo.Commands;
using toDo.Models;
using toDo.Services;

namespace toDo.ViewModels
{
    static class EnumerableExtensions
    {
        public static IEnumerable<ToDoItem> Where2(this IEnumerable<ToDoItem> todos, Func<ToDoItem, bool> filterMethod)
        {
            List<ToDoItem> result = new List<ToDoItem>();
            foreach (var todo in todos)
            {
                if (filterMethod(todo))
                {
                    result.Add(todo);
                }
            }
            return result;
        }

        public static IEnumerable<TodoItemViewModel> Select2(this IEnumerable<ToDoItem> todos,
            Func<ToDoItem, TodoItemViewModel> selector)
        {
            List<TodoItemViewModel> result = new List<TodoItemViewModel>();
            foreach(var todo in todos)
            {
                result.Add(selector(todo));
            }
            return result;
        }
    }
    public class MainWindowViewModel : ViewModelBase
    {        
        private const string NEW_TODO = "Neues Todo";

        public ITodoItemService _todoItemService;
        public IDateTimeService _dateTimeService;

        private ObservableCollection<TodoItemViewModel> _todoItems;
        public ObservableCollection<TodoItemViewModel> TodoItems 
        {
            get { return _todoItems; }
            set 
            {
                _todoItems = value;
                RaisePropertyChanged(nameof(TodoItems));
            }
        }

        private TodoItemViewModel _selectedTodoItem;
        public TodoItemViewModel SelectedTodoItem 
        {
            get { return _selectedTodoItem; }
            set 
            { 
                _selectedTodoItem = value; 
                DeleteButtonCommand?.RaiseCanExecuteChanged(); 
            }
        }

        private string _newTodoName;
        public string NewTodoName
        {
            get { return _newTodoName; }
            set 
            {
                _newTodoName = value; 
                AddButtonCommand?.RaiseCanExecuteChanged();
                RaisePropertyChanged(nameof(NewTodoName));
            }
        }

        private int _todaysNotFinishedTodosCounter;
        public int TodaysNotFinishedTodosCounter
        {
            get
            {
                return _todaysNotFinishedTodosCounter;
            }
            set
            {
                _todaysNotFinishedTodosCounter = value;
                RaisePropertyChanged(nameof(TodaysNotFinishedTodosCounter));
            }
        }

        public RelayCommand AddButtonCommand { get; set; }
        public RelayCommand DeleteButtonCommand { get; set; }
        public RelayCommand ShowAllTodosCommand { get; }
        public RelayCommand ShowFinishedTodosCommand { get; }
        public RelayCommand ShowNotFinishedTodosCommand { get; }


        public MainWindowViewModel(
            ITodoItemService todoItemService,
            IDateTimeService dateTimeService)
        {
            _todoItemService = todoItemService;
            _dateTimeService = dateTimeService;
           
            TodoItems = new ObservableCollection<TodoItemViewModel>();
            var SortedTodoItemModels = _todoItemService.ReadItemsFromJsonFile()
                .OrderBy(todo => todo);

            foreach(var item in SortedTodoItemModels)
            {
                TodoItems.Add(CreateTodoViewModel(item));
            } 
             
            AddButtonCommand = new RelayCommand(NewAddTodoItem, AddButtonCanUse);
            DeleteButtonCommand = new RelayCommand(NewDeleteTodoItem, DeleteButtonCanUse);
            ShowAllTodosCommand = new RelayCommand(ShowAllTodos, ShowAllTodosCanUse);
            ShowFinishedTodosCommand = new RelayCommand(ShowFinishedTodos, ShowFinishedTodosCanUse);
            ShowNotFinishedTodosCommand = new RelayCommand(ShowNotFinishedTodos, ShowNotFinishedTodosCanUse);

            UpdateCounter();
            NewTodoName = NEW_TODO;
        }        

        private bool ShowNotFinishedTodosCanUse()
        {
            return true;
        }

        private void ShowNotFinishedTodos()
        {
            var notFinishedTodos = _todoItemService.ReadItemsFromJsonFile()
                .Where2(item => !item.IsDone)
                .Select2(CreateTodoViewModel);
            TodoItems = new ObservableCollection<TodoItemViewModel>(notFinishedTodos); 
        }

        private bool ShowFinishedTodosCanUse()
        {
            return true;
        }

        private void ShowFinishedTodos()
        {
            var allTodoItems = _todoItemService.ReadItemsFromJsonFile();
            var finishedTodos = from item 
                                in _todoItemService.ReadItemsFromJsonFile()
                                where item.IsDone
                                select CreateTodoViewModel(item);

            TodoItems = new ObservableCollection<TodoItemViewModel>(finishedTodos);
        }

        private bool ShowAllTodosCanUse()
        {
            return true;
        }

        private void ShowAllTodos()
        {
            TodoItems = new ObservableCollection<TodoItemViewModel>();
            var allTodoItems = _todoItemService.ReadItemsFromJsonFile();
            foreach(var item in allTodoItems)
            {
                TodoItems.Add(CreateTodoViewModel(item));
            }
        }

        private TodoItemViewModel CreateTodoViewModel(ToDoItem todoItem)
        {
            return new TodoItemViewModel(todoItem, _todoItemService, TodoItems, this);
        }

        public void UpdateCounter()
        {
            var filteredList = TodoItems
                .Where(item => !item.IsDone)
                .Where(TodoItemIsCreatedToday);

            TodaysNotFinishedTodosCounter = filteredList.Count();
        }

        private bool TodoItemIsCreatedToday(TodoItemViewModel todo)
        {
            return todo.TimeStamp.Date == DateTime.Now.Date;
        }

        public bool AddButtonCanUse()
        {
            return (!String.IsNullOrEmpty(NewTodoName)) & !String.Equals(NewTodoName, NEW_TODO);
        }

        public bool DeleteButtonCanUse()
        {
            return SelectedTodoItem != null;
        }

        private void NewAddTodoItem()
        {
            if (!(String.IsNullOrWhiteSpace(NewTodoName)) && NewTodoName != NEW_TODO)
            {
                var newToDoItem = new ToDoItem()
                {
                    Name = NewTodoName,
                    TimeStamp = _dateTimeService.Now()
                };
                TodoItems.Add(CreateTodoViewModel(newToDoItem));

                _todoItemService.SerializeAllItems(TodoItems.Select(viewModel => viewModel.TodoItem));
                UpdateCounter();
                NewTodoName = NEW_TODO;
            }
        }

        private void NewDeleteTodoItem()
        {
            if (SelectedTodoItem != null)
            {
                TodoItems.Remove(SelectedTodoItem);
                _todoItemService.SerializeAllItems(TodoItems.Select(vm => vm.TodoItem));
                UpdateCounter();
            }
        }

        private void OnAddObjectButtonClickedWithList(object sender, RoutedEventArgs e)
        {
            var newToDoItem = new ToDoItem()
            {
                //Name = EingabeTextBox.Text,
                TimeStamp = DateTime.Now,
            };
            TodoItems.Add(CreateTodoViewModel(newToDoItem));

            var jsonString = JsonConvert.SerializeObject(TodoItems, Formatting.Indented);

        }

        private void OnDeleteButtonClickedWithList(object sender, RoutedEventArgs e)
        {
            if (SelectedTodoItem != null)
            {
                TodoItems.Remove(SelectedTodoItem);
            }
        }

        private void EingabeTextBox_TextChanged(object sender, RoutedEventArgs e) { } 
    }
}
