using System.Windows;

using BookListViewer.Views;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BookData;
using BookListViewer.ViewModels;
using BookListViewer.DAL;
using System.Diagnostics;
using System;

namespace BookListViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
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

        #region IDisposable Support

        // Since we "own" a CancellationTokenSource which implements IDisposable, this class must implement IDisposable
        // see CA1001 for details.

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //Dispose managed state (managed objects).
                    if(_cancellationTS != null)
                    {
                        _cancellationTS.Dispose();
                    }
                }

                // If we had any unmanaged resources (unmanaged objects) we would free them here
                // and override a finalizer below.

                // If we had any large fields, we would set them to null here.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MainWindow() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion

    }
}
