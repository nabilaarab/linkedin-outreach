using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinOutReach
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ExcelManager excelManager = new ExcelManager();

            excelManager.loadLinkedinProfiles();
        }
    }
}
