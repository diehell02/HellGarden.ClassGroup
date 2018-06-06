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

        public List<IStudent> Students
        {
            get;
            set;
        }

        public static double GetValue(string propertyName, List<IStudent> students)
        {
            PropertyInfo propertyInfo = typeof(Student).GetProperty(propertyName);
            Type type = propertyInfo.PropertyType;

            if (type == typeof(double))
            {
                double sum = 0;

                students.ForEach(student =>
                {
                    sum += (double)(propertyInfo.GetValue(student));
                });

                return sum / (double)students.Count;
            }
            else if(type == typeof(bool))
            {
                double sum = 0;

                students.ForEach(student =>
                {
                    sum += (bool)(propertyInfo.GetValue(student)) == true ? 1 : 0;
                });

                return sum;
            }

            return 0;
        }
    }
}
