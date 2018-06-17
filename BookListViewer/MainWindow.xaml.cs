using System.Windows;

using BookListViewer.Views;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BookData;
using BookListViewer.ViewModels;
using BookListViewer.DAL;
using System.Diagnostics;

namespace BookListViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BookListWithDetail booksListViewerWindow;

        CancellationTokenSource _cancellationTS;

        public MainWindow()
        {
            _cancellationTS = new CancellationTokenSource();
            BookListDAL bookListDAL = new BookListDAL();
            Task<List<BookRecDTO>> fetchCatalogDataTask = bookListDAL.FetchBookDataAsync("Books", IsInDesign, _cancellationTS.Token);

            BookListVM vm = new BookListVM(fetchCatalogDataTask, _cancellationTS);
            booksListViewerWindow = new BookListWithDetail(vm);

            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, System.EventArgs e)
        {

            Thread.Sleep(500);

            booksListViewerWindow.Show();

            this.Close();
        }

        private bool? _isInDesign = null;
        private bool IsInDesign
        {
            get
            {
                if(!_isInDesign.HasValue)
                {
                    bool temp = false;
                    SetDesignModeFlag(ref temp);
                    _isInDesign = temp;
                }
                return _isInDesign.Value;
            }
        }

        [Conditional("DEBUG")] // Only called if the (compilation) DEBUG constant is set
        private void SetDesignModeFlag(ref bool inDesignMode)
        {
            inDesignMode = System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject());
        }
    }
}
