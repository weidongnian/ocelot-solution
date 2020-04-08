using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        static List<Student> list = new List<Student>() {
              new Student(){ ID = "001", StudentName = "学生1", StudentAge = 16 },
              new Student(){ ID = "002", StudentName = "学生2", StudentAge = 18 },
              new Student(){ ID = "003", StudentName = "学生3", StudentAge = 17 }
          };
 
         [HttpGet]
         public List<Student> GetList()
         {
             return list;
         }
 
         [HttpGet]
         public Student GetModel(string id)
         {
             return list.Find(t => t.ID == id);
         }
    }

    public class Student
    {
        public string ID { get; set; }
        public string StudentName { get; set; }
        public int StudentAge { get; set; }
    }
}