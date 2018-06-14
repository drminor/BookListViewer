using System.Windows;
using BookDataReaderXML;
using System.Collections.Generic;
using BookData;

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
            List<BookRec> lst = br.FetchBookData(XML_DATA_PATH);

            InitializeComponent();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
