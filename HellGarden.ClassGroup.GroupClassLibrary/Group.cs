using HellGarden.ClassGroup.Contracts.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HellGarden.ClassGroup.GroupClassLibrary.Entity;

namespace HellGarden.ClassGroup.GroupClassLibrary
{
    public class Group : IGroup
    {
        public List<IClass> Grouping(List<IStudent> students, int classCount)
        {
            int count = 100000;
            List<IClass> classes = initClasses(students, classCount);

            while (!CalculateWeight(classes) && count > 0)
            {
                count--;
                Swap(students);

                classes = initClasses(students, classCount);
            }

            if (count == 0)
            {
                return null;
            }

            return classes;
        }

        private List<IStudent> Swap(List<IStudent> students)
        {
            int count = students.Count;

            Random random = new Random();
            int index1 = random.Next(0, count);
            int index2 = random.Next(0, count);

            IStudent temp = students[index1];
            students[index1] = students[index2];
            students[index2] = temp;

            return students;
        }

        private List<IClass> initClasses(List<IStudent> students, int classCount)
        {
            List<IClass> classes = new List<IClass>();
            int id = 0;

            int studentCount = students.Count / classCount;

            int index = studentCount;
            for (int i = 0; i < students.Count; i += studentCount)
            {
                List<IStudent> _students = students.Take(i + studentCount).Skip(i).ToList();

                classes.Add(new Class() { ID = id++, Students = _students });
            }

            return classes;
        }

        private bool CalculateWeight(List<IClass> classes)
        {
            bool flag = true;
            List<string> propertyList = new List<string>()
            {
                "Score", "Sex", "IsLodge", "IsDowntown"
            };

            propertyList.ForEach(propertyName =>
            {
                double min = 0;
                double max = 0;

                classes.ForEach(_class =>
                {
                    double weight = _class.GetWeight(propertyName);

                    if(weight < min)
                    {
                        min = weight;
                    }

                    if (weight > max)
                    {
                        max = weight;
                    }
                });

                if(max - min > 1)
                {
                    flag = false;
                }
            });

            return flag;
        }
    }
}
