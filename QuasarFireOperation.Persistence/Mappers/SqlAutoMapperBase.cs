using System;
using System.Reflection;
using AutoMapper.Configuration;
using QuasarFireOperation.Infrastructure.Mappers;

namespace QuasarFireOperation.Persistence.Mappers
{
    public abstract class SqlAutoMapperBase<T> : AutoMapperBase<T>
    {
        protected override void ConfigureMappings(MapperConfigurationExpression cfg)
        {
            base.ConfigureMappings(cfg);
        }

        #region Singleton

        private static T _Instance;
        private static readonly object syncLock = new object();

        public static T GetInstance()
        {
            // Support multithreaded applications through
            // 'Double checked locking' pattern which (once
            // the instance exists) avoids locking each
            // time the method is invoked
            if (_Instance == null)
                lock (syncLock)
                {
                    if (_Instance == null)
                    {
                        //_Instance = new T();

                        //create via reflection -- necessary because derived classes should declare private constructors
                        Type[] paramTypes = { };
                        object[] paramVals = { };

                        var t = typeof(T);
                        var ci = t.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null,
                            paramTypes, null);
                        _Instance = (T) ci.Invoke(paramVals);
                    }
                }

            return _Instance;
        }

        #endregion
    }
}