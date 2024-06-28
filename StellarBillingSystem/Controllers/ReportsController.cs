
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
        public ReportsController(BillingContext _billingContext)
        {
            _billingContext = _billingContext;

        }

        [HttpPost]
        public IActionResult GetReports(String inputValue)
        {
            BusinessClassBilling business = new BusinessClassBilling(_billingContext);
            ViewData["reportid"] = business.GetReportId();
           
            var reportQuery = (from rep in _billingContext.SHReportModel
                               where (rep.ReportName == inputValue)
                               select new ReportModel
                               {
                                   ReportName = rep.ReportName,
                                   ReportQuery = rep.ReportQuery,
                                   ReportDescription = rep.ReportDescription
                               }).First();



            var query = BusinessClassCommon.DataTable(_billingContext, reportQuery.ReportQuery);


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
