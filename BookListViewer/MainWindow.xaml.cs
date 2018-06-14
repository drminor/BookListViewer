using System.Windows;

using BookListViewer.Views;
using System.Threading;

namespace BookListViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, System.EventArgs e)
        {
            BookListWithDetail booksListViewerWindow = new BookListWithDetail();

            Thread.Sleep(1000);

            booksListViewerWindow.Show();

            this.Close();
        }
    }
}
