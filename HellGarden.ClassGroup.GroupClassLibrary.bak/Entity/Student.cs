using HellGarden.ClassGroup.Contracts.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace HellGarden.ClassGroup.GroupClassLibrary.Entity
{
    class Student : IStudent
    {
        public string Name
        {
            get;
            set;
        }

        public double Score
        {
            get;
            set;
        }

        public Sex Sex
        {
            get;
            set;
        }

        public bool IsLodge
        {
            get;
            set;
        }

        public bool IsDowntown
        {
            get;
            set;
        }
    }
}
