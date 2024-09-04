using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using CustomsTransactionSystem.Models;

namespace CustomsTransactionSystem.Controllers
{
    public class CustomController : Controller
    {
        private readonly string _connectionString = "data source=DESKTOP-OO9SAI1\\SQLEXPRESS02; database=WPF; integrated security=SSPI;";

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Verify(Custom acc)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input.";
                return View("Login");
            }

            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                using (var com = new SqlCommand("SELECT COUNT(1) FROM tbl_login WHERE username = @username AND password = @password", con))
                {
                    com.Parameters.AddWithValue("@username", acc.Name);
                    com.Parameters.AddWithValue("@password", acc.Password);

                    var userExists = (int)com.ExecuteScalar() > 0;

                    if (userExists)
                    {
                        return RedirectToAction("AddOrEdit", "Transaction");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Incorrect username or password";
                        return View("Login");
                    }
                }
            }
        }
    }
}
