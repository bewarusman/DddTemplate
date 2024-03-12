using Application.Employees;
using Domain.EmployeeContext;
using MediatR;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Common;

namespace Web.Controllers
{
    public class EmployeeSalaryController : BaseController
    {
        // GET: EmployeeSalaryController
        public ActionResult Index()
        {
            return View();
        }

        // GET: EmployeeSalaryController/Details/5
        public async Task <IActionResult> Details(string id)
        {
            var query = new FindEmployeeSalaryByIdQuery { id = id };
            var result = await Mediator.Send(query);
            var salary = result.GetValue<List<EmployeeSalary>>();
            return View(salary);
        }

        // GET: EmployeeSalaryController/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: EmployeeSalaryController/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeSalaryCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            var result = await Mediator.Send(command);
            if (result.Succeeded)
                return RedirectToAction("Index", "Employees");
            else
                return RedirectToAction("Index", "Employees");
        }

        // GET: EmployeeSalaryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeSalaryController/Edit/5
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

        // GET: EmployeeSalaryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeSalaryController/Delete/5
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
