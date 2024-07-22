
using StellarBillingSystem.Business;
using StellarBillingSystem.Context;
using StellarBillingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StellarBillingSystem.Controllers
{
    public class ReportsController : Controller
    {

        private BillingContext _billingContext;

        public ReportsController(BillingContext billingContext)
        {
            _billingContext = billingContext;

        }

        [HttpPost]
        public IActionResult GetReports(String inputValue,string fromDate,string toDate)
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
                               }).First();

           

            var query = BusinessClassCommon.DataTableReport(_billingContext, reportQuery.ReportQuery, reportQuery.Datecolumn, fromDate, toDate);


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
