using CRUDwithADONet.Web.DataAccessLayer;
using CRUDwithADONet.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDwithADONet.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Employee_DAL _dal;

        public EmployeeController(Employee_DAL dal)
        {
            _dal = dal;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                employees = _dal.GetAll();
            }
            catch (Exception ex)
            {
                TempData["errorMessae"] = ex.Message;
            }

            return View();
        }
    }
}
