using System.Windows;

namespace BookListViewer.Views
{
    /// <summary>
    /// Interaction logic for BookListWithDetail.xaml
    /// </summary>
    public partial class BookListWithDetail : Window
    {
        const string BOOK_RESOURCE_PATH = @"Data\Books.xml";

        public BookListWithDetail()
        {
            InitializeComponent();

            this.lstBoxBookSelector.Focus();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
