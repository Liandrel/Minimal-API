using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoLibrary.DataAccess;
using TodoLibrary.Models;

namespace TodoLibrary.Data
{
    public class TodoData : ITodoData
    {
        private readonly ISqlDataAccess _dataAccess;
        private readonly string _connectionStringName = "SqlConnectionString";
        public TodoData(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<List<TodoModel>> GetAllAssigned(int assignedTo)
        {
            return _dataAccess.LoadData<TodoModel, dynamic>("spTodos_GetAllAssigned", new { AssignedTo = assignedTo }, _connectionStringName);
        }

        public async Task<TodoModel?> GetOneAssigned(int assignedTo, int todoId)
        {
            var results = await _dataAccess.LoadData<TodoModel, dynamic>("spTodos_GetOneAssigned", new { AssignedTo = assignedTo, TodoId = todoId }, _connectionStringName);

            return results.FirstOrDefault();
        }

        public async Task<TodoModel?> CreateTodo(int assignedTo, string task)
        {
            var results = await _dataAccess.LoadData<TodoModel, dynamic>("spTodos_Create", new { AssignedTo = assignedTo, Task = task }, _connectionStringName);

            return results.FirstOrDefault();
        }

        public Task UpdateTask(int assignedTo, int todoId, string task)
        {
            return _dataAccess.SaveData<dynamic>("spTodos_UpdateTask", new { AssignedTo = assignedTo, TodoId = todoId, Task = task }, _connectionStringName);
        }

        public Task CompleteTodo(int assignedTo, int todoId)
        {
            return _dataAccess.SaveData<dynamic>("spTodos_CompleteTodo", new { AssignedTo = assignedTo, TodoId = todoId }, _connectionStringName);
        }

        public Task DeleteTodo(int assignedTo, int todoId)
        {
            return _dataAccess.SaveData<dynamic>("spTodos_Delete", new { AssignedTo = assignedTo, TodoId = todoId }, _connectionStringName);
        }
    }
}
