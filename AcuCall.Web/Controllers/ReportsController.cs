using AcuCall.Core.Interfaces;
using AcuCall.Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AcuCall.Web.Controllers
{
    public class ReportsController : BaseController
    {
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;

        public ReportsController(IReportService reportService, IMapper mapper)
        {
            _reportService = reportService;
            _mapper = mapper;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetReport(int month, int year)
        {
            var report = _reportService.GetReportByMonth(month, year);
            return PartialView("_List", _mapper.Map<List<Report>>(report));
        }
    }
}
