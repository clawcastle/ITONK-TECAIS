using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingControl.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingControl.Controllers
{
    public class HouseholdController : Controller
    {
        private readonly AccountingContext _context;

        public HouseholdController(AccountingContext context)
        {
            _context = context;
        }
        // GET: Household
        public async Task<IActionResult> Index()
        {
            return View(await _context.Households.ToListAsync());
        }

        // GET: Household/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Household/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Household/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Household/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Household/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Household/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Household/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}