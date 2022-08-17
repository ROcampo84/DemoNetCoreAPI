using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.API.Models
{
    /// <summary>
    /// Employee's summary
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// 
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DateOfJoining { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PhotoFileName { get; set; }
    }
}
