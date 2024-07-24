using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StellarBillingSystem.Models;

namespace StellarBillingSystem.Controllers;

[Authorize]
public class HomeController : Controller
{
    








    public IActionResult Index()
    {

        string salesMessage = GetSalesComparison();
        decimal dailySales = GetDailySales();
        decimal dailyPayments = GetDailyPayments();

        ViewBag.SalesMessage = salesMessage;
        ViewBag.DailySales = dailySales.ToString("F2"); 
        ViewBag.DailyPayments = dailyPayments.ToString("F2");
        return View();
    }

    private string GetSalesComparison()
    {
        string result = string.Empty;

        using (SqlConnection conn = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=StellarBilling;Integrated Security=True;Trust Server Certificate=True;"))
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

        using (SqlConnection conn = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=StellarBilling;Integrated Security=True;Trust Server Certificate=True;"))
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

        using (SqlConnection conn = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=StellarBilling;Integrated Security=True;Trust Server Certificate=True;"))
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
    
