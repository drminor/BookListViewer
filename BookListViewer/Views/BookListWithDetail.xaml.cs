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
            CatalogReader catReader = new CatalogReader();

            // TODO: Create two new methods: BeginFetchBookData and EndFetchBookData
            // and have the calling window create the Reader and call the BeginFetchBookData method.
            // This constuctor can then call EndFetchBookData, having been passed a reference to the Reader.
            List<BookRecDTO> catalogDTO = catReader.FetchBookData(XML_DATA_PATH);

            BookListVM bookListVM = new BookListVM(catalogDTO);

            this.DataContext = bookListVM;

            InitializeComponent();
            this.lstBoxBookSelector.Focus();

        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
