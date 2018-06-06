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
        List<IStudent> Students
        {
            get;
        }

        /// <summary>
        /// 获取数值
        /// </summary>
        /// <returns></returns>
        // double GetValue(string propertyName);
    }
}
