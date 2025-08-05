using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using StellarBillingSystem.Context;
using StellarBillingSystem.Models;
using StellarBillingSystem_Malar.Business;
using System.Data;

namespace StellarBillingSystem_Malar.Controllers
{
    [ApiController]
    [Route("api/data")]
    public class DataController : Controller
    {
        private BillingContext _billingsoftware;
        private readonly IConfiguration _configuration;


        public DataController(BillingContext billingsoftware, IConfiguration configuration)
        {
            _billingsoftware = billingsoftware;
            _configuration = configuration;
        }

        [HttpGet("userinfo")]
        [Authorize(AuthenticationSchemes = "Jwt")]
        public IActionResult GetBillDetailwindows(string billID, string branchId)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("BillingDBConnection")))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_GetBillDetails_Malar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pBillID", billID);
                    cmd.Parameters.AddWithValue("@pBranchID", branchId);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    if (dt.Rows.Count == 0)
                    {
                        return Json(new { success = false, message = "No data found." });
                    }
                    else
                    {
                        // Convert DataTable to List<Dictionary<string, object>>
                        var list = new List<Dictionary<string, object>>();

                        foreach (DataRow row in dt.Rows)
                        {
                            var dict = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {
                                dict[col.ColumnName] = row[col];
                            }
                            list.Add(dict);
                        }

                        return Json(new { success = true, data = list });
                    }
                }    
            }
        }

    

    public IActionResult Index()
        {
            return View();
        }
    }
}
