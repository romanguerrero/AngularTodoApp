using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

public class DataDTO<T>
{
    public DataDTO(List<T> data)
    {
        this.data = data;
    }
    public List<T> data { get; set; }
}


namespace todo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private TodoService _todoService;
        private readonly ILogger<TodoController> _logger;

        public TodoController(ILogger<TodoController> logger, TodoService todoService)
        {
            _logger = logger;
            _todoService = todoService;
        }

        //* GET: /api/todo
        [HttpGet]
        public async Task<DataDTO<Todo>> Get([FromQuery] bool includeDone = false)
        {
            return new DataDTO<Todo>(await _todoService.FindTodos(includeDone));
        }

        //* GET: /api/todo/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            //* Search database for todo 
            var dto = new DataDTO<Todo>(await _todoService.FindTodoById(id));

            try
            {
                return dto.data[0];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return NotFound("Todo not found!");
        }
                
        //* POST: /api/todo
        [HttpPost]
        public async Task<ActionResult<Todo>> Create(CreateTodoDTO dto)
        {
            //* Create todo 
            var todo = new Todo
            {
                title = dto.title,
                createdDate = DateTime.Now,
                done = false
            };

            //* Insert into database
            var rowsChanged = await _todoService.CreateTodo(todo);

            //* Query database to determine todo.id
            var tmp = await _todoService.FindTodos(false);            
            var newTodo = tmp.First(item => item.title == todo.title);
            todo.id = newTodo.id;        

            if (rowsChanged > 0) 
            {                
                return todo;                
            }                      

            return BadRequest("Failed to POST todo");
        }

        //* PUT: /api/todo
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(UpdateTodoDTO dto, int id)
        {
            var todo = (await GetTodo(id)).Value;

            if (todo == null) return NotFound("Todo not found");

            //* Updates Todo. Returns No Content if successful
            var rowsUpdated = await _todoService.UpdateTodo(dto, id);

            if (rowsUpdated > 0) return NoContent();

            return BadRequest("Failed to UPDATE todo");
        }

        //* PATCH: /api/todo/id
        [HttpPatch("{id}")]
        public async Task<ActionResult<Todo>> Patch(PatchTodoDTO dto, int id)
        {
            var todo = (await GetTodo(id)).Value;

            if (todo == null) return NotFound("Todo not found");

            // * Change todo attributes if applicable, else keeps the same
            if (dto.id != -1) todo.id = dto.id;
            if (dto.title != "") todo.title = dto.title;
            if (dto.done != todo.done) todo.done = dto.done;

            // * Updates patch in database
            var rowsPatched = await _todoService.PatchTodo(todo, id);

            if (rowsPatched > 0) return NoContent();

            return BadRequest("Failed to PATCH todo");
        }

        //* DELETE: /api/todo/id
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var rowsDeleted = await _todoService.DeleteTodo(id);

            if (rowsDeleted > 0) return NoContent();

            return BadRequest("Failed to DELETE todo");
        }

    }
}
