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
    /// Interaction logic for ActionUserControl.xaml
    /// </summary>
    public partial class ActionUserControl : UserControl
    {
        public ActionUserControl()
        {
            InitializeComponent();
        }
        #region Save
        public static readonly DependencyProperty SaveProperty =
            DependencyProperty.Register(
                "SaveCommand",
                typeof(ICommand),
                typeof(ActionUserControl),
                new UIPropertyMetadata(null));

        public ICommand SaveCommand
        {
            get
            {
                return (ICommand)GetValue(SaveProperty);
            }
            set
            {
                SetValue(SaveProperty, value);
            }
        }

        #endregion Save
    }
}
