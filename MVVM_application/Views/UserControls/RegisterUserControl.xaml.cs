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

namespace MVVM_application.Views.UserControls
{
    /// <summary>
    /// Interaction logic for RegisterUserControl.xaml
    /// </summary>
    public partial class RegisterUserControl : UserControl
    {
        public RegisterUserControl()
        {
            InitializeComponent();
        }

        #region RegisterDailyVisit
        public static readonly DependencyProperty RegisterDailyVisitProperty =
            DependencyProperty.Register(
                "RegisterDailyVisitCommand",
                typeof(ICommand),
                typeof(RegisterUserControl),
                new UIPropertyMetadata(null));

        public ICommand RegisterDailyVisitCommand
        {
            get
            {
                return (ICommand)GetValue(RegisterDailyVisitProperty);
            }
            set
            {
                SetValue(RegisterDailyVisitProperty, value);
            }
        }

        #endregion RegisterDailyVisit

        #region EditVisit
        public static readonly DependencyProperty EditVisitProperty =
         DependencyProperty.Register(
             "EditVisitCommand",
             typeof(ICommand),
             typeof(RegisterUserControl),
             new UIPropertyMetadata(null));

        public ICommand EditVisitCommand
        {
            get
            {
                return (ICommand)GetValue(EditVisitProperty);
            }
            set
            {
                SetValue(EditVisitProperty, value);
            }
        }
        #endregion EditVisit

        #region AddNewPatient
        public static readonly DependencyProperty AddNewPatientProperty =
         DependencyProperty.Register(
             "AddNewPatientCommand",
             typeof(ICommand),
             typeof(RegisterUserControl),
             new UIPropertyMetadata(null));

        public ICommand AddNewPatientCommand
        {
            get
            {
                return (ICommand)GetValue(AddNewPatientProperty);
            }
            set
            {
                SetValue(AddNewPatientProperty, value);
            }
        }
        #endregion AddNewPatient
    }
}
