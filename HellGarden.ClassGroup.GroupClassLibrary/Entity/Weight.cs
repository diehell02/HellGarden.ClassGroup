using HellGarden.ClassGroup.Contracts.Enum;
using HellGarden.ClassGroup.Contracts.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace HellGarden.ClassGroup.GroupClassLibrary.Entity
{
    class Weight : IWeight
    {
        public int ID
        {
            get;
            set;
        }

        /// <summary>
        /// 列序号
        /// </summary>
        public int Index
        {
            get; set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public WeightType Type
        {
            get; set;
        }

        /// <summary>
        /// 枚举值
        /// </summary>
        public string EnumValue
        {
            get; set;
        }

        /// <summary>
        /// 方差限制
        /// </summary>
        public double Limit
        {
            get;
            set;
        }

        /// <summary>
        /// 倍数
        /// </summary>
        public double Multiple
        {
            get;
            set;
        }
    }
}
