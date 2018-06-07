using HellGarden.ClassGroup.Contracts.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace HellGarden.ClassGroup.GroupClassLibrary.Entity
{
    class Weight
    {
        public Weight(string propertyName, Func<IStudent[], double> func)
        {
            this.PropertyName = propertyName;
            this.Limit = 0;
            this.Multiple = 1;
            this.Func = func;
        }

        public Weight(string propertyName, int limit, int multiple, Func<IStudent[], double> func)
        {
            this.PropertyName = propertyName;
            this.Limit = limit;
            this.Multiple = multiple;
            this.Func = func;
        }

        public string PropertyName
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
