using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    public class TodoController : Controller
    {
        private static List<TodoItemViewModel> _todos = new List<TodoItemViewModel>();

        // GET: Todo
        public IActionResult Index()
        {
            return View(_todos);
        }

        // GET: Todo/Create
        public IActionResult Create()
        {
            var model = new TodoItemViewModel();
            return View(model);
        }


        // POST: Todo/Create
        [HttpPost]
        public IActionResult Create(TodoItemViewModel todoItem)
        {
            if (ModelState.IsValid)
            {
                todoItem.Id = _todos.Count > 0 ? _todos.Max(t => t.Id) + 1 : 1; // Auto-generate the ID
                _todos.Add(todoItem);
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        // GET: Todo/Edit/5
        public IActionResult Edit(int id)
        {
            var todoItem = _todos.FirstOrDefault(t => t.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return View(todoItem);
        }

        // POST: Todo/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, TodoItemViewModel todoItem)
        {
            if (ModelState.IsValid)
            {
                var existingTodo = _todos.FirstOrDefault(t => t.Id == id);
                if (existingTodo != null)
                {
                    existingTodo.TaskName = todoItem.TaskName;
                    existingTodo.Description = todoItem.Description; // Update the description
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(todoItem);
        }

        // GET: Todo/Delete/5
        public IActionResult Delete(int id)
        {
            var todoItem = _todos.FirstOrDefault(t => t.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return View(todoItem);
        }

        // POST: Todo/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            // Find the to-do item by id
            var todoItem = _todos.FirstOrDefault(t => t.Id == id);

            if (todoItem != null)
            {
                // Remove the item from the list
                _todos.Remove(todoItem);
            }

            // Redirect back to the index page
            return RedirectToAction(nameof(Index));
        }

    }
}
