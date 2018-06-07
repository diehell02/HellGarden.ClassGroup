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

        public static double GetChineseAvg(IStudent[] students)
        {
            double sum = 0;
            foreach (var student in students)
            {
                sum += student.Chinese;
            }
            return sum / students.Length;
        }

        public static double GetMathAvg(IStudent[] students)
        {
            double sum = 0;
            foreach (var student in students)
            {
                sum += student.Math;
            }
            return sum / students.Length;
        }

        public static double GetEnglishAvg(IStudent[] students)
        {
            double sum = 0;
            foreach (var student in students)
            {
                sum += student.English;
            }
            return sum / students.Length;
        }

        public static double GetPhysicsAvg(IStudent[] students)
        {
            double sum = 0;
            foreach (var student in students)
            {
                sum += student.Physics;
            }
            return sum / students.Length;
        }

        public static double GetChemistryAvg(IStudent[] students)
        {
            double sum = 0;
            foreach (var student in students)
            {
                sum += student.Chemistry;
            }
            return sum / students.Length;
        }

        public static double GetBiologyAvg(IStudent[] students)
        {
            double sum = 0;
            foreach (var student in students)
            {
                sum += student.Biology;
            }
            return sum / students.Length;
        }

        public static double GetIsMaleAvg(IStudent[] students)
        {
            double sum = 0;
            foreach (var student in students)
            {
                sum += student.IsMale == true ? 1 : 0;
            }
            return sum / students.Length;
        }

        public static double GetIsLodgeAvg(IStudent[] students)
        {
            double sum = 0;
            foreach (var student in students)
            {
                sum += student.IsLodge == true ? 1 : 0;
            }
            return sum / students.Length;
        }

        public static double GetIsDowntownAvg(IStudent[] students)
        {
            double sum = 0;
            foreach (var student in students)
            {
                sum += student.IsDowntown == true ? 1 : 0;
            }
            return sum / students.Length;
        }
    }
}
