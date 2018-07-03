using System;
using System.Collections.Generic;
using System.Text;

namespace HellGarden.ClassGroup.Contracts.Interface
{
    public interface IWeight
    {
        int ID
        {
            get;
        }

        /// <summary>
        /// 列序号
        /// </summary>
        int Index
        {
            get;
        }

        /// <summary>
        /// 名称
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// 类型
        /// </summary>
        WeightType Type
        {
            get;
        }

        /// <summary>
        /// 枚举值
        /// </summary>
        string EnumValue
        {
            get;
        }

        /// <summary>
        /// 方差限制
        /// </summary>
        double Limit
        {
            get;
            set;
        }

        /// <summary>
        /// 倍数
        /// </summary>
        double Multiple
        {
            get;
            set;
        }
    }

    public enum WeightType
    {
        Score,
        Enum
    }
}
