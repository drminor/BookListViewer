using BookData;
using BookDataReaderXML;
using System.Collections.Generic;
using System.Windows;
using BookListViewer.ViewModels;
using System;
using System.Windows.Resources;

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
            List<BookRecDTO> catalogDTO = FetchBookData(BOOK_RESOURCE_PATH);
            BookListVM bookListVM = new BookListVM(catalogDTO);
            this.DataContext = bookListVM;

            InitializeComponent();

            this.lstBoxBookSelector.Focus();
        }

        // TODO: Create two new methods: BeginFetchBookData and EndFetchBookData
        // and have the calling window create the Reader and call the BeginFetchBookData method.
        // This window can then call EndFetchBookData, having been passed a reference to the Reader.

        private List<BookRecDTO> FetchBookData(string ResourcePath) 
        {
            Uri uri = new Uri(ResourcePath, UriKind.Relative);
            StreamResourceInfo info = Application.GetResourceStream(uri);

            CatalogReader catReader = new CatalogReader();

            List<BookRecDTO> catalogDTO = catReader.FetchBookData(info.Stream);
            info.Stream.Close();

            return catalogDTO;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
