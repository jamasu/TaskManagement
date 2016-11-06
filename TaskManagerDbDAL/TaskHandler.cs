using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace TaskManagerDbDAL
{
    public class TaskHandler
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string TaskMessage { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CompleteDate { get; set; }


    }
}
