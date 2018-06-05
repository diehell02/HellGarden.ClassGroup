using HellGarden.ClassGroup.Contracts.Interface;
using HellGarden.ClassGroup.GroupClassLibrary.Entity;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HellGarden.ClassGroup.GroupClassLibrary
{
    public class FileUtil
    {
        public static List<IStudent> Load(string path)
        {
            IWorkbook workbook = null;

            var fs = File.OpenRead(path);

            if (path.IndexOf(".xlsx") > 0) // 2007版本 
            {
                workbook = new XSSFWorkbook(fs);
            }                
            else if (path.IndexOf(".xls") > 0) // 2003版本
            {
                workbook = new HSSFWorkbook(fs);
            }                

            ISheet sheet = workbook.GetSheetAt(0);

            List<IStudent> students = null;

            if (sheet != null)
            {
                students = new List<IStudent>();

                int index = 1;

                while(index < sheet.LastRowNum)
                {
                    IRow cells = sheet.GetRow(index++);

                    if(cells is null)
                    {
                        continue;
                    }

                    IStudent student = new Student()
                    {
                        Name = cells.GetCell(0).StringCellValue,
                        Score = cells.GetCell(7).NumericCellValue,
                        Sex = cells.GetCell(9).StringCellValue == "男" ? Sex.Male : Sex.Female,
                        IsLodge = cells.GetCell(10).StringCellValue == "寄宿" ? true: false,
                        IsDowntown = cells.GetCell(8).StringCellValue == "市直" ? true : false,
                    };

                    students.Add(student);
                }
            }

            return students;
        }
    }
}
