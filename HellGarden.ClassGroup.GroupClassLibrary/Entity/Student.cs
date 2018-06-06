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

        public double Chinese
        {
            get;
            set;
        }

        public double Math
        {
            get;
            set;
        }

        public double English
        {
            get;
            set;
        }

        public double Physics
        {
            get;
            set;
        }

        public double Chemistry
        {
            get; set;
        }

        public double Biology
        {
            get; set;
        }

        public bool IsMale
        {
            get;
            set;
        }

        public string Sex
        {
            get;
            set;
        }

        public bool IsLodge
        {
            get;
            set;
        }

        public string Lodge
        {
            get;
            set;
        }

        public bool IsDowntown
        {
            get;
            set;
        }

        public string Hometown
        {
            get;
            set;
        }
    }
}
