using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using Weather.Domain.Entities;

namespace Weather.Domain.PersistentStorage
{
    /// <summary>
    /// Helper class to make sure only one context object exists
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context = new WeatherDbContext();
        private IRepository<Search> _searchRepository;
        private IRepository<Place> _placeRepository;
        private IRepository<Forecast> _forecastRepository;
        private IRepository<FavouritePlace> _favouritePlaceRepository;
        private UserManager<User> _userManager;

        private bool _dispose = false;

        public IRepository<Search> SearchRepository
        {
            get { return _searchRepository ?? (_searchRepository = new Repository<Search>(_context)); }
        }

        public IRepository<Place> PlaceRepository
        {
            get { return _placeRepository ?? (_placeRepository = new Repository<Place>(_context)); }
        }

        public IRepository<Forecast> ForecastRepository
        {
            get { return _forecastRepository ?? (_forecastRepository = new Repository<Forecast>(_context)); }
        }

        public IRepository<FavouritePlace> FavouritePlaceRepository
        {
            get { return _favouritePlaceRepository ?? (_favouritePlaceRepository = new Repository<FavouritePlace>(_context)); }
        }

        public UserManager<User> UserManager
        {
            get { return _userManager ?? (_userManager = new UserManager<User>(new UserStore<User>(_context))); }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!this._dispose)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this._dispose = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private static UnitOfWork _unitOfWorkInstance;

        public static UnitOfWork getInstance()
        {
            return _unitOfWorkInstance ?? (_unitOfWorkInstance = new UnitOfWork());
        }
    }
}
