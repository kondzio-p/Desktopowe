using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp.Models
{
    public class TaxObligation
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime DueDate { get; set; }
    }

}
