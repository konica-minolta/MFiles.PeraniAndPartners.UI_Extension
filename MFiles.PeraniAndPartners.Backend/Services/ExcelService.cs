using MFiles.PeraniAndPartners.Backend.Models;
using MimeKit;
using OfficeOpenXml;

namespace MFiles.PeraniAndPartners.Backend.Services
{
    public class ExcelService
    {
        public static byte[] ListToExcel<T>(List<T> query)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                //Create the worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Result");

                //get our column headings
                var t = typeof(T);
                var Headings = t.GetProperties();
                for (int i = 0; i < Headings.Count(); i++)
                {
                    ws.Cells[1, i + 1].Value = Headings[i].Name;
                }
                //populate our Data
                if (query.Count() > 0)
                {
                    ws.Cells["A2"].LoadFromCollection(query);
                    ws.Column(3).Style.Numberformat.Format = "dd/mm/yyyy";
                    ws.Column(4).Style.Numberformat.Format = "dd/mm/yyyy";
                }
                return pck.GetAsByteArray();

            }
        }

        public static async Task<int> ListToExcelAsync<T>(List<T> query, MailSettings mailSettings,string to)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                //Create the worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Result");

                //get our column headings
                var t = typeof(T);
                var Headings = t.GetProperties();
                for (int i = 0; i < Headings.Count(); i++)
                {
                    ws.Cells[1, i + 1].Value = Headings[i].Name;
                }
                //populate our Data
                if (query.Count() > 0)
                {
                    ws.Cells["A2"].LoadFromCollection(query);
                    ws.Column(3).Style.Numberformat.Format = "dd/mm/yyyy";
                    ws.Column(4).Style.Numberformat.Format = "dd/mm/yyyy";
                }
                FileInfo fileInfo = new FileInfo("C:\\Temp\\" + Guid.NewGuid() + ".xlsx");
                MailAttachment ma = new MailAttachment();
                ma.Name = Guid.NewGuid() + ".xlsx";
                ma.Content = pck.GetAsByteArray(); 

                MFiles.PeraniAndPartners.Backend.Services.MailService ms = new MFiles.PeraniAndPartners.Backend.Services.MailService(mailSettings);
                ms.SendMail(to,ma);
                return 200;

            }
        }

    }
}
