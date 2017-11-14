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
    /// Interaction logic for DoctorUserControl.xaml
    /// </summary>
    public partial class DoctorUserControl : UserControl
    {
        public DoctorUserControl()
        {
            InitializeComponent();
        }

        #region DoctorDailyVisit
        public static readonly DependencyProperty DoctorDailyVisitProperty =
            DependencyProperty.Register(
                "DoctorDailyVisitCommand",
                typeof(ICommand),
                typeof(DoctorUserControl),
                new UIPropertyMetadata(null));

        public ICommand DoctorDailyVisitCommand
        {
            get
            {
                return (ICommand)GetValue(DoctorDailyVisitProperty);
            }
            set
            {
                SetValue(DoctorDailyVisitProperty, value);
            }
        }

        #endregion DoctorDailyVisit

        #region DoctorVisit
        public static readonly DependencyProperty DoctorVisitProperty =
         DependencyProperty.Register(
             "DoctorVisitCommand",
             typeof(ICommand),
             typeof(DoctorUserControl),
             new UIPropertyMetadata(null));

        public ICommand DoctorVisitCommand
        {
            get
            {
                return (ICommand)GetValue(DoctorVisitProperty);
            }
            set
            {
                SetValue(DoctorVisitProperty, value);
            }
        }
        #endregion DoctorVisit

        #region DoctorEditData
        public static readonly DependencyProperty DoctorEditDataProperty =
         DependencyProperty.Register(
             "DoctorEditDataCommand",
             typeof(ICommand),
             typeof(DoctorUserControl),
             new UIPropertyMetadata(null));

        public ICommand DoctorEditDataCommand
        {
            get
            {
                return (ICommand)GetValue(DoctorEditDataProperty);
            }
            set
            {
                SetValue(DoctorEditDataProperty, value);
            }
        }
        #endregion DoctorEditDataData
    }
}
