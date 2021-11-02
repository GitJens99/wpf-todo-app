using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using toDo.Models;

namespace toDo.Services
{
    public class TodoItemService : ITodoItemService
    {
        private const string jsonPath = @"c:\temp\toDoListe.json";

        public IEnumerable<ToDoItem> ReadItemsFromJsonFile()
        {
            ObservableCollection<ToDoItem> tempToDoItems = new ObservableCollection<ToDoItem>();
            // Json-Datei einlesen und ggf. in toDo-Liste schreiben
            if (File.Exists(jsonPath))
            {
                var jsonString = File.ReadAllText(jsonPath);
                var toDoItems = JsonConvert.DeserializeObject<List<ToDoItem>>(jsonString);

                foreach (var toDoItem in toDoItems)
                {
                    tempToDoItems.Add(toDoItem);
                }
            }
            return tempToDoItems;
        }

        public void SerializeAllItems(IEnumerable<ToDoItem> todoItems)
        {
            var jsonString = JsonConvert.SerializeObject(todoItems, Formatting.Indented);

            File.WriteAllText(jsonPath, jsonString);
        }
    }
}
