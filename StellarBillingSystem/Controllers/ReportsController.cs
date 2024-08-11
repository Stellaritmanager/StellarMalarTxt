
using StellarBillingSystem.Business;
using StellarBillingSystem.Context;
using StellarBillingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StellarBillingSystem.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {

        private BillingContext _billingContext;

        public ReportsController(BillingContext billingContext)
        {
            _billingContext = billingContext;

        }

        [HttpPost]
        public IActionResult GetReports(String inputValue,string fromDate,string toDate,string GroupBy)
        {
            BusinessClassBilling business = new BusinessClassBilling(_billingContext);
            ViewData["reportid"] = business.GetReportId();

           
            var reportQuery = (from rep in _billingContext.ShGenericReport
                               where (rep.ReportName == inputValue)
                               select new GenericReportModel
                               {
                                   ReportName = rep.ReportName,
                                   ReportQuery = rep.ReportQuery,
                                   ReportDescription = rep.ReportDescription,
                                   Datecolumn = rep.Datecolumn,
                                   GroupBy = rep.GroupBy
                               }).First();

           

            var query = BusinessClassCommon.DataTableReport(_billingContext, reportQuery.ReportQuery, reportQuery.Datecolumn, fromDate, toDate,reportQuery.GroupBy);



            ViewBag.Reportname = reportQuery.ReportName;


            return View("Reports", query);
        }


        public IActionResult Reports()
        {
            BusinessClassBilling business = new BusinessClassBilling(_billingContext);
            ViewData["reportid"] = business.GetReportId();


            return View();
        }


    }
}
