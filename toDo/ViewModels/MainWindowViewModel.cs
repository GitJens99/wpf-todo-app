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
    public class MainWindowViewModel : ViewModelBase
    {
        private const string NEW_TODO = "Neues Todo";
        
        public ITodoItemService _todoItemService;
        public IDateTimeService _dateTimeService;

        public ObservableCollection<TodoItemViewModel> TodoItems { get; set; }

        private TodoItemViewModel _selectedTodoItem;
        public TodoItemViewModel SelectedTodoItem 
        {
            get { return _selectedTodoItem; }
            set { _selectedTodoItem = value; DeleteButtonCommand?.RaiseCanExecuteChanged(); }
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

        public RelayCommand AddButtonCommand { get; set; }
        public RelayCommand DeleteButtonCommand { get; set; }

        public MainWindowViewModel(
            ITodoItemService todoItemService,
            IDateTimeService dateTimeService)
        {
            _todoItemService = todoItemService;
            _dateTimeService = dateTimeService;
           
            TodoItems = new ObservableCollection<TodoItemViewModel>();
            var todoItemModels = _todoItemService.ReadItemsFromJsonFile();

            foreach(var item in todoItemModels)
            {
                TodoItems.Add(CreateTodoViewModel(item));
            }

            AddButtonCommand = new RelayCommand(NewAddTodoItem, AddButtonCanUse);
            DeleteButtonCommand = new RelayCommand(NewDeleteTodoItem, DeleteButtonCanUse);

            NewTodoName = NEW_TODO;
        }

        private TodoItemViewModel CreateTodoViewModel(ToDoItem todoItem)
        {
            return new TodoItemViewModel(todoItem, _todoItemService, TodoItems);
        }

        public bool AddButtonCanUse()
        {
            return (!String.IsNullOrEmpty(NewTodoName)) &! String.Equals(NewTodoName, NEW_TODO);
        }

        public bool DeleteButtonCanUse()
        {
            return SelectedTodoItem != null;
        }

        private void NewAddTodoItem()
        {
            if (!(String.IsNullOrWhiteSpace(NewTodoName)))
            {
                var newToDoItem = new ToDoItem()
                {
                    Name = NewTodoName,
                    TimeStamp = _dateTimeService.Now()
                };
                TodoItems.Add(CreateTodoViewModel(newToDoItem));

                _todoItemService.SerializeAllItems(TodoItems.Select(vm => vm.TodoItem));

                NewTodoName = NEW_TODO;
            }
        }

        private void NewDeleteTodoItem()
        {
            if (SelectedTodoItem != null)
            {
                TodoItems.Remove(SelectedTodoItem);
                _todoItemService.SerializeAllItems(TodoItems.Select(vm => vm.TodoItem));
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
