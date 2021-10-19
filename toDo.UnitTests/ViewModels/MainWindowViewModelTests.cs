using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using toDo.Models;
using toDo.Services;
using toDo.ViewModels;

namespace toDo.UnitTests.ViewModels
{
    public class FakeTodoService : ITodoItemService
    {
        public IEnumerable<ToDoItem> ReadItemsFromJsonFile()
        {
            return new ObservableCollection<ToDoItem>();
        }

        public bool SerializeAllItemsIsCalled = false;

        public void SerializeAllItems(IEnumerable<ToDoItem> todoItems)
        {
            SerializeAllItemsIsCalled = true;
        }
    }

    public class FakeDateTimeService : IDateTimeService
    {
        public DateTime TestNow { get; set; }
        public DateTime Now()
        {
            return TestNow;
        }
    }

    [TestClass]
    public class MainWindowViewModelTests
    {
        [TestMethod]
        public void AddNewTodo_NewTodoNameIsEmpty_AddTodoButtonCannotBeExecuted()
        {
            // Arrange
            var viewModel = CreateSut();
            viewModel.NewTodoName = "";

            // Act
            var canExecute = viewModel.AddButtonCommand.CanExecute(null);
            // Assert
            canExecute.ShouldBeFalse();
        }

        [TestMethod]
        public void AddNewTodo_NewTodoNameNotEmpty_AddTodoButtonCanBeExucted()
        {
            // Arrange
            var viewModel = CreateSut();
            viewModel.NewTodoName = "Einkaufen";
            //Act
            var canExecute = viewModel.AddButtonCommand.CanExecute(null);
            // Assert
            canExecute.ShouldBeTrue();
        }

        [TestMethod]
        public void SelectedTodoItem_NoItemIsSelected_DeleteButtonCannotBeExecuted()
        {
            // Arrange
            var viewModel = CreateSut();
            viewModel.SelectedTodoItem = null;
            // Act
            var canExecute = viewModel.DeleteButtonCommand.CanExecute(null);
            // Assert
            canExecute.ShouldBeFalse();
        }

        [TestMethod]
        public void SelectedTodoItem_ItemIsSelected_DeleteButtonCanBeExecuted()
        {
            // Arrange
            var viewModel = CreateSut();
            viewModel.SelectedTodoItem = new TodoItemViewModel(new ToDoItem(), null, viewModel.TodoItems) ;
            // Act
            var canExecute = viewModel.DeleteButtonCommand.CanExecute(null);
            // Assert
            canExecute.ShouldBeTrue();
        }

        [TestMethod]
        public void ExecuteAddNewTodo_TodoNameNotEmtpy_TodoItemIsAddedToList()
        {
            // Arrange
            var viewModel = CreateSut();
            viewModel.NewTodoName = "Einkaufen";
            // Act
            viewModel.AddButtonCommand.Execute(null);
            // Assert
            viewModel.TodoItems.Single().Name.ShouldBe("Einkaufen");
            //viewModel.TodoItems[0].Name.ShouldBe("Einkaufen");
        }

        [TestMethod]
        public void ExecuteAddNewTodo_TodoNameEmpty_TodoItemNotAddedToList()
        {
            // Arrange
            var viewModel = CreateSut();
            // Act
            viewModel.AddButtonCommand.Execute(null);
            // Assert
            viewModel.TodoItems.Count.ShouldBe(0);
        }

        [TestMethod]
        public void ExecuteDeleteTodo_SelectedTodoItemNotEmpty_TodoItemIsRemovedFromList()
        {
            // Arrange
            var viewModel = CreateSut();
            viewModel.NewTodoName = "Einkaufen";
            // Act
            // TodoItem hinzufügen
            viewModel.AddButtonCommand.Execute(null);
            // TodoItem entfernen
            viewModel.SelectedTodoItem = viewModel.TodoItems[0];
            viewModel.DeleteButtonCommand.Execute(null);
            // Assert
            viewModel.TodoItems.Count.ShouldBe(0);
        }

        [TestMethod]
        public void ExecuteDeleteTodo_SelectedTodoItemEmpty_TodoItemNotRemovedFromList()
        {
            // Arrange
            var viewModel = CreateSut();
            viewModel.NewTodoName = "Einkaufen";
            // Act
            // TodoItem hinzufügen
            viewModel.AddButtonCommand.Execute(null);
            // TodoItem entfernen
            viewModel.DeleteButtonCommand.Execute(null);
            // Assert
            viewModel.TodoItems.Count.ShouldBe(1);
        }

        [TestMethod]
        public void AddNewTodo_NewTodoHasCurrentTimeAsTimeStamp()
        {
            // Arrange
            var currentTime = DateTime.Now;
            var viewModel = CreateSut(currentTime);
            viewModel.NewTodoName = "Datum auf Richtigkeit pruefen";
            // Act
            viewModel.AddButtonCommand.Execute(null);
            // Assert
            var todo = viewModel.TodoItems.Single();
            todo.TimeStamp.ShouldBe(currentTime);
        }

        private MainWindowViewModel CreateSut(DateTime fakeNow = default(DateTime))
        {
            var dateTimeService = new FakeDateTimeService();
            dateTimeService.TestNow = fakeNow;
            return new MainWindowViewModel(new FakeTodoService(), dateTimeService);
        }
    }
}
