using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MVVM_application.Manager;

namespace MVVM_application.ViewModels.UserControlsModel
{
    public class ActionUCModel: ViewModelBase
    {
        IManager _manager;

        #region ICommand

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        #endregion

        public ActionUCModel(IManager manager)
        {
             _manager = manager;

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
