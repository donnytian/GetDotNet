using System.Collections.Generic;
using System.Web.Mvc;
using Gdn.Web.Models.Report;
using Microsoft.Reporting.WebForms;

namespace Gdn.Web.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GenerateReport()
        {
            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Content/reports/Countries.rdlc") };
            var customerList = new List<WorldModel>
            {
                new WorldModel("Europe", "SE", "2001", "123"),
                new WorldModel("Europe", "SE", "2002", "1234"),
                new WorldModel("Europe", "SE", "2003", "12345"),

                new WorldModel("Europe", "DE", "2001", "1"),
                new WorldModel("Europe", "DE", "2002", "12"),
                new WorldModel("Europe", "DE", "2003", "123"),

                new WorldModel("Europe", "NE", "2001", "11"),
                new WorldModel("Europe", "NE", "2002", "112"),
                new WorldModel("Europe", "NE", "2003", "1123"),

                new WorldModel("Asia", "CHN", "2001", "1121"),
                new WorldModel("Asia", "CHN", "2002", "112"),
                new WorldModel("Asia", "CHN", "2003", "11231"),
                new WorldModel("Asia", "CHN", "2004", "44433"),

                new WorldModel("Asia", "IND", "2001", "11"),
                new WorldModel("Asia", "IND", "2002", "11211"),
                new WorldModel("Asia", "IND", "2003", "112311"),
            };

            var reportDataSource = new ReportDataSource
            {
                Name = "DataSet1",
                Value = customerList
            };

            localReport.DataSources.Add(reportDataSource);
            const string reportType = "pdf";
            //The DeviceInfo settings should be changed based on the reportType
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx
            const string deviceInfo = "<DeviceInfo>" +
                                      "  <OutputFormat>PDF</OutputFormat>" +
                                      "  <PageWidth>8.5in</PageWidth>" +
                                      "  <PageHeight>11in</PageHeight>" +
                                      "  <MarginTop>0.5in</MarginTop>" +
                                      "  <MarginLeft>1in</MarginLeft>" +
                                      "  <MarginRight>1in</MarginRight>" +
                                      "  <MarginBottom>0.5in</MarginBottom>" +
                                      "</DeviceInfo>";
            string mimeType;
            //string encoding;
            string extension;
            string[] streams;
            Warning[] warnings;
            //Render the report
            var renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out string encoding, out extension, out streams, out warnings);

            // Tell browser to download as a file instead of inline content.
            //Response.AddHeader("content-disposition", "attachment; filename=Country." + extension); 

            return File(renderedBytes, mimeType);
        }
    }
}