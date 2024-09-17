using ContractClaimSystem.Data;
using ContractClaimSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ContractClaimSystem.Controllers
{
    public class ClaimsController : Controller
    {
        public IActionResult Index()
        {
            return View(ClaimContext.Claims);
        }

        public IActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(Claim model, IFormFile document)
        {
            if (ModelState.IsValid)
            {
                if (document != null && document.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/documents", document.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await document.CopyToAsync(stream);
                    }
                    model.DocumentName = document.FileName;
                }

                model.ClaimID = ClaimContext.Claims.Count + 1;
                ClaimContext.Claims.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Verify(int id)
        {
            var claim = ClaimContext.Claims.FirstOrDefault(c => c.ClaimID == id);
            if (claim == null) return NotFound();
            return View(claim);
        }

        [HttpPost]
        public IActionResult Verify(Claim model, string action)
        {
            var claim = ClaimContext.Claims.FirstOrDefault(c => c.ClaimID == model.ClaimID);
            if (claim == null) return NotFound();

            if (action == "Approve")
                claim.Status = "Approved";
            else if (action == "Reject")
                claim.Status = "Rejected";

            return RedirectToAction("Index");
        }
    }
}

