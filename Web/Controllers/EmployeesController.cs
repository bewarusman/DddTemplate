using Application.Employees;
using Domain.EmployeeContext;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using Web.Common;

namespace Web.Controllers
{
    public class EmployeesController : BaseController
    {
        // GET: EmployeeController
        public async Task<IActionResult> Index()
        {
            var query = new FindEmployeesQuery();
            var result = await Mediator.Send(query);
            var employees = result.GetValue<List<Employee>>();
            return View(employees);
        }

        // GET: EmployeeController/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var query = new FindManyEmployeeByIdQuery { id = id };
            var result = await Mediator.Send(query);
            var employee = result.GetValue<List<Employee>>();  
            return View(employee);  
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(CreateEmployeeCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            var result = await Mediator.Send(command);
            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                return View(command);
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
