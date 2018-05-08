using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSupply.Web.Interfaces
{
    public interface IExcelExportService
    {
        byte[] ExportToExcel<T>(IEnumerable<T> data, string worksheetTitle, List<string[]> titles);
    }
}
