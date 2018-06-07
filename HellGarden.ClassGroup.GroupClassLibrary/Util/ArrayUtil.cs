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

                for(int i = 0; i < r.Length; i++)
                {
                    var _r = r[i];
                    var temp = list[i];
                    var n = new T[temp.Length + 1];

                    Array.Copy(temp, n, temp.Length);
                    n[temp.Length] = _r;

                    list[i] = n;
                }
            }

            return list;
        }

        //拆分任意类型数组  
        public static List<T[]> SplitArray<T>(List<T> data, int size)
        {
            List<T[]> list = new List<T[]>();

            for (int i = 0; i < data.Count / size; i++)
            {
                T[] r = new T[size];
                data.CopyTo(i * size, r, 0, size);
                list.Add(r);
            }

            if (data.Count % size != 0)
            {
                T[] r = new T[data.Count % size];
                data.CopyTo(data.Count - data.Count % size, r, 0, data.Count % size);

                for (int i = 0; i < r.Length; i++)
                {
                    var _r = r[i];
                    var temp = list[i];
                    var n = new T[temp.Length + 1];

                    Array.Copy(temp, n, temp.Length);
                    n[temp.Length] = _r;

                    list[i] = n;
                }
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
