using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using toDo.Models;
using toDo.Services;
using toDo.ViewModels;

namespace toDo.UnitTests.ViewModels
{
    [TestClass]
    public class TodoItemViewModelTests
    {
        private TodoItemViewModel CreateSut(FakeTodoService fakeTodoService = null)
        {
            if(fakeTodoService == null)
            {
                fakeTodoService = new FakeTodoService();
            }
            return new TodoItemViewModel(
                new ToDoItem(), fakeTodoService, new ObservableCollection<TodoItemViewModel>());
        }

        [TestMethod]
        public void SetName_TodoItemNameIsSetToTodoItemViewModelName()
        {
            // Arrange --> Set up the data
            var todoItemViewModel = CreateSut();
            // Act --> Take an action
            todoItemViewModel.Name = "Todo Item Name is set to Todo Item ViewModel Name";
            // Assert the results
            todoItemViewModel.TodoItem.Name.ShouldBe("Todo Item Name is set to Todo Item ViewModel Name");
        }

        [TestMethod]
        public void SetIsDone_TodoItemIsDoneIsSetToTodoItemViewModelIsDone()
        {
            // Arrange --> Set up the data
            var todoItemViewModel = CreateSut();
            // Act --> Take an action
            todoItemViewModel.IsDone = true;
            // Assert the results
            todoItemViewModel.TodoItem.IsDone.ShouldBeTrue();
        }

        [TestMethod]
        public void TodoItemTimeStampViweModel_TodoItemTimeStampViewModelIsSetCorrectly()
        {
            // Arrange --> Set up the data
            var todoItemViewModel = CreateSut();
            var currentTime = DateTime.Now;
            // Act --> Take an action
            todoItemViewModel.TimeStamp = currentTime;
            // Assert the results
            todoItemViewModel.TodoItem.TimeStamp.ShouldBe(currentTime);
        }

        [TestMethod]
        public void SetIsDone_IsDoneIsTrue_WriteTodoIsExecuted()
        {
            // Arrange
            var fakeTodoService = new FakeTodoService();
            var todoItemViewModel = CreateSut(fakeTodoService);
            // Act
            todoItemViewModel.IsDone = true;
            // Assert
            fakeTodoService.SerializeAllItemsIsCalled.ShouldBeTrue();
        }

        [TestMethod]
        public void SetIsDone_IsDoneIsFalse_WriteTodoIsExecuted()
        {
            // Arrange 
            var fakeTodoService = new FakeTodoService();
            var todoItemViewModel = CreateSut(fakeTodoService);
            // Act
            todoItemViewModel.IsDone = false;
            // Assert
            fakeTodoService.SerializeAllItemsIsCalled.ShouldBeTrue();
        }
    }
}
