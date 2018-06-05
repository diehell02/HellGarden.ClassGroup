using HellGarden.ClassGroup.GroupClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellGarden.ClassGroup.GroupConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var students = FileUtil.Load(@"C:\Users\diehe\Documents\Visual Studio 2017\Projects\HellGarden.ClassGroup\import.xlsx");

            var group = new Group();

            group.Grouping(students, 14);
        }
    }
}
