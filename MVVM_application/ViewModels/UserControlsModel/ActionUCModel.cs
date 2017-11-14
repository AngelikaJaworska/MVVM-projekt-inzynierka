using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MVVM_application.ViewModels.Manager;

namespace MVVM_application.ViewModels.UserControlsModel
{
    public class ActionUCModel: ViewModelBase
    {
        IViewManager _viewManager;

        #region ICommand

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        #endregion

        public ActionUCModel(IViewManager viewManager)
        {
             _viewManager = viewManager;

            InitialiseCommand();
        }

        public void InitialiseCommand()
        {
            SaveCommand = new RelayCommand(ExecuteSaveCommand);
            CancelCommand = new RelayCommand(ExecuteCancelCommand);
        }

        public void ExecuteSaveCommand()
        {
            MessageBox.Show("Test save");
        }
        public void ExecuteCancelCommand()
        {
            MessageBox.Show("Test cancel");
        }
    }
}
