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
            // Let the View Model know that "its" view is being closed.
            BookListVM vm = this.DataContext as BookListVM;
            vm?.ViewIsClosing();

            this.Close();
        }

        #endregion
    }
}
