using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AccountingControl.Data;
using TECAIS.AccountingControl.Models;

namespace AccountingControl.Pages
{
    public class HouseViewModel : PageModel
    {
        private readonly AccountingControl.Data.AccountingContext _context;

        public HouseViewModel(AccountingControl.Data.AccountingContext context)
        {
            _context = context;
        }

        public IList<HouseholdModel> HouseholdModel { get;set; }
        public IList<AccountingInformation> Bills { get; set; }

        public async Task OnGetAsync()
        {
            HouseholdModel = await _context.Households.ToListAsync();
            Bills = await _context.Billings.ToListAsync();
        }
    }
}
