using AcuCall.Core.Objects;
using System.Collections.Generic;

namespace AcuCall.Core.Interfaces
{
    public interface IReportRepository
    {
        List<Report> GetSessionReport(int month, int year);
    }
}
