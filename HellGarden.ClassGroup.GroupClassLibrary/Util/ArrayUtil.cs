using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellGarden.ClassGroup.GroupClassLibrary.Util
{
    class ArrayUtil
    {
        //拆分任意类型数组  
        public static List<T[]> SplitArray<T>(T[] data, int size)
        {
            List<T[]> list = new List<T[]>();
            for (int i = 0; i < data.Length / size; i++)
            {
                T[] r = new T[size];
                Array.Copy(data, i * size, r, 0, size);
                list.Add(r);
            }
            if (data.Length % size != 0)
            {
                T[] r = new T[data.Length % size];
                Array.Copy(data, data.Length - data.Length % size, r, 0, data.Length % size);
                list.Add(r);
            }
            return list;
        }

        //合并任意类型数据  
        public static T[] MergeArray<T>(List<T[]> arraies)
        {
            List<T> list = new List<T>();
            foreach (T[] item in arraies)
            {
                for (int i = 0; i < item.Length; i++) list.Add(item[i]);
            }
            T[] r = new T[list.Count];
            int n = 0;
            foreach (T x in list)
            {
                r[n++] = x;
            }
            return r;
        }
    }
}
