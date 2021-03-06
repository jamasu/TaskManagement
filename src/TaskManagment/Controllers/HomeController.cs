﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TaskManagerDbDAL;
using TaskManagment.Model;
using TaskManagment.Repository;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TaskManagment.Controllers
{
    public class HomeController : Controller
    {     
        private readonly TaskRepository<TaskHandler> repo;
        public HomeController(TaskRepository<TaskHandler> taskrepo)
        {
            repo = taskrepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public List<TaskHandler> ListOfActiveTasks()
        {
            var tasks = repo.Find(t => !t.Checked).OrderByDescending(t => t.Id).ToList();

            return tasks;
        }
        public IActionResult ListActiveTasks()
        {
            var tasks =  repo.Find(t=> !t.Checked).OrderByDescending(t => t.Id).ToList();

            ViewData.Model = tasks;
            
            return View();
        }
        
        public IActionResult Delete(string id)
        {
             var task = repo.Get(Int32.Parse(id));
             repo.Remove(task);
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

                await repo.Add(job);
                return RedirectToAction("Index");
            }
            return CreateTask();
        }

        public IActionResult Details(string id)
        {
            var task =  repo.Get(Int32.Parse(id));
            ViewData.Model = task;
            return View();
        }
        public IActionResult ListCompletedTasks()
        {
            var tasks = repo.Find(t => t.Checked);

            ViewData.Model = tasks;
            return View("ListCompletedTasks");
        }
        public IActionResult Edit(string id)
        {
            var task = repo.Get(Int32.Parse(id));
            var model = new TaskModel
            {
                Id = task.Id,
                TaskName = task.TaskName,
                TaskMessage = task.TaskMessage,
                CompleteDate = task.CompleteDate,
                CreateDate = task.CreateDate,
                DueDate = task.DueDate,
                Checked = task.Checked
            };
            ViewData.Model = model;
            return View("Update");
        }

        [HttpPost]
        public IActionResult Update(string id, TaskModel model)
        {
         
            if (ModelState.IsValid)
            {
                var task = repo.Get(model.Id);
                task.TaskMessage = model.TaskMessage;
                task.TaskName = model.TaskName;
                task.CompleteDate = model.CompleteDate;
                task.CreateDate = model.CreateDate;
                task.DueDate = model.DueDate;
                task.Checked = model.Checked;
                repo.Update(task);

                ViewData.Model = ListOfActiveTasks();
                return View("ListActiveTasks");
            }
            return View("Index"); //something went wrong
        }
        
        public  IActionResult Done(string id)
        {
            var task =  repo.Get(Int32.Parse(id));
            task.Checked = true;
            repo.Update(task);
        
            return RedirectToAction("ListCompletedTasks");
        }
        
    }
}
