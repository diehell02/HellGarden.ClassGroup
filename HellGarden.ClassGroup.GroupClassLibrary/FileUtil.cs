using HellGarden.ClassGroup.Contracts.Interface;
using HellGarden.ClassGroup.GroupClassLibrary.Config;
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
        List<string> headers = null;

        public List<IStudent> Load(string path)
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
                IRow headerRow = sheet.GetRow(0);

                if(headerRow != null)
                {
                    headers = new List<string>();

                    foreach (var cell in headerRow)
                    {
                        headers.Add(cell.StringCellValue);
                    }
                }

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
                        //Name = cells.GetCell(0).StringCellValue,
                        //Chinese = cells.GetCell(1).NumericCellValue,
                        //Math = cells.GetCell(2).NumericCellValue,
                        //English = cells.GetCell(3).NumericCellValue,
                        //Physics = cells.GetCell(4).NumericCellValue,
                        //Chemistry = cells.GetCell(5).NumericCellValue,
                        //Biology = cells.GetCell(6).NumericCellValue,
                        //Score = cells.GetCell(7).NumericCellValue,
                        //IsMale = cells.GetCell(9).StringCellValue == "男" ? true : false,
                        //Sex = cells.GetCell(9).StringCellValue,
                        //IsLodge = cells.GetCell(10).StringCellValue == "寄宿" ? true: false,
                        //Lodge = cells.GetCell(10).StringCellValue,
                        //IsDowntown = cells.GetCell(8).StringCellValue == "市直" ? true : false,
                        //Hometown = cells.GetCell(8).StringCellValue,
                    };

                    for(int i = 0; i < cells.LastCellNum; i++)
                    {
                        student.RawValues.Add(cells.GetCell(i));
                    }

                    for (int i = 0; i < WeightConfig.Weights.Length; i++)
                    {
                        var weight = WeightConfig.Weights[i];

                        double value = 0;
                        ICell cell = cells.GetCell(weight.Index);

                        switch (weight.Type)
                        {
                            case WeightType.Score:
                                if(cell.CellType == CellType.Numeric)
                                {
                                    value = cell.NumericCellValue;
                                }                                
                                break;
                            case WeightType.Enum:
                                if(cell.CellType == CellType.String)
                                {
                                    value = cells.GetCell(weight.Index).StringCellValue == weight.EnumValue ? 1 : 0;
                                }                                
                                break;
                        }

                        student.WeightValues[weight.ID] = value;
                    }

                    students.Add(student);
                }
            }

            return students;
        }

        public void Save(string path, List<IClass> classes)
        {
            IWorkbook workbook = null;

            var fs = File.OpenWrite(path);

            if (path.IndexOf(".xlsx") > 0) // 2007版本 
            {
                workbook = new XSSFWorkbook();
            }
            else if (path.IndexOf(".xls") > 0) // 2003版本
            {
                workbook = new HSSFWorkbook();
            }

            classes.ForEach(_class =>
            {
                ISheet sheet = workbook.CreateSheet(string.Format("班级{0}", _class.ID));

                int rowIndex = 0;
                int colIndex = 0;

                var row = sheet.CreateRow(rowIndex++);

                headers.ForEach(header =>
                {
                    row.CreateCell(colIndex++).SetCellValue(header);
                });

                var students = _class.Students;

                foreach(var student in students)
                {
                    colIndex = 0;
                    row = sheet.CreateRow(rowIndex++);

                    for(int i = 0; i < student.RawValues.Count; i++)
                    {
                        var value = student.RawValues[i];

                        if(value is ICell)
                        {
                            ICell cell = value as ICell;
                            switch (cell.CellType)
                            {
                                case CellType.String:
                                    row.CreateCell(colIndex++).SetCellValue(cell.StringCellValue);
                                    break;
                                case CellType.Numeric:
                                    row.CreateCell(colIndex++).SetCellValue(cell.NumericCellValue);
                                    break;
                            }
                        }
                        else
                        {
                            row.CreateCell(colIndex++).SetCellValue(value.ToString());
                        }                        
                    }

                    //row.CreateCell(colIndex++).SetCellValue(student.Name);
                    //row.CreateCell(colIndex++).SetCellValue(student.Chinese);
                    //row.CreateCell(colIndex++).SetCellValue(student.Math);
                    //row.CreateCell(colIndex++).SetCellValue(student.English);
                    //row.CreateCell(colIndex++).SetCellValue(student.Physics);
                    //row.CreateCell(colIndex++).SetCellValue(student.Chemistry);
                    //row.CreateCell(colIndex++).SetCellValue(student.Biology);
                    //row.CreateCell(colIndex++).SetCellValue(student.Score);
                    //row.CreateCell(colIndex++).SetCellValue(student.Hometown);
                    //row.CreateCell(colIndex++).SetCellValue(student.Sex);
                    //row.CreateCell(colIndex++).SetCellValue(student.Lodge);
                }
            });

            workbook.Write(fs);

            fs.Close();
        }
    }
}
