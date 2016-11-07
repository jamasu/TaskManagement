using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            var tasks = await _db.Tasks.ToListAsync();
            ViewData.Model = tasks.OrderByDescending(t => t.Id);
            return View();
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
                    CompleteDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    DueDate = DateTime.Now
                };

                //TODO no need?
             _db.Tasks.Add(job);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
            return CreateTask();
    }

    public IActionResult ListCompletedTasks()
    {
        throw new NotImplementedException();
    }


}
}
