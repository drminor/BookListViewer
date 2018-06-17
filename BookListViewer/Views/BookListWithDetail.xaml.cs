using System.Windows;

namespace BookListViewer.Views
{
    /// <summary>
    /// Interaction logic for BookListWithDetail.xaml
    /// </summary>
    public partial class BookListWithDetail : Window
    {
        public BookListWithDetail()
        {
            //BookListDataContextSelector selector = new BookListDataContextSelector();
            //object dc = selector.GetDataContext("Books");
            //this.DataContext = dc;

            InitializeComponent();

            this.lstBoxBookSelector.Focus();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Raise a cancel task event and make the ViewModel listen for this event.
            this.Close();
        }
    }
}
