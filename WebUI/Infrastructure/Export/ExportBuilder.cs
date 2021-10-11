using System;
using System.Collections.Generic;
using Calabonga.Xml.Exports;
using Domain.ModelView;
using WebUI.Models;
using Domain.Repository;
using Domain.Entities;

namespace WebUI.Infrastructure
{
    public static class ExportBuilder {
        /// <summary>
        /// Build Excel file
        /// </summary>
        /// <param name="items">product</param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string Build(List<Ttn> items, string title="Без заголовка") {
            var workbook = new Workbook();
            PropertiesBuilder.Apply(workbook);
            StylesBuilder.Apply(workbook);

            // create first worksheet
            var worksheet = new Worksheet("Лист 1");

            worksheet.AddCellWithStyle(0,0, title, StylesBuilder.TitleStyle, 6U);

            // adding headers
            worksheet.AddCellWithStyle(1, 0, "ТТН № ", StylesBuilder.Headerstyle);
            worksheet.AddCellWithStyle(1, 1, "Дата", StylesBuilder.Headerstyle);
            worksheet.AddCellWithStyle(1, 2, "Время", StylesBuilder.Headerstyle);
            worksheet.AddCellWithStyle(1, 3, "Продукция", StylesBuilder.Headerstyle);
            worksheet.AddCellWithStyle(1, 4, "Кол", StylesBuilder.Headerstyle);

            //worksheet.AddCellWithStyle(1, 5, "Цена", StylesBuilder.Headerstyle);
            //worksheet.AddCellWithStyle(1, 6, "Сумма", StylesBuilder.Headerstyle);
            worksheet.AddCellWithStyle(1, 7, "Модель", StylesBuilder.Headerstyle);
            worksheet.AddCellWithStyle(1, 8, "Автобаза", StylesBuilder.Headerstyle);
            worksheet.AddCellWithStyle(1, 9, "Гос. №", StylesBuilder.Headerstyle);
            worksheet.AddCellWithStyle(1, 6, "Водитель", StylesBuilder.Headerstyle);
            worksheet.AddCellWithStyle(1, 5, "Адрес", StylesBuilder.Headerstyle);

            var row = 2;
            foreach (var item in items) {
                worksheet.AddCell(row, 0, item.Num);
                worksheet.AddCell(row, 1, item.Dat.ToString()); 
                worksheet.AddCell(row, 2, item.Tm);
                worksheet.AddCell(row, 3, item.Good);
                worksheet.AddCell(row, 4, item.Kol);

                //worksheet.AddCell(row, 5, item.Price);
                //worksheet.AddCell(row, 6, item.Sm);
                worksheet.AddCell(row, 7, item.Amodel);
                worksheet.AddCell(row, 8, item.Ab);
                worksheet.AddCell(row, 9, item.Gn);
                worksheet.AddCell(row, 5, item.Driv);
                worksheet.AddCell(row, 6, item.Adres);
                row++;
            }

            // appending footer with formulas
            //worksheet.AddCellWithStyle(row, 0, string.Empty, StylesBuilder.SummaryStyle);
            //worksheet.AddCellWithStyle(row, 1, string.Empty, StylesBuilder.SummaryStyle);
            //worksheet.AddCellWithStyle(row, 2, string.Empty, StylesBuilder.SummaryStyle);
            //worksheet.AddCellWithStyle(row, 3, string.Empty, StylesBuilder.SummaryStyle);
            //worksheet.AddCellWithStyle(row, 4, string.Empty, StylesBuilder.SummaryStyle);
            //worksheet.AddCellWithStyleAndFormula(row, 5, 0, "=AVERAGE(R[-" + (row - 1) + "]C:R[-1]C)", StylesBuilder.SummaryStyle);
            //worksheet.AddCellWithStyleAndFormula(row, 6, 0, "=SUM(R[-" + (row - 1) + "]C:R[-1]C)", StylesBuilder.SummaryStyle);

            workbook.AddWorksheet(worksheet);
            return workbook.ExportToXML();
        }
    }
}
