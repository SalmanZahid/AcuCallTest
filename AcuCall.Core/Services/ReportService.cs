using AcuCall.Core.Interfaces;
using AcuCall.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AcuCall.Core.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public List<Report> GetReportByMonth(int month, int year)
        {
            var sessions = _reportRepository.GetSessionReport(month, year);
            List<Report> sessionReport = GenerateReportDates(month, year);

            foreach (var item in sessionReport)
            {
                item.MaxUsers = sessions.Where(x => x.Date >= item.Date && x.Date < item.Date.AddDays(7)).Sum(x => x.MaxUsers);
            }

            return sessionReport;
        }

        private List<Report> GenerateReportDates(int month, int year)
        {
            List<Report> reportDates = new List<Report>();
            int totalDays = DateTime.DaysInMonth(year, month);
            DateTime fromDate = new DateTime(year, month, 1);
            DateTime toDate = new DateTime(year, month, totalDays);

            for (int i = 1; i <= totalDays; i += 7)
            {
                reportDates.Add(new Report() { Date = new DateTime(year, month, i) });
            }
       
            return reportDates;
        }       
    }
}
