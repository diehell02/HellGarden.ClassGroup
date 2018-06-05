using System;
using System.Collections.Generic;
using System.Text;

namespace HellGarden.ClassGroup.Contracts.Interface
{
    public interface IGroup
    {
        /// <summary>
        /// 分班
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        List<IClass> Grouping(List<IStudent> students, int classCount);
    }
}
