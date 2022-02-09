using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AS3_RM5831.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace PressYourLuck.Controllers
{
    //This controller will display a list of audit records.
    public class AuditController : Controller
    {

        private AuditContext _auditContext;
        public AuditController(AuditContext auditContext)
        {
            _auditContext = auditContext;
        }
        //AuditTypes Actions
        public IActionResult Index()
        {
            //All
            //Get infos from database
            var audits = _auditContext.Audits.Include(a => a.AuditType).OrderByDescending(c => c.CreatedDate);
            //save the last Tab inside a session
            SaveTabinSession("Index");

            return View(audits.ToList());
        }
        public IActionResult CashIn()
        {
            //Get infos from database
            var audits = _auditContext.Audits.Include(a => a.AuditType).Where(m => m.AuditTypeId == "CashIn" ).OrderByDescending(c => c.CreatedDate);
            //save the last Tab inside a session
            SaveTabinSession("CashIn");

            return View("Index", audits.ToList());
        }
        public IActionResult CashOut()
        {
            //Get infos from database
            var audits = _auditContext.Audits.Include(a => a.AuditType).Where(m => m.AuditTypeId == "CashOut").OrderByDescending(c => c.CreatedDate);
            //save the last Tab inside a session
            SaveTabinSession("CashOut");

            return View("Index", audits.ToList());
        }
        public IActionResult Lose()
        {
            //Get infos from database
            var audits = _auditContext.Audits.Include(a => a.AuditType).Where(m => m.AuditTypeId == "lose").OrderByDescending(c => c.CreatedDate);
            //save the last Tab inside a session
            SaveTabinSession("Lose");

            return View("Index", audits.ToList());
        }
        public IActionResult Win()
        {
            //Get infos from database
            var audits = _auditContext.Audits.Include(a => a.AuditType).Where(m => m.AuditTypeId == "Win").OrderByDescending(c => c.CreatedDate);
            //save the last Tab inside a session
            SaveTabinSession("Win");

            return View("Index", audits.ToList());
        }


        public void SaveTabinSession(string tab)
        {
            HttpContext.Session.SetString("ActiveTab", tab);
        }

    }
}
