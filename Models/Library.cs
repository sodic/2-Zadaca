using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericList;
using Interfaces;
using Models;
using Exceptions;

namespace Interfaces
{
    public interface IToDoRepository
    {
        /// <summary>
        /// Gets ToDoItem for a given ID
        /// </summary>
        /// <param name="toDoID">The requested item's ID</param>
        /// <returns>ToDoItem if found, null otherwise</returns>
        ToDoItem Get(Guid toDoID);

        /// <summary>
        /// Adds new ToDoItem object to the database.
        /// If the object with the same ID already exists,
        /// the method should throw DuplicateToDoItemException 
        /// with the message "duplicate ID: {ID}"
        /// </summary>
        /// <param name="toDoItem"></param>
        void Add(ToDoItem toDoItem);

        /// <summary >
        /// Tries to remove a ToDoItem with given ID from the database .
        /// </summary>
        /// <returns > True if success , false otherwise </ returns >
        bool Remove(Guid toDoID);

        /// <summary >
        /// Updates given ToDoItem in database .
        /// If ToDoItem does not exist , method will add one .
        /// </summary>
        void Update(ToDoItem toDoItem);

        /// <summary >
        /// Tries to mark a ToDoItem as completed in database .
        /// </summary>
        /// <returns > True if success , false otherwise </ returns >
        bool MarkAsCompleted(Guid toDoID);

        /// <summary >
        /// Gets all ToDoItem objects in database , sorted by date created
        /// (descending)
        /// </summary>
        List<ToDoItem> GetAll();

        /// <summary >
        /// Gets all incomplete ToDoItem objects in database
        /// </summary>
        List<ToDoItem> GetActive();

        /// <summary >
        /// Gets all completed ToDoItem objects in database
        /// </summary>
        List<ToDoItem> GetCompleted();

        /// <summary >
        /// Gets all ToDoItem objects in database that apply to the filter
        /// </summary>
        List<ToDoItem> GetFiltered(Func<ToDoItem, bool> filterFunction);
    }
}

namespace Repositories
{
    /// <summary >
    /// Class that encapsulates all the logic for accessing TodoTtems .
    /// </summary>
    public class TodoRepository : IToDoRepository
    {
        private List<ToDoItem> _dataBase = new List<ToDoItem>();


        public ToDoItem Get(Guid toDoId)
        {
            return _dataBase.FirstOrDefault(x => x.ID == toDoId);
        }

        public void Add(ToDoItem item)
        {
            if (item == null)
                throw new ArgumentNullException();
            if (_dataBase.Count(x => x.ID == item.ID) != 0)
                throw new DuplicateTodoItemException();
            _dataBase.Add(item);
        }

        public bool Remove(Guid toDoID)
        {
            if (_dataBase.Count(x => x.ID == toDoID) == 0)
                return false;   //there was no element to remove
            _dataBase.Remove(_dataBase.FirstOrDefault(x => x.ID == toDoID));
            return true;
        }

        public void Update(ToDoItem item)
        {
            if (_dataBase.Count(x => x.ID == item.ID) == 0)
            {
                Add(item);
            }
            else
            {
                _dataBase.Remove(Get(item.ID));
                _dataBase.Add(item);
            }
        }

        public bool MarkAsCompleted(Guid toDoID)
        {
            try
            {
                ToDoItem helper = Get(toDoID);
                if (helper == null)
                    return false;
                Remove(toDoID);
                helper.IsCompleted = true;
                Add(helper);
                return true;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }

        public List<ToDoItem> GetAll()
        {
            return GetFiltered(x => true);
        }

        public List<ToDoItem> GetActive()
        {
            return GetFiltered(x => !x.IsCompleted);
        }

        public List<ToDoItem> GetCompleted()
        {
            return GetFiltered(x => x.IsCompleted); ;
        }

        public List<ToDoItem> GetFiltered(Func<ToDoItem, bool> filterFunction)
        {
            List<ToDoItem> ReturnList = new List<ToDoItem>();
            ReturnList = _dataBase.Where(filterFunction).OrderByDescending(x => x.DateCreated).ToList();
            return ReturnList.Count == 0 ? null : ReturnList;
        }

        /// <summary >
        /// Repository does not fetch ToDoItems from the actual database ,
        /// it uses in memory storage for this excersise .
        /// </summary>
        private readonly IList<ToDoItem> _inMemoryTodoDatabase;

        public TodoRepository(IList<ToDoItem> initialDbState = null)
        {
            _inMemoryTodoDatabase = initialDbState ?? new List<ToDoItem>();
            // Shorter way to write this in C# using ?? operator :
            // _inMemoryTodoDatabase = initialDbState ?? new List < ToDoItem >() ;
            // x ?? y -> if x is not null , expression returns x. Else y.
        }
    }
    // implement ITodoRepository
}
