using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.Models.StoredProcedureModels
{
    public class InsertEmployee
    {
        public string Name { get; set; }
        public string Designation { get; set; }
        public long Mobile { get; set; }
        public string Address { get; set; }
    }
}
