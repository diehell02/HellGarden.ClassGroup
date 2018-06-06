using HellGarden.ClassGroup.GroupClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellGarden.ClassGroup.GroupConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                FileUtil fileUtil = new FileUtil();

                string importPath = string.Format(@"{0}\import.xlsx", AppDomain.CurrentDomain.BaseDirectory);
                string outportPath = string.Format(@"{0}\outport.xlsx", AppDomain.CurrentDomain.BaseDirectory);

                while (!File.Exists(importPath))
                {
                    Console.WriteLine("“import.xlsx”不存在，请输入导入文件地址：");
                    importPath = Console.ReadLine();
                }

                var students = fileUtil.Load(importPath);

                var group = new Group();
                bool IsMultithreading = false;

                Console.WriteLine("是否开启多线程计算(y/n)：");
                Console.WriteLine("注：内存小于4G不建议开启多线程模式");
                string result = Console.ReadLine();

                if (result == "y")
                {
                    IsMultithreading = true;
                }

                Console.WriteLine("分成几个班：");
                string classCountString = Console.ReadLine();
                int classCount = 0;
                int readCount = 0;

                while (!int.TryParse(classCountString, out classCount))
                {
                    readCount++;

                    if (readCount > 2)
                    {
                        Console.WriteLine("给劳资输入数字：");
                    }
                    else
                    {
                        Console.WriteLine("别闹，请输入数字：");
                    }

                    classCountString = Console.ReadLine();
                }

                Console.WriteLine("循环次数(默认10万次)：");
                string repeatCountString = Console.ReadLine();
                int repeatCount = 0;

                while (!int.TryParse(repeatCountString, out repeatCount))
                {
                    repeatCount = 100000;
                }

                Console.WriteLine("计算中，请勿手贱乱碰...");

                var classes = group.Grouping(students, classCount, repeatCount, IsMultithreading, message =>
                {
                    Console.WriteLine(message);
                });

                fileUtil.Save(outportPath, classes);

                Console.WriteLine("生成完成，按任意键退出");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadKey();
        }
    }
}
