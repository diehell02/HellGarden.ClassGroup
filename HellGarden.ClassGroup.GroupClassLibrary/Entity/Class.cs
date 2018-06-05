using HellGarden.ClassGroup.Contracts.Interface;
using System;
using System.Collections.Generic;
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

        public double GetWeight(string propertyName)
        {
            Type type = typeof(IStudent).GetProperty(propertyName).DeclaringType;

            if(type == typeof(double))
            {
                double sum = 0;

                Students.ForEach(student =>
                {
                    sum += student.Score;
                });

                return sum / (double)Students.Count;
            }
            else if(type == typeof(bool))
            {
                double sum = 0;

                Students.ForEach(student =>
                {
                    sum += (bool)(student.GetType().GetProperty(propertyName).GetValue(student)) == true ? 1 : 0;
                });

                return sum;
            }
            else if (type.IsEnum)
            {
                double sum = 0;

                Students.ForEach(student =>
                {
                    sum += student.Sex == Sex.Male ? 1 : 0;
                });

                return sum;
            }

            return 0;
        }
    }
}
