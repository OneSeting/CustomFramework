using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LittleHourglass.Commonality.BaseExpand
{
    public class ServiceLocator
    {
        #region Fileds
        private ServiceProvider _currentServiceProvider;
        private static ServiceProvider _serviceProvider;
        #endregion

        #region Constructors
        public ServiceLocator(ServiceProvider currentServiceProvider)
        {
            _currentServiceProvider = currentServiceProvider;
        }
        #endregion

        #region Utilities
        #endregion

        #region Methods
        public static ServiceLocator Current { get { return new ServiceLocator(_serviceProvider); } }

        public static void SetLocatorProvider(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object GetInstance(Type serviceType)
        {
            return _currentServiceProvider.GetService(serviceType);
        }

        public TService GetInstance<TService>()
        {
            return _currentServiceProvider.GetService<TService>();
        }
        #endregion
    }
}
