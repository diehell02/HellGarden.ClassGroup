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
        public static double Variance(List<IStudent[]> list, Func<IStudent[], double> func)
        {
            double sum = 0;
            double avg = 0;
            List<double> values = new List<double>();

            list.ForEach(item =>
            {
                double value = func.Invoke(item);
                values.Add(value);

                sum += value;
            });

            avg = sum / (double)list.Count;
            sum = 0;

            values.ForEach(value =>
            {
                sum += Math.Pow(value - avg, 2);
            });

            return sum / (double)values.Count;
        }

        public static double Average(IStudent[] array, Func<IStudent, double> func)
        {
            double sum = 0;
            foreach (var item in array)
            {
                sum += func.Invoke(item);
            }
            return sum / array.Length;
        }
    }
}
