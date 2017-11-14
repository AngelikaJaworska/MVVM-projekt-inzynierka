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
    /// Interaction logic for PatientCardUserControl.xaml
    /// </summary>
    public partial class PatientCardUserControl : UserControl
    {
        public PatientCardUserControl()
        {
            InitializeComponent();
        }

        #region PatientNewVisit
        public static readonly DependencyProperty PatientNewVisitProperty =
            DependencyProperty.Register(
                "PatientNewVisitCommand",
                typeof(ICommand),
                typeof(PatientCardUserControl),
                new UIPropertyMetadata(null));

        public ICommand PatientNewVisitCommand
        {
            get
            {
                return (ICommand)GetValue(PatientNewVisitProperty);
            }
            set
            {
                SetValue(PatientNewVisitProperty, value);
            }
        }

        #endregion PatientNewVisit

        #region PatientVisit
        public static readonly DependencyProperty PatientVisitProperty =
         DependencyProperty.Register(
             "PatientVisitCommand",
             typeof(ICommand),
             typeof(PatientCardUserControl),
             new UIPropertyMetadata(null));

        public ICommand PatientVisitCommand
        {
            get
            {
                return (ICommand)GetValue(PatientVisitProperty);
            }
            set
            {
                SetValue(PatientVisitProperty, value);
            }
        }
        #endregion PatientVisit

        #region PatientEditData
        public static readonly DependencyProperty PatientEditDataProperty =
         DependencyProperty.Register(
             "PatientEditDataCommand",
             typeof(ICommand),
             typeof(PatientCardUserControl),
             new UIPropertyMetadata(null));

        public ICommand PatientEditDataCommand
        {
            get
            {
                return (ICommand)GetValue(PatientEditDataProperty);
            }
            set
            {
                SetValue(PatientEditDataProperty, value);
            }
        }
        #endregion PatientEditData
    }
}
