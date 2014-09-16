using CarRental.Admin.ViewModels;
using Core.Common;
using Core.Common.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarRental.Admin.Views
{
    /// <summary>
    /// Interaction logic for MaintainCarsView.xaml
    /// </summary>
    public partial class MaintainCarsView : UserControlViewBase
    {
        public MaintainCarsView()
        {
            InitializeComponent();
        }

        protected override void OnWireViewModelEvents(ViewModelBase viewModel)
        {
            MaintainCarsViewModel vm = viewModel as MaintainCarsViewModel;
            if (vm != null)
            {
                vm.ConfirmDelete += OnConfirmDelete;
                vm.ErrorOccured += OnErrorOccured;
            }
        }

        protected override void OnUnwireViewModelEvents(ViewModelBase viewModel)
        {
            base.OnUnwireViewModelEvents(viewModel);
        }

        void OnConfirmDelete(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this car?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        void OnErrorOccured(object sender, ErrorMessageEventArgs e)
        {
            MessageBox.Show(e.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
