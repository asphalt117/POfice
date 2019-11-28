using System;
using System.Collections.Generic;
using Calabonga.Xml.Exports;
using Domain.Entities;
using Domain.ModelView;
using WebUI.Models;
using Domain.Repository;

namespace WebUI.Infrastructure
{
    public static class ExportBuilder {
        /// <summary>
        /// Build Excel file
        /// </summary>
        /// <param name="items">product</param>
        /// <param name="title"></param>
        /// <returns></returns>
        /// 

        public ActionResult Export()
        {
            var result = string.Empty;
            var wb = new Workbook();

            // properties
            wb.Properties.Author = "ABZ";
            wb.Properties.Created = DateTime.Today;
            wb.Properties.LastAutor = "ABZ";
            wb.Properties.Version = "14";

            // options sheets
            wb.ExcelWorkbook.ActiveSheet = 1;
            wb.ExcelWorkbook.DisplayInkNotes = false;
            wb.ExcelWorkbook.FirstVisibleSheet = 1;
            wb.ExcelWorkbook.ProtectStructure = false;
            wb.ExcelWorkbook.WindowHeight = 800;
            wb.ExcelWorkbook.WindowTopX = 0;
            wb.ExcelWorkbook.WindowTopY = 0;
            wb.ExcelWorkbook.WindowWidth = 600;

            // create style s1 for header
            var s1 = new Style("s1") { Font = new Font { Bold = true, Italic = true, Color = "#FF0000" } };
            wb.AddStyle(s1);

            // create style s2 for header
            var s2 = new Style("s2") { Font = new Font { Bold = true, Italic = true, Size = 12, Color = "#0000FF" } };
            wb.AddStyle(s2);

            var ws3 = new Worksheet("Реестр отпущенной продукции");

            // Adding Headers
            ws3.AddCell(0, 0, "ТТН № ", 0);
            ws3.AddCell(0, 1, "Дата", 0);
            ws3.AddCell(0, 2, "Время", 0);
            ws3.AddCell(0, 3, "Продукция", 0);
            ws3.AddCell(0, 4, "Кол", 0);

            ws3.AddCell(0, 5, "Цена", 0);
            ws3.AddCell(0, 6, "Сумма", 0);
            ws3.AddCell(0, 7, "Модель", 0);
            ws3.AddCell(0, 8, "Автобаза", 0);
            ws3.AddCell(0, 9, "Гос. №", 0);
            ws3.AddCell(0, 10, "Водитель", 0);
            ws3.AddCell(0, 11, "Адрес", 0);

            // get data
            int dm = (int)Session["Month"];

            int year = (int)Session["Year"];

            //Установили 1е число
            DateTime begDt = new DateTime(year, dm, 01, 0, 0, 0);
            //Установили 1е число следующего месяца
            DateTime endDt = begDt.AddMonths(1);
            TtnRepository repo = new TtnRepository();
            List<Ttn> ttn = repo.GetTtn(Cust.CustId, begDt, endDt);

            int totalRows = 0;

            // appending rows with data
            for (int i = 0; i < ttn.Count; i++)
            {
                ws3.AddCell(i + 1, 0, ttn[i].Num, 0);
                ws3.AddCell(i + 1, 1, ttn[i].Dat, 0);
                ws3.AddCell(i + 1, 2, ttn[i].Tm, 0);
                ws3.AddCell(i + 1, 3, ttn[i].Good, 0);
                ws3.AddCell(i + 1, 4, ttn[i].Kol, 0);

                ws3.AddCell(i + 1, 5, ttn[i].Price, 0);
                ws3.AddCell(i + 1, 6, ttn[i].Sm, 0);
                ws3.AddCell(i + 1, 7, ttn[i].Amodel, 0);
                ws3.AddCell(i + 1, 8, ttn[i].Ab, 0);
                ws3.AddCell(i + 1, 9, ttn[i].Gn, 0);
                ws3.AddCell(i + 1, 10, ttn[i].Driv, 0);
                ws3.AddCell(i + 1, 11, ttn[i].Adres, 0);

                totalRows++;
            }
            totalRows++;

            wb.AddWorksheet(ws3);

            // generate xml 
            var workstring = wb.ExportToXML();

            // Send to user file
            return new ExcelResult("Reestr.xlsx", workstring);
        }

        public static string Build(List<Ttn> items, string title="Без заголовка") {
            var workbook = new Workbook();
            PropertiesBuilder.Apply(workbook);
            StylesBuilder.Apply(workbook);

            // create first worksheet
            var worksheet = new Worksheet("Лист 1");

            worksheet.AddCellWithStyle(0,0, title, StylesBuilder.TitleStyle, 6U);

            // adding headers
            worksheet.AddCellWithStyle(1, 0, "Номер заказа", StylesBuilder.Headerstyle);
            worksheet.AddCellWithStyle(1, 1, "Дата заказа", StylesBuilder.Headerstyle);
            worksheet.AddCellWithStyle(1, 2, "Артикул товара", StylesBuilder.Headerstyle);
            worksheet.AddCellWithStyle(1, 3, "Наименование товара", StylesBuilder.Headerstyle);
            worksheet.AddCellWithStyle(1, 4, "Количество", StylesBuilder.Headerstyle);
            worksheet.AddCellWithStyle(1, 5, "Цена", StylesBuilder.Headerstyle);
            worksheet.AddCellWithStyle(1, 6, "Сумма", StylesBuilder.Headerstyle);

            var row = 2;
            foreach (var item in items) {
                worksheet.AddCell(row, 0, item.Id);
                worksheet.AddCell(row, 1, item.OrderDate.ToString());
                worksheet.AddCell(row, 2, item.ProductId);
                worksheet.AddCell(row, 3, item.ProductName);
                worksheet.AddCell(row, 4, item.Quantity);
                worksheet.AddCell(row, 5, item.UnitPrice);
                worksheet.AddCellWithFormula(row, 6, 0, "=RC[-2]*RC[-1]");
                row++;
            }

            // appending footer with formulas
            worksheet.AddCellWithStyle(row, 0, string.Empty, StylesBuilder.SummaryStyle);
            worksheet.AddCellWithStyle(row, 1, string.Empty, StylesBuilder.SummaryStyle);
            worksheet.AddCellWithStyle(row, 2, string.Empty, StylesBuilder.SummaryStyle);
            worksheet.AddCellWithStyle(row, 3, string.Empty, StylesBuilder.SummaryStyle);
            worksheet.AddCellWithStyle(row, 4, string.Empty, StylesBuilder.SummaryStyle);
            worksheet.AddCellWithStyleAndFormula(row, 5, 0, "=AVERAGE(R[-" + (row - 1) + "]C:R[-1]C)", StylesBuilder.SummaryStyle);
            worksheet.AddCellWithStyleAndFormula(row, 6, 0, "=SUM(R[-" + (row - 1) + "]C:R[-1]C)", StylesBuilder.SummaryStyle);

            workbook.AddWorksheet(worksheet);
            return workbook.ExportToXML();
        }
    }
}
