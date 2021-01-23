
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuasarFireOperation.Domain.CommandModel.Repositories;
using QuasarFireOperation.Persistence.Sql.Context;
using QuasarFireOperation.Persistence.Sql.EntitiesRepositories;

namespace QuasarFireOperation.Persistence.Sql
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, SqlRepositoryBase> _Repositories = new Dictionary<Type, SqlRepositoryBase>();

        public SqlUnitOfWork(IConfiguration configuration)
        {
            Configuration = configuration;

            var optionsBuilder = new DbContextOptionsBuilder<QuasarFireOperationContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyDataBase"));
            Database = new PersistentContext(optionsBuilder.Options);
        }
        
        internal PersistentContext Database { get; }
        private IConfiguration Configuration { get; }

        

        public async void Commit()
        {
            await Database.SaveChangesAsync();

            foreach (var repository in _Repositories.Values)
            {
                repository.SyncCoreEntitiesWithSavedSqlEntities();
            }
        }


        #region Satellite

        public ISatelliteRepository SatelliteRepository
        {
            get
            {
                return GetRepository(uow =>
                    new SqlSatelliteRepository(uow));
            }
        }

        #endregion
        /// <summary>
        ///     Returns an instance of the repository of the type requested.
        ///     It uses the Func delegate to call the constructor to create a new instance of the repository if necessary.
        ///     The Func is only necessary because with generics you can only create a new instance of a class
        ///     by calling a parameterless constructor--but we need to pass the UOW as a parameter.  So this is a work-around.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ctor"></param>
        /// <returns></returns>
        private T GetRepository<T>(Func<SqlUnitOfWork, T> ctor) where T : SqlRepositoryBase
        {
            if (!_Repositories.ContainsKey(typeof(T)))
            {
                _Repositories.Add(typeof(T), ctor(this));
            }

            return (T)_Repositories[typeof(T)];
        }
    }
}
