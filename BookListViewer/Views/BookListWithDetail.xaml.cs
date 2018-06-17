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

        #endregion
    }
}
