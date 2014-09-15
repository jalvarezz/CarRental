using Core.Common.Contracts;
using Core.Common.UI.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Admin.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RentalsViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public RentalsViewModel(IServiceFactory serviceFactory)
        {
            _ServiceFactory = serviceFactory;
        }

        IServiceFactory _ServiceFactory;

        public override string ViewTitle
        {
            get { return "Rentals"; }
        }
    }
}
