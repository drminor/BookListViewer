using BookData;
using BookDataReaderXML;
using System.Collections.Generic;
using System.Windows;
using BookListViewer.ViewModels;

namespace BookListViewer.Views
{
    /// <summary>
    /// Interaction logic for BookListWithDetail.xaml
    /// </summary>
    public partial class BookListWithDetail : Window
    {
        const string XML_DATA_PATH = @"..\Debug\Data\Books.xml";

        public BookListWithDetail()
        {
            Reader br = new Reader();

            // TODO: Create two new methods: BeginFetchBookData and EndFetchBookData
            // and have the calling window create the Reader and call the BeginFetchBookData method.
            // This constuctor can then call EndFetchBookData, having been passed a reference to the Reader.
            List<BookRec> catalog = br.FetchBookData(XML_DATA_PATH);

            this.DataContext = new BookListVM(catalog);

            InitializeComponent();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
