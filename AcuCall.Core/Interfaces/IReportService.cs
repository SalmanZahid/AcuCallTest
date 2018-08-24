using AcuCall.Core.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcuCall.Core.Interfaces
{
    public interface IReportService
    {
        List<Report> GetReportByMonth(int month, int year);
    }
}
