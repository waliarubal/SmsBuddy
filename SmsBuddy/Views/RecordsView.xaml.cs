using SmsBuddy.Models;
using System.Windows;
using System.Windows.Controls;

namespace SmsBuddy.Views
{
    /// <summary>
    /// Interaction logic for RecordsView.xaml
    /// </summary>
    public partial class RecordsView : UserControl
    {
        public static readonly DependencyProperty RecordProperty;

        #region constructor/destructor

        static RecordsView()
        {
            RecordProperty = DependencyProperty.Register(nameof(Record), typeof(ModelBase), typeof(RecordsView));
        }

        public RecordsView()
        {
            InitializeComponent();
        }

        #endregion

        #region properties

        public ModelBase Record
        {
            get { return (ModelBase)GetValue(RecordProperty); }
            set { SetValue(RecordProperty, value); }
        }

        #endregion
    }
}
