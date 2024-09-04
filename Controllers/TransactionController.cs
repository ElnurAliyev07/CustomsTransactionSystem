using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using CustomsTransactionSystem.Models;
using System.IO;

namespace CustomsTransactionSystem.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionDbContext _context;

        public TransactionController(TransactionDbContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index(string searchString)
        {
            var transactions = from t in _context.Transactions
                               select t;

            if (!String.IsNullOrEmpty(searchString))
            {
                transactions = transactions.Where(t => t.ProductCode == searchString);
            }

            return View(await transactions.ToListAsync());
        }

        // Excel export feature
        public IActionResult ExportToExcel(string searchString)
        {
            IQueryable<Transaction> transactions = _context.Transactions;

            if (!string.IsNullOrEmpty(searchString))
            {
                transactions = transactions.Where(t => t.ProductCode == searchString);
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Create a new Excel package
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                // Create a worksheet
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Transactions");

                // Add headers
                worksheet.Cells[1, 1].Value = "Product Code";
                worksheet.Cells[1, 2].Value = "Product Name";
                worksheet.Cells[1, 3].Value = "Country Name";
                worksheet.Cells[1, 4].Value = "Country Code";
                worksheet.Cells[1, 5].Value = "Quantity";
                worksheet.Cells[1, 6].Value = "Date";

                // Add data rows
                int row = 2;
                foreach (var transaction in transactions)
                {
                    worksheet.Cells[row, 1].Value = transaction.ProductCode;
                    worksheet.Cells[row, 2].Value = transaction.ProductName;
                    worksheet.Cells[row, 3].Value = transaction.CountryName;
                    worksheet.Cells[row, 4].Value = transaction.CountryCode;
                    worksheet.Cells[row, 5].Value = transaction.Miqdar;
                    worksheet.Cells[row, 6].Value = transaction.Tarix;
                    row++;
                }

                // Convert the package to a byte array
                byte[] excelBytes = excelPackage.GetAsByteArray();

                // Return the Excel file
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Transactions.xlsx");
            }
        }


        // GET: Transaction/AddOrEdit
        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Transaction());
            else
                return View(_context.Transactions.Find(id));
        }

        // POST: Transaction/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransactionId,ProductCode,ProductName,CountryName,CountryCode,Miqdar,Tarix")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                if (transaction.TransactionId == 0)
                {
                    transaction.Tarix = DateTime.Now;
                    _context.Add(transaction);
                }
                else
                    _context.Update(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }


        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}