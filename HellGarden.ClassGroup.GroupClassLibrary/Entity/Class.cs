using HellGarden.ClassGroup.Contracts.Interface;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HellGarden.ClassGroup.GroupClassLibrary.Entity
{
    public class Class : IClass
    {
        public int ID
        {
            get;
            set;
        }

        public IStudent[] Students
        {
            get;
            set;
        }
    }
}
