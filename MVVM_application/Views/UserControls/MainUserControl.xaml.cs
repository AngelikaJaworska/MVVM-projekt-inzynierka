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
    /// Interaction logic for MainUserControl.xaml
    /// </summary>
    public partial class MainUserControl : UserControl
    {
        public MainUserControl()
        {
            InitializeComponent();
        }

        #region Daily
        public static readonly DependencyProperty DailyProperty =
            DependencyProperty.Register(
                "DailyCommand",
                typeof(ICommand),
                typeof(MainUserControl),
                new UIPropertyMetadata(null));

        public ICommand DailyCommand
        {
            get
            {
                return (ICommand)GetValue(DailyProperty);
            }
            set
            {
                SetValue(DailyProperty, value);
            }
        }

        #endregion Daily/

        #region Register
        public static readonly DependencyProperty RegisterProperty =
         DependencyProperty.Register(
             "RegisterCommand",
             typeof(ICommand),
             typeof(MainUserControl),
             new UIPropertyMetadata(null));

        public ICommand RegisterCommand
        {
            get
            {
                return (ICommand)GetValue(RegisterProperty);
            }
            set
            {
                SetValue(RegisterProperty, value);
            }
        }
        #endregion Register

        #region PatientCard
        public static readonly DependencyProperty PatientCardProperty =
         DependencyProperty.Register(
             "PatientCardCommand",
             typeof(ICommand),
             typeof(MainUserControl),
             new UIPropertyMetadata(null));

        public ICommand PatientCardCommand
        {
            get
            {
                return (ICommand)GetValue(PatientCardProperty);
            }
            set
            {
                SetValue(PatientCardProperty, value);
            }
        }
        #endregion PatientCard

        #region Doctor
        public static readonly DependencyProperty DoctorProperty =
         DependencyProperty.Register(
             "DoctorCommand",
             typeof(ICommand),
             typeof(MainUserControl),
             new UIPropertyMetadata(null));

        public ICommand DoctorCommand
        {
            get
            {
                return (ICommand)GetValue(DoctorProperty);
            }
            set
            {
                SetValue(DoctorProperty, value);
            }
        }
        #endregion Doctor

        #region Logout
        public static readonly DependencyProperty LogoutProperty =
         DependencyProperty.Register(
             "LogoutCommand",
             typeof(ICommand),
             typeof(MainUserControl),
             new UIPropertyMetadata(null));

        public ICommand LogoutCommand
        {
            get
            {
                return (ICommand)GetValue(LogoutProperty);
            }
            set
            {
                SetValue(LogoutProperty, value);
            }
        }
        #endregion Logout

        #region Exit
        public static readonly DependencyProperty ExitProperty =
         DependencyProperty.Register(
             "ExitCommand",
             typeof(ICommand),
             typeof(MainUserControl),
             new UIPropertyMetadata(null));

        public ICommand ExitCommand
        {
            get
            {
                return (ICommand)GetValue(ExitProperty);
            }
            set
            {
                SetValue(ExitProperty, value);
            }
        }
        #endregion Exit

    }
}
