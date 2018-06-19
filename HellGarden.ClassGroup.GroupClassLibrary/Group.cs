using HellGarden.ClassGroup.Contracts.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HellGarden.ClassGroup.GroupClassLibrary.Entity;
using HellGarden.ClassGroup.GroupClassLibrary.Util;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;
using HellGarden.ClassGroup.Contracts.Enum;

namespace HellGarden.ClassGroup.GroupClassLibrary
{
    public class Group : IGroup
    {
        double minWeight = double.MaxValue;
        List<IClass> result = null;
        Dictionary<WeightProperty, double> weightDic = new Dictionary<WeightProperty, double>();
        object lockObj = new object();

        List<Weight> propertyList = null;

        Random random = new Random();

        public Group()
        {
            propertyList = new List<Weight>()
            {
                new Weight(WeightProperty.Chinese, 10, 10, Student.GetChineseAvg),
                new Weight(WeightProperty.Math, 10, 10, Student.GetMathAvg),
                new Weight(WeightProperty.English, 10, 10, Student.GetEnglishAvg),
                new Weight(WeightProperty.Physics, 10, 10, Student.GetPhysicsAvg),
                new Weight(WeightProperty.Chemistry, 10, 10, Student.GetChemistryAvg),
                new Weight(WeightProperty.Biology, 10, 10, Student.GetBiologyAvg),
                new Weight(WeightProperty.IsMale, 10, 1000, Student.GetIsMaleAvg),
                new Weight(WeightProperty.IsLodge, 10, 100, Student.GetIsLodgeAvg),
                new Weight(WeightProperty.IsDowntown, 10, 100, Student.GetIsDowntownAvg),
            };
        }

        public List<IClass> Grouping(List<IStudent> students, int classCount, int repeatCount, bool IsMultithreading, Action<string> action = null)
        {
            List<IClass> result = null;

            int count = 0;

            Timer timer = new Timer(30000)
            {
                Enabled = true,
                AutoReset = true,
            };
            int timerCount = 0;

            timer.Elapsed += (object sender, ElapsedEventArgs e) =>
            {
                if(count == 0)
                {
                    return;
                }

                string message = string.Empty;

                switch(timerCount)
                {
                    case 0:
                        message = "玩命在算！";
                        break;
                    case 1:
                        message = "你这电脑有点慢呐……";
                        break;
                    case 2:
                        message = "把电脑卖了吧，可以买根冰棍。";
                        break;
                    case 3:                        
                        message = "表情逐渐凝固。";
                        break;
                    case 4:
                        message = "我已无f**k说。";
                        break;
                    case 5:
                        message = "你自己玩吧，劳资不干了。";
                        break;
                }

                //message += string.Format("\n当前循环还剩余{0}次", count);

                action?.Invoke(message);

                timerCount++;
            };

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            int _repeatCount = repeatCount > 10 ? 10 : repeatCount;

            do
            {
                _repeatCount--;

                action?.Invoke(string.Format("执行第{0}次循环", repeatCount - _repeatCount));

                int studentCount = students.Count / classCount;
                List<IClass> classes = initClasses(students, classCount, studentCount);

                count = 100000;
                while (count > 0)
                {
                    classes = Swap(classes, studentCount, students.Count);

                    if (IsMultithreading)
                    {
                        Task.Run(() =>
                        {
                            result = CalculateWeight(classes);
                        });
                    }
                    else
                    {
                        result = CalculateWeight(classes);
                    }

                    count--;
                }
            } while (_repeatCount > 0 && !IsPass());

            Task.WaitAll();

            stopwatch.Stop();

            timer.Stop();

            return result;
        }

        private bool IsPass()
        {
            foreach(var pair in weightDic)
            {
                if(pair.Value > 10)
                {
                    return false;
                }
            }

            return true;
        }

        private List<IClass> Swap(List<IClass> classes, int studentCount, int studentsCount)
        {
            int index1 = random.Next(0, studentsCount);
            int index2 = random.Next(0, studentsCount);

            int index1_1 = index1 / studentCount;
            if (index1_1 == classes.Count)
            {
                index1_1--;
            }
            int index1_2 = index1 - index1_1 * studentCount - 1;
            if(index1_2 < 0)
            {
                index1_2 = 0;
            }

            int index2_1 = index2 / studentCount;
            if (index2_1 == classes.Count)
            {
                index2_1--;
            }
            int index2_2 = index2 - index2_1 * studentCount - 1;
            if (index2_2 < 0)
            {
                index2_2 = 0;
            }

            IStudent temp = classes[index1_1].Students[index1_2];
            classes[index1_1].Students[index1_2] = classes[index2_1].Students[index2_2];
            classes[index2_1].Students[index2_2] = temp;

            classes[index1_1].InitAvgs();
            classes[index2_1].InitAvgs();

            return classes;
        }

        private List<IClass> initClasses(List<IStudent> students, int classCount, int studentCount)
        {
            //IStudent[] studentsArr = students.ToArray();

            var result = ArrayUtil.SplitArray(students, studentCount);

            return CreateClasses(result);
        }

        private List<IClass> CreateClasses(List<IStudent[]> studentsList)
        {
            List<IClass> classes = new List<IClass>(studentsList.Count);

            for(int i = 0; i < studentsList.Count; i++)
            {
                classes.Add(new Class(i, studentsList[i]));
            }

            return classes;
        }

        private List<IClass> CalculateWeight(List<IClass> classes)
        {
            double sumVariance = 0;
            Dictionary<WeightProperty, double> _weightDic = new Dictionary<WeightProperty, double>();

            propertyList.ForEach(weight =>
            {
                var weightProperty = weight.WeightProperty;
                double[] values = new double[classes.Count];

                for(int i = 0; i < classes.Count; i++)
                {
                    values[i] = classes[i].Avgs[weightProperty];
                }

                double variance = MathUtil.Variance(values);

                _weightDic.Add(weightProperty, variance);

                if (variance > weight.Limit)
                {
                    variance *= weight.Multiple;
                }

                sumVariance += variance;
            });

            lock (lockObj)
            {
                if (sumVariance < minWeight)
                {
                    weightDic = _weightDic;
                    minWeight = sumVariance;
                    result = classes;
                }
            }

            return result;
        }
    }
}
