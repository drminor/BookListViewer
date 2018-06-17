using BookData;
using BookListViewer.DAL;
using BookListViewer.ViewModels;
using System.Collections.Generic;

namespace BookListViewer.Views
{
    public class BookListDesignTimeDataProvider
    {
        public object GetViewModel(string resourceName)
        {
            System.Diagnostics.Debug.WriteLine("The GetViewModel method of the BookListDesignTimeDataProvider class is being called.");

            List<BookRecDTO> catalog = new BookListDAL().FetchBookData(resourceName, useSampleData: true);
            BookListVM result = new BookListVM(catalog);
            return result;
        }
    }
}
