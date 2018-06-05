using System;
using System.Collections.Generic;
using System.Text;

namespace HellGarden.ClassGroup.Contracts.Interface
{
    public interface IStudent
    {
        /// <summary>
        /// 姓名
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// 分数
        /// </summary>
        double Score
        {
            get;
        }

        /// <summary>
        /// 性别
        /// </summary>
        Sex Sex
        {
            get;
        }

        /// <summary>
        /// 是否住宿
        /// </summary>
        bool IsLodge
        {
            get;
        }

        /// <summary>
        /// 是否市区
        /// </summary>
        bool IsDowntown
        {
            get;
        }
    }

    public enum Sex
    {
        Male = 0,
        Female = 1,
    }
}
