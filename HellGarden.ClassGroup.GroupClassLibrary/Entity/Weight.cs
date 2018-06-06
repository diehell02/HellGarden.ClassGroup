using System;
using System.Collections.Generic;
using System.Text;

namespace HellGarden.ClassGroup.GroupClassLibrary.Entity
{
    class Weight
    {
        public Weight(string PropertyName)
        {
            this.PropertyName = PropertyName;
            this.Limit = 0;
            this.Multiple = 1;
        }

        public Weight(string PropertyName, int Limit, int Multiple)
        {
            this.PropertyName = PropertyName;
            this.Limit = Limit;
            this.Multiple = Multiple;
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
    }
}
