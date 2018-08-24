using AcuCall.Core.Interfaces;
using AcuCall.Core.Objects;
using AcuCall.Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace AcuCall.Web.Subscriptions
{
    public class UserSubscription : IDatabaseSubscription
    {
        private bool disposedValue = false;
        private readonly IHubContext<UserHub> _hubContext;
        private SqlTableDependency<User> _tableDependency;

        public UserSubscription(IHubContext<UserHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public void Configure(string connectionString)
        {
            _tableDependency = new SqlTableDependency<User>(connectionString, "Users", null, null, null, null, DmlTriggerType.All);
            _tableDependency.OnChanged += Changed;
            _tableDependency.OnError += TableDependency_OnError;
            _tableDependency.Start();
        }

        private void TableDependency_OnError(object sender, ErrorEventArgs e)
        {
            throw e.Error;
        }

        private void Changed(object sender, RecordChangedEventArgs<User> e)
        {
            if (e.ChangeType != ChangeType.None && e.ChangeType == ChangeType.Delete)
            {
                _hubContext.Clients.All.SendAsync("removedUser", e.Entity.UserId);
            }
            else if (e.ChangeType != ChangeType.None && e.ChangeType == ChangeType.Update)
            {
                _hubContext.Clients.All.SendAsync("updateUser", e.Entity);
            }
            else if (e.ChangeType != ChangeType.None && e.ChangeType == ChangeType.Insert)
            {
                _hubContext.Clients.All.SendAsync("newUser", e.Entity);
            }
        }

        #region IDisposable

        ~UserSubscription()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _tableDependency.Stop();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
