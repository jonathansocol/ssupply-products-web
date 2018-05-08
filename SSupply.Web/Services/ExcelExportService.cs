using ClosedXML.Excel;
using SSupply.Web.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SSupply.Web.Services
{
    public class ExcelExportService : IExcelExportService
    {
        public byte[] ExportToExcel<T>(IEnumerable<T> data, string worksheetTitle, List<string[]> titles)
        {
            var workBook = new XLWorkbook();
            var workSheet = workBook.Worksheets.Add(worksheetTitle);

            workSheet.Cell(1, 1).InsertData(titles); 

            if (data != null && data.Any())
            {
                workSheet.Cell(2, 1).InsertData(data);
            }

            workSheet.Columns().AdjustToContents();

            using (var ms = new MemoryStream())
            {
                workBook.SaveAs(ms);

                return ms.ToArray();
            }
        }
    }
}
