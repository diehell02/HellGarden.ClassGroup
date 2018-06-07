using HellGarden.ClassGroup.Contracts.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellGarden.ClassGroup.GroupClassLibrary.Util
{
    class MathUtil
    {
        public static double Variance<T>(List<T> list, Func<T, double> func)
        {
            double sum = 0;
            double avg = 0;
            double[] values = new double[list.Count];

            for(int i = 0; i < list.Count; i++)
            {
                var item = list[i];

                values[i] = func.Invoke(item);
            }

            avg = Average(values);

            foreach(var value in values)
            {
                sum += Math.Pow(value - avg, 2);
            }

            return sum / values.Length;
        }

        public static double Average(double[] array)
        {
            double sum = 0;
            foreach (var item in array)
            {
                sum += item;
            }
            return sum / array.Length;
        }
    }
}
