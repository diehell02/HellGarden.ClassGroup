﻿using HellGarden.ClassGroup.Contracts.Interface;
using HellGarden.ClassGroup.Contracts.Enum;
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

        public IDictionary<WeightProperty, double> Avgs
        {
            get;
            set;
        }

        public Class(int id, IStudent[] students)
        {
            this.ID = id;
            this.Students = students;
            InitAvgs(students);
        }

        public void InitAvgs(IStudent[] students = null)
        {
            if(Avgs is null)
            {
                Avgs = new Dictionary<WeightProperty, double>();
            }
            else
            {
                Avgs.Clear();
            }

            if(students is null)
            {
                students = Students;
            }
            
            if(!Avgs.ContainsKey(WeightProperty.Chinese))
            {
                Avgs.Add(WeightProperty.Chinese, Student.GetChineseAvg(students));
            }

            if (!Avgs.ContainsKey(WeightProperty.Math))
            {
                Avgs.Add(WeightProperty.Math, Student.GetMathAvg(students));
            }

            if (!Avgs.ContainsKey(WeightProperty.English))
            {
                Avgs.Add(WeightProperty.English, Student.GetEnglishAvg(students));
            }

            if (!Avgs.ContainsKey(WeightProperty.Physics))
            {
                Avgs.Add(WeightProperty.Physics, Student.GetPhysicsAvg(students));
            }

            if (!Avgs.ContainsKey(WeightProperty.Chemistry))
            {
                Avgs.Add(WeightProperty.Chemistry, Student.GetChemistryAvg(students));
            }

            if (!Avgs.ContainsKey(WeightProperty.Biology))
            {
                Avgs.Add(WeightProperty.Biology, Student.GetBiologyAvg(students));
            }

            if (!Avgs.ContainsKey(WeightProperty.IsDowntown))
            {
                Avgs.Add(WeightProperty.IsDowntown, Student.GetIsDowntownAvg(students));
            }

            if (!Avgs.ContainsKey(WeightProperty.IsLodge))
            {
                Avgs.Add(WeightProperty.IsLodge, Student.GetIsLodgeAvg(students));
            }

            if (!Avgs.ContainsKey(WeightProperty.IsMale))
            {
                Avgs.Add(WeightProperty.IsMale, Student.GetIsMaleAvg(students));
            }
        }
    }
}
