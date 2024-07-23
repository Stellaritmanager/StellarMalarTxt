using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StellarBillingSystem.Business;
using StellarBillingSystem.Context;
using StellarBillingSystem.Models;

namespace StellarBillingSystem.Controllers;


[Authorize]
public class HomeController : Controller
{

    private BillingContext _billingContext;

    public HomeController(BillingContext billingContext)
    {
        _billingContext = billingContext;

    }







    public IActionResult Index(string DashBoard = null, string fromDate = null, string toDate = null, string GroupBy = null)
    {
        
        string salesMessage = GetSalesComparison();
        decimal dailySales = GetDailySales();
        decimal dailyPayments = GetDailyPayments();

        ViewBag.SalesMessage = salesMessage;
        ViewBag.DailySales = dailySales.ToString("F2");
        ViewBag.DailyPayments = dailyPayments.ToString("F2");

        
        
            BusinessClassBilling business = new BusinessClassBilling(_billingContext);
            ViewData["reportid"] = business.GetReportId();

            var reportQuery = (from rep in _billingContext.ShGenericReport
                               where rep.ReportName == "DashBoard"
                               select new GenericReportModel
                               {
                                   ReportName = rep.ReportName,
                                   ReportQuery = rep.ReportQuery,
                                   ReportDescription = rep.ReportDescription,
                                   Datecolumn = rep.Datecolumn,
                                   GroupBy = rep.GroupBy
                               }).FirstOrDefault();

            if (reportQuery != null)
            {
                var query = BusinessClassCommon.DataTableReport(_billingContext, reportQuery.ReportQuery, reportQuery.Datecolumn, fromDate, toDate, reportQuery.GroupBy);
                ViewBag.Reportname = reportQuery.ReportName;
                return View(query);
            }
        

        return View();
    }


    private string GetSalesComparison()
    {
        string result = string.Empty;

        using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-49S4H3N\\SQLEXPRESS;Initial Catalog=StellarBilling;Integrated Security=True;Trust Server Certificate=True;"))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT dbo.CompareDailySales()", conn))
            {
                result = cmd.ExecuteScalar().ToString();
            }
        }

        return result;
    }

    private decimal GetDailySales()
    {
        decimal result = 0;

        using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-49S4H3N\\SQLEXPRESS;Initial Catalog=StellarBilling;Integrated Security=True;Trust Server Certificate=True;"))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT dbo.GetDailySales()", conn))
            {
                var dbResult = cmd.ExecuteScalar();
                if (dbResult != DBNull.Value)
                {
                    result = Convert.ToDecimal(dbResult);
                }
            }
        }

        return result;
    }

    private decimal GetDailyPayments()
    {
        decimal result = 0;

        using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-49S4H3N\\SQLEXPRESS;Initial Catalog=StellarBilling;Integrated Security=True;Trust Server Certificate=True;"))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT dbo.GetDailyPayments()", conn))
            {
                var dbResult = cmd.ExecuteScalar();
                if (dbResult != DBNull.Value)
                {
                    result = Convert.ToDecimal(dbResult);
                }
            }
        }

        return result;
    }

    public IActionResult Privacy()
    {
        return View();
    }



    public IActionResult RedirectToReports()
    {
       
        return RedirectToAction("Reports", "Reports");
    }

}
    
