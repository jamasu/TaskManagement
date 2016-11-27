using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TaskManagerDbDAL;
using TaskManagment.Model;

namespace TaskManagment.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskManagerDbContext _db;

        public HomeController(TaskManagerDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListActiveTasks()
        {
            var tasks = await _db.Tasks.Where(t=> !t.Checked).OrderByDescending(t => t.Id).ToListAsync();

            ViewData.Model = tasks;
            return View();
        }

        public async Task<IActionResult> Delete(string id)
        {
             var task = await _db.Tasks.FindAsync(Int32.Parse(id));
             _db.Tasks.Remove( task);
            await _db.SaveChangesAsync();
            ViewData.Model = task;
            return View("Deleted");
        }

        public IActionResult CreateTask()
        {

            return View();
        }
        [HttpPost]
      
        public async Task<IActionResult> CreateTask(TaskModel model)
        {

            if (ModelState.IsValid)
            {
                var job = new TaskHandler
                {
                    TaskMessage = model.TaskMessage,
                    TaskName = model.TaskName,
                    CompleteDate = DateTime.Now.AddDays(10), 
                    CreateDate = DateTime.Now,
                    DueDate = DateTime.Now,
                    Checked = false
                };
                _db.Tasks.Add(job);
                await _db.SaveChangesAsync();
                return  RedirectToAction("Index");
            }
            return CreateTask();
        }

        public async Task<IActionResult> Details(string id)
        {
            var task = await _db.Tasks.FindAsync(Int32.Parse(id));
            ViewData.Model = task;
            return View();
        }
        public IActionResult ListCompletedTasks()
        {
            var tasks = _db.Tasks.Where(t => t.Checked);

            ViewData.Model = tasks;
            return View("ListCompletedTasks");
        }

        public async Task<IActionResult> Done(string id)
        {
            var task = await _db.Tasks.FindAsync(Int32.Parse(id));
            task.Checked = true;
            _db.Tasks.AddOrUpdate(task);
            await _db.SaveChangesAsync();
            return RedirectToAction("ListCompletedTasks");
        }
    }
}
