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

namespace HellGarden.ClassGroup.GroupClassLibrary
{
    public class Group : IGroup
    {
        double minWeight = double.MaxValue;
        List<IClass> result = null;
        Dictionary<string, double> weightDic = new Dictionary<string, double>();
        object lockObj = new object();

        List<Weight> propertyList = null;

        Random random = new Random();

        public Group()
        {
            propertyList = new List<Weight>()
            {
                new Weight("Chinese", 10, 10, Student.GetChineseAvg),
                new Weight("Math", 10, 10, Student.GetMathAvg),
                new Weight("English", 10, 10, Student.GetEnglishAvg),
                new Weight("Physics", 10, 10, Student.GetPhysicsAvg),
                new Weight("Chemistry", 10, 10, Student.GetChemistryAvg),
                new Weight("Biology", 10, 10, Student.GetBiologyAvg),
                new Weight("IsMale", 10, 1000, Student.GetIsMaleAvg),
                new Weight("IsLodge", 10, 100, Student.GetIsLodgeAvg),
                new Weight("IsDowntown", 10, 100, Student.GetIsDowntownAvg),
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

                message += string.Format("\n当前循环还剩余{0}次", count);

                action?.Invoke(message);

                timerCount++;
            };

            //Stopwatch stopwatch = new Stopwatch();

            //stopwatch.Start();

            int _repeatCount = repeatCount > 10 ? 10 : repeatCount;

            do
            {
                _repeatCount--;

                action?.Invoke(string.Format("执行第{0}次循环", repeatCount - _repeatCount));

                count = 100000;
                while (count > 0)
                {
                    List<IStudent> _students = Swap(students);

                    List<IStudent[]> studentsList = initClasses(_students, classCount);

                    if (IsMultithreading)
                    {
                        Task.Run(() =>
                        {
                            result = CalculateWeight(studentsList);
                        });
                    }
                    else
                    {
                        result = CalculateWeight(studentsList);
                    }

                    count--;
                }
            } while (_repeatCount > 0 && !IsPass());

            Task.WaitAll();

            //stopwatch.Stop();

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

        private List<IStudent> Swap(List<IStudent> students)
        {
            List<IStudent> _students = students;

            int count = _students.Count;

            int index1 = random.Next(0, count);
            int index2 = random.Next(0, count);

            IStudent temp = _students[index1];
            _students[index1] = _students[index2];
            _students[index2] = temp;

            return _students;
        }

        private List<IStudent[]> initClasses(List<IStudent> students, int classCount)
        {
            int studentCount = students.Count / classCount;

            //IStudent[] studentsArr = students.ToArray();

            var result = ArrayUtil.SplitArray(students, studentCount);

            return result;
        }

        private List<IClass> CreateClasses(List<IStudent[]> studentsList)
        {
            List<IClass> classes = new List<IClass>(studentsList.Count);

            for(int i = 0; i < studentsList.Count; i++)
            {
                classes.Add(new Class() { ID = i, Students = studentsList[i] });
            }

            return classes;
        }

        private List<IClass> CalculateWeight(List<IStudent[]> studentsList)
        {
            double sumVariance = 0;
            Dictionary<string, double> _weightDic = new Dictionary<string, double>();

            propertyList.ForEach(weight =>
            {
                string propertyName = weight.PropertyName;

                double variance = MathUtil.Variance(studentsList, weight.Func);

                _weightDic.Add(propertyName, variance);

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
                    result = CreateClasses(studentsList);
                }
            }

            return result;
        }
    }
}
