using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lm.test.admin.Models.DotnetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lm.test.admin.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
            if(_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.TodoItems.Add(new TodoItem { Name = "Item2" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<PartialViewResult> Index()
        {
            return PartialView(await _context.TodoItems.ToListAsync());
        }

        [HttpGet]
        public async Task<PartialViewResult> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return PartialView(NotFound());
            }

            return PartialView(todoItem);
        }

        [HttpPost]
        public void PostTodoItem(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChangesAsync();
        }
    }
}