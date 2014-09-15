using CarRental.Client.Bootstrapper;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace CarRental.Admin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ObjectBase.Container = MEFLoader.Init(new List<ComposablePartCatalog>(){
                new AssemblyCatalog(Assembly.GetExecutingAssembly())
            });
        }
    }
}
