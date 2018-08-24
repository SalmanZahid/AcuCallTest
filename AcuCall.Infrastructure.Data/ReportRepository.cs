using AcuCall.Core.Interfaces;
using AcuCall.Core.Objects;
using AcuCall.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AcuCall.Infrastructure.Data.Extensions;
using System.Linq;

namespace AcuCall.Infrastructure.Data
{
    public class ReportRepository : IReportRepository
    {
        private readonly Interfaces.IAcuCallsContext _acuCallsContext;

        public ReportRepository(Interfaces.IAcuCallsContext acuCallsContext)
        {
            _acuCallsContext = acuCallsContext;
        }

        public List<Report> GetSessionReport(int month, int year)
        {
            var result = _acuCallsContext.Context.LoadStoredProc("GetUserSessionReport")
                                .WithSqlParam("@Month", month)
                                .WithSqlParam("@Year", year)
                                .ExecuteStoredProc<Report>().ToList();

            return result;
        }
    }
}
