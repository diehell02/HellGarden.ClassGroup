using HellGarden.ClassGroup.Contracts.Enum;
using HellGarden.ClassGroup.Contracts.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace HellGarden.ClassGroup.GroupClassLibrary.Entity
{
    class Weight
    {
        public Weight(WeightProperty weightProperty, Func<IStudent[], double> func)
        {
            this.WeightProperty = weightProperty;
            this.Limit = 0;
            this.Multiple = 1;
            this.Func = func;
        }

        public Weight(WeightProperty weightProperty, int limit, int multiple, Func<IStudent[], double> func)
        {
            this.WeightProperty = weightProperty;
            this.Limit = limit;
            this.Multiple = multiple;
            this.Func = func;
        }

        public WeightProperty WeightProperty
        {
            get;
            set;
        }

        public int Limit
        {
            get;
            set;
        }

        public int Multiple
        {
            get;
            set;
        }

        public Func<IStudent[], double> Func
        {
            get;
            set;
        }
    }
}
