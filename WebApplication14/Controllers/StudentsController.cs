using Microsoft.AspNetCore.Mvc;
using WebApplication14.Models;
using WebApplication14.Services;

namespace WebApplication14.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IPermissionService _permissionService;

        public StudentsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<IActionResult> Index()
        {
            if (!await _permissionService.CanRead("Student"))
            {
                return Forbid();
            }
            return View();
            // Logic for displaying students
        }

        public async Task<IActionResult> Create()
        {
            if (!await _permissionService.CanCreate("Student"))
            {
                return Forbid();
            }
            return View(new Student());
            // Logic for creating a student
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student model)
        {
            if (!await _permissionService.CanCreate("Student"))
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                // Logic for saving the student
            }

            return View(model);
        }

        //public async Task<IActionResult> Edit(int id)
        //{
        //    if (!await _permissionService.CanUpdate("Students"))
        //    {
        //        return Forbid();
        //    }

        //    // Logic for editing a student
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Student model)
        //{
        //    if (!await _permissionService.CanUpdate("Students"))
        //    {
        //        return Forbid();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Logic for updating the student
        //    }

        //    return View(model);
        //}

        //public async Task<IActionResult> Delete(int id)
        //{
        //    if (!await _permissionService.CanDelete("Students"))
        //    {
        //        return Forbid();
        //    }

        //    // Logic for deleting a student
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (!await _permissionService.CanDelete("Students"))
        //    {
        //        return Forbid();
        //    }

        //    // Logic for confirming the deletion of a student
        //}
    }

}
