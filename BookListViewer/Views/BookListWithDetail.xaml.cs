using BookListViewer.ViewModels;
using System;
using System.Windows;

namespace BookListViewer.Views
{
    /// <summary>
    /// Interaction logic for BookListWithDetail.xaml
    /// </summary>
    public partial class BookListWithDetail : Window
    {
        //Task<List<BookRecDTO>> _fetchCatalogDataTask;

        #region Constructors

        public BookListWithDetail()
        {
            throw new NotSupportedException($"Using the parameterless constructor of the {nameof(BookListWithDetail)} class is not supported.");
        }

        public BookListWithDetail(BookListVM vm)
        {
            this.DataContext = vm;
            InitializeComponent();
            this.lstBoxBookSelector.Focus();
        }

        //public BookListWithDetail(Task<List<BookRecDTO>> fetchCatalogDataTask)
        //{
        //    _fetchCatalogDataTask = fetchCatalogDataTask;

        //    InitializeComponent();

        //    this.lstBoxBookSelector.Focus();
        //}

        #endregion

        #region Event Handlers

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Make the VM support a method that can be called to signal that the view is closing
            // instead of the current method: CancelDataLoading.

            // Ask the ViewModel to stop loading data.
            BookListVM vm = this.DataContext as BookListVM;
            vm?.CancelDataLoading();

            this.Close();
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{

        //    if (_fetchCatalogDataTask == null)
        //    {
        //        BookListDataContextSelector selector = new BookListDataContextSelector();
        //        _fetchCatalogDataTask = selector.GetAFetchDataTask("Books");
        //    }

        //    BookListVM vm = this.TopGrid?.DataContext as BookListVM;

        //    if (vm == null)
        //    {
        //        this.TopGrid.DataContext = new BookListVM(_fetchCatalogDataTask, null);
        //    }
        //    else
        //    {
        //        vm.LoadData(_fetchCatalogDataTask, null); // TODO create cancel token.
        //    }
        //}

        #endregion
    }
}
