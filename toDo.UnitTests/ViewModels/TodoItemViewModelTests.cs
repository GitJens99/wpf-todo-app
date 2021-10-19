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
        [TestMethod]
        public void SetName_TodoItemNameIsSetToTodoItemViewModelName()
        {
            // Arrange --> Set up the data
            var todo = new ToDoItem();
            todo.Name = "Name is set correctly";
            var todoItemViewModel = new TodoItemViewModel(
                todo,
                new TodoItemService(),
                new ObservableCollection<TodoItemViewModel>());

            // Act --> Take an action
            var actual = todo.Name;
            // Assert the results
            todo.Name.ShouldBe("Name is set correctly");
            //Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void TodoItemIsDoneViweModel_TodoItemIsDoneViewModelIsSetCorrectly()
        {
            // Arrange --> Set up the data
            var todo = new ToDoItem();
            todo.IsDone = true;
            var todoItemViewModel = new TodoItemViewModel(
                todo,
                new TodoItemService(),
                new ObservableCollection<TodoItemViewModel>());

            bool expected = true;
            // Act --> Take an action
            var actual = todo.IsDone;
            // Assert the results
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TodoItemTimeStampViweModel_TodoItemTimeStampViewModelIsSetCorrectly()
        {
            // Arrange --> Set up the data
            var currentTime = DateTime.Now;
            var todo = new ToDoItem();
            todo.TimeStamp = currentTime;
            var todoItemViewModel = new TodoItemViewModel(
                todo,
                new TodoItemService(),
                new ObservableCollection<TodoItemViewModel>());

            DateTime expected = currentTime;
            // Act --> Take an action
            var actual = todo.TimeStamp;
            // Assert the results
            Assert.AreEqual(expected, actual);
        }
    }
}
