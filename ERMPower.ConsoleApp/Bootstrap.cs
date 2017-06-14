using System;
using System.Collections.Generic;
using Autofac;
using ERMPower.Business.DataExtractors;
using ERMPower.Business.FileProcessors;
using ERMPower.Infrastructure;
using ERMPower.Infrastructure.Calculations.Median;

namespace ERMPower.ConsoleApp
{
    //
    //  This class is a temporary class to store the dependencies for demo purposes.
    //  Idealy we should use a DI resolver (like autofac or ninject), and share it across the projects. For that first need to create an interface
    //  in "ERMPower.Core" project and then projects which needs to be bootstrapped must have a class implementing that interface.
    //  The starter application (in this case the console app) will create the main container, and will be passing it across the projects to
    //  register their own dependencies
    //

    public class Bootstrap
    {
        private static Bootstrap _instance;

        private IDictionary<Type, object> _dependencies;
        private IContainer _container;

        public static Bootstrap Instance
        {
            get { return _instance ?? (_instance = new Bootstrap()); }
        }

        private Bootstrap()
        {
            _dependencies = new Dictionary<Type, object>();
            RegisterDependencies();
        }

        public void RegisterDependencies()
        {
            //
            //  TODO:
            //  This is temporary. There should be a custom attribute which will be decorated for types which are necessary to be recognizable to be bootstrapped.
            //  In here actually the application knows its dependencies, meaning it knows it by implementation :) 
            //  But ideally it should be just through abstractions
            //
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<AsyncFileLoader>().AsImplementedInterfaces();
            containerBuilder.RegisterType<LpDataExtractor>().AsImplementedInterfaces();
            containerBuilder.RegisterGeneric(typeof (DefaultFileProcessor<>)).AsImplementedInterfaces();
            containerBuilder.RegisterType<DefaultMedianStrategy>().AsImplementedInterfaces();

            _container = containerBuilder.Build();
        }

        public T Get<T>()
        {
            var dependency = _container.Resolve<T>();
            return dependency;
        }

        public object Get(Type type)
        {
            var dependency = _container.Resolve(type);
            return dependency;
        }
    }
}