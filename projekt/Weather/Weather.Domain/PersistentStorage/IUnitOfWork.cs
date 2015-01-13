using Microsoft.AspNet.Identity;
using System;
using Weather.Domain.Entities;

namespace Weather.Domain.PersistentStorage
{
    /// <summary>
    /// Interface in UnitOfWork pattern. Make all repositories share the same context object.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Search> SearchRepository { get; }
        IRepository<Place> PlaceRepository { get; }
        IRepository<Forecast> ForecastRepository { get; }
        UserManager<User> UserManager { get; }

        void Save();
    }
}
