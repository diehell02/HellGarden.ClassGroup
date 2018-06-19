using HellGarden.ClassGroup.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace HellGarden.ClassGroup.Contracts.Interface
{
    public interface IClass
    {
        /// <summary>
        /// 编号
        /// </summary>
        int ID
        {
            get;
        }

        /// <summary>
        /// 学生
        /// </summary>
        IStudent[] Students
        {
            get;
        }

        IDictionary<WeightProperty, double> Avgs
        {
            get;
        }

        void InitAvgs(IStudent[] Students = null);
    }
}
