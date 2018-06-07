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
        /// 总分数
        /// </summary>
        double Score
        {
            get;
        }

        /// <summary>
        /// 语文
        /// </summary>
        double Chinese
        {
            get;
        }

        double Math
        {
            get;
        }        

        double English
        {
            get;
        }        

        double Physics
        {
            get;
        }

        double Chemistry
        {
            get;
        }

        double Biology
        {
            get;
        }

        /// <summary>
        /// 是否男性
        /// </summary>
        bool IsMale
        {
            get;
        }

        string Sex
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

        string Lodge
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

        string Hometown
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
