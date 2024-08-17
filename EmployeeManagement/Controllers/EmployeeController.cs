using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EmployeeManagement.Models;
using Newtonsoft.Json;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {

        private string JsonFilePath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/employees.json");

        // Cargar empleados desde JSON
        private List<Employee> LoadEmployees()
        {
            if (!System.IO.File.Exists(JsonFilePath))
            {
                return new List<Employee>();
            }

            var jsonData = System.IO.File.ReadAllText(JsonFilePath);
            return JsonConvert.DeserializeObject<List<Employee>>(jsonData) ?? new List<Employee>();
        }

        // Guardar empleados en JSON
        private void SaveEmployees(List<Employee> employees)
        {
            var jsonData = JsonConvert.SerializeObject(employees, Formatting.Indented);
            System.IO.File.WriteAllText(JsonFilePath, jsonData);
        }

        // GET: Employee
        public ActionResult Index()
        {
            var employees = LoadEmployees();
            return View(employees);
        }

        // GET: Employee/GetEmployee/{id}
        public ActionResult GetEmployee(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employees = LoadEmployees();
            Employee employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult AddEmployee()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee([Bind(Include = "Id,Name,Position,Office,Salary")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                var employees = LoadEmployees();
                employee.Id = employees.Any() ? employees.Max(e => e.Id) + 1 : 1;
                employees.Add(employee);
                SaveEmployees(employees);
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employee/UpdateEmployee/{id}
        public ActionResult UpdateEmployee(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employees = LoadEmployees();
            Employee employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/UpdateEmployee/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateEmployee([Bind(Include = "Id,Name,Position,Office,Salary")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                var employees = LoadEmployees();
                var employeeInList = employees.FirstOrDefault(e => e.Id == employee.Id);
                if (employeeInList != null)
                {
                    employeeInList.Name = employee.Name;
                    employeeInList.Position = employee.Position;
                    employeeInList.Office = employee.Office;
                    employeeInList.Salary = employee.Salary;
                    SaveEmployees(employees);
                }
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employee/DeleteEmployee/{id}
        public ActionResult DeleteEmployee(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employees = LoadEmployees();
            Employee employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/DeleteEmployee/
        [HttpPost, ActionName("DeleteEmployee")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEmployee(int id)
        {
            var employees = LoadEmployees();
            Employee employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                employees.Remove(employee);
                SaveEmployees(employees);
            }
            return RedirectToAction("Index");
        }
    }
}
