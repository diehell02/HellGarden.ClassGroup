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

        public Group()
        {
            propertyList = new List<Weight>()
            {
                new Weight("Chinese", Student.GetChineseAvg),
                new Weight("Math", Student.GetMathAvg),
                new Weight("English", Student.GetEnglishAvg),
                new Weight("Physics", Student.GetPhysicsAvg),
                new Weight("Chemistry", Student.GetChemistryAvg),
                new Weight("Biology", Student.GetBiologyAvg),
                new Weight("IsMale", 3, 100, Student.GetIsMaleAvg),
                new Weight("IsLodge", 3, 50, Student.GetIsLodgeAvg),
                new Weight("IsDowntown", 3, 100, Student.GetIsDowntownAvg),
                //new Weight("Chinese", student => student.Chinese),
                //new Weight("Math", student => student.Math),
                //new Weight("English", student => student.English),
                //new Weight("Physics", student => student.Physics),
                //new Weight("Chemistry", student => student.Chemistry),
                //new Weight("Biology", student => student.Biology),
                //new Weight("IsMale", 3, 100, student => student.IsMale == true ? 1 : 0),
                //new Weight("IsLodge", 3, 50, student => student.IsLodge == true ? 1 : 0),
                //new Weight("IsDowntown", 3, 100, student => student.IsDowntown == true ? 1 : 0),
                //new Weight("Chinese", students => MathUtil.Average(students, student => student.Chinese)),
                //new Weight("Math", students => MathUtil.Average(students, student => student.Math)),
                //new Weight("English", students => MathUtil.Average(students, student => student.English)),
                //new Weight("Physics", students => MathUtil.Average(students, student => student.Physics)),
                //new Weight("Chemistry", students => MathUtil.Average(students, student => student.Chemistry)),
                //new Weight("Biology", students => MathUtil.Average(students, student => student.Biology)),
                //new Weight("IsMale", 3, 100, students => MathUtil.Average(students, student => student.IsMale == true ? 1 : 0)),
                //new Weight("IsLodge", 3, 50, students => MathUtil.Average(students, student => student.IsLodge == true ? 1 : 0)),
                //new Weight("IsDowntown", 3, 100, students => MathUtil.Average(students, student => student.IsDowntown == true ? 1 : 0)),
                //new Weight("Chinese", students => {
                //    double sum = 0;
                //    foreach(var student in students)
                //            {
                //                sum += student.Chinese;
                //            }
                //    return sum / students.Length;
                //}),
                //new Weight("Math", students => {
                //    double sum = 0;
                //    foreach(var student in students)
                //            {
                //                sum += student.Math;
                //            }
                //    return sum / students.Length;
                //}),
                //new Weight("English", students => {
                //    double sum = 0;
                //    foreach(var student in students)
                //            {
                //                sum += student.English;
                //            }
                //    return sum / students.Length;
                //}),
                //new Weight("Physics", students => {
                //    double sum = 0;
                //    foreach(var student in students)
                //            {
                //                sum += student.Physics;
                //            }
                //    return sum / students.Length;
                //}),
                //new Weight("Chemistry", students => {
                //    double sum = 0;
                //    foreach(var student in students)
                //            {
                //                sum += student.Chemistry;
                //            }
                //    return sum / students.Length;
                //}),
                //new Weight("Biology", students => {
                //    double sum = 0;
                //    foreach(var student in students)
                //            {
                //                sum += student.Biology;
                //            }
                //    return sum / students.Length;
                //}),
                //new Weight("IsMale", 3, 100, students => {
                //    double sum = 0;
                //    foreach(var student in students)
                //            {
                //                sum += student.IsMale == true ? 1 : 0;
                //            }
                //    return sum / students.Length;
                //}),
                //new Weight("IsLodge", 3, 50, students => {
                //    double sum = 0;
                //    foreach(var student in students)
                //            {
                //                sum += student.IsLodge == true ? 1 : 0;
                //            }
                //    return sum / students.Length;
                //}),
                //new Weight("IsDowntown", 3, 100, students => {
                //    double sum = 0;
                //    foreach(var student in students)
                //            {
                //                sum += student.IsDowntown == true ? 1 : 0;
                //            }
                //    return sum / students.Length;
                //}),
            };
        }

        public List<IClass> Grouping(List<IStudent> students, int classCount, int repeatCount, bool IsMultithreading, Action<string> action = null)
        {
            List<IClass> result = null;

            int count = repeatCount;

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

                message += string.Format("还剩余{0}次", count);

                action?.Invoke(message);

                timerCount++;
            };

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            while (count > 0)
            {
                List<IStudent> _students = Swap(students);

                List<IStudent[]> studentsList = initClasses(_students, classCount);

                if(IsMultithreading)
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

            Task.WaitAll();

            stopwatch.Stop();

            timer.Stop();

            return result;
        }

        private List<IStudent> Swap(List<IStudent> students)
        {
            List<IStudent> _students = students;

            int count = _students.Count;

            Random random = new Random();
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
            List<IClass> classes = new List<IClass>();

            for(int i = 0; i < studentsList.Count; i++)
            {
                classes.Add(new Class() { ID = i, Students = studentsList[i] });
            }

            return classes;
        }

        private List<IClass> CalculateWeight(List<IStudent[]> studentsList)
        {
            double sumVariance = 0;
            //Dictionary<string, double> _weightDic = new Dictionary<string, double>();

            propertyList.ForEach(weight =>
            {
                string propertyName = weight.PropertyName;

                double variance = MathUtil.Variance(studentsList, weight.Func);

                //_weightDic.Add(propertyName, variance);

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
                    //weightDic = _weightDic;
                    minWeight = sumVariance;
                    result = CreateClasses(studentsList);
                }
            }

            return result;
        }
    }
}
