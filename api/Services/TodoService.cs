

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using MySqlConnector;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace todo
{
    public class TodoService
    {
        private ILogger<TodoService> _logger;
        private string CONNECTION_STRING = "server=127.0.0.1;user=todo_rw;password=todo_pass;database=todo";

        public TodoService(ILogger<TodoService> logger)
        {
            this._logger = logger;
        }

        public async Task<List<Todo>> FindTodos(bool includeDone)
        {
            using (System.Data.IDbConnection db = new MySqlConnection(this.CONNECTION_STRING))
            {
                var filter = includeDone ? "" : " WHERE done = false ";
                var query = $"SELECT * FROM todo.todos {filter} ORDER BY createdDate DESC";  // Order by createdDate desc
                return (await db.QueryAsync<Todo>(query)).ToList();
            }
        }

        public async Task<int> CreateTodo(Todo todo)
        {
            //* Insert into database
            using (System.Data.IDbConnection connection = new MySqlConnection(this.CONNECTION_STRING))
            {
                connection.Open();
                var sqlStatement = "INSERT INTO todo.todos (title, createdDate, done) VALUES (@title, @createdDate, @done);";
                return await connection.ExecuteAsync(sqlStatement, todo);
            }
        }

        public async Task<int> UpdateTodo(UpdateTodoDTO update, int id)
        {
            //* Update in database    
            using (System.Data.IDbConnection connection = new MySqlConnection(this.CONNECTION_STRING))
            {
                connection.Open();
                var sqlStatement = $"UPDATE todo.todos SET title = @title, id = @id, done = @done WHERE id = {id}";
                return await connection.ExecuteScalarAsync<int>(sqlStatement, update);
            }
        }

        public async Task<int> DeleteTodo(int id)
        {
            //* Delete in database            
            using (System.Data.IDbConnection connection = new MySqlConnection(this.CONNECTION_STRING))
            {

                connection.Open();
                var sqlStatement = "DELETE FROM todo.todos WHERE id = @id";
                return await connection.ExecuteAsync(sqlStatement, new { id = id });
            }
        }

        public async Task<List<Todo>> FindTodoById(int id)
        {
            //* Queries database for specific Todo
            using (System.Data.IDbConnection db = new MySqlConnection(this.CONNECTION_STRING))
            {
                var query = $"SELECT * FROM todo.todos WHERE id = {id}";
                return (await db.QueryAsync<Todo>(query)).ToList();
            }
        }

        public async Task<int> PatchTodo(Todo patch, int id)
        {
            //* Patch in database
            using (System.Data.IDbConnection connection = new MySqlConnection(this.CONNECTION_STRING))
            {
                connection.Open();
                var sqlStatement = $"UPDATE todo.todos SET title = @title, id = @id, done = @done WHERE id = {id}";
                return await connection.ExecuteAsync(sqlStatement, patch);
            }
        }


    }
}