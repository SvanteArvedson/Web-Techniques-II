using Microsoft.AspNet.Identity;
using System;
using Weather.Domain.Entities;

namespace Weather.Domain.PersistentStorage
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Search> SearchRepository { get; }
        IRepository<Place> PlaceRepository { get; }
        IRepository<Forecast> ForecastRepository { get; }
        UserManager<User> UserManager { get; }

        void Save();
    }
}
