using BookData;
using BookDataReaderXML;
using BookListViewer.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Resources;

namespace BookListViewer.Views
{
    public class BookListDataContextSelector
    {
        public object GetDataContext(string resourceName)
        {
            //string ResourcePath = @"Data\Books.xml";


            if (resourceName == null || resourceName == string.Empty)
            {
                resourceName = "Books";
            }

            string ResourcePath = $"Data\\{resourceName}.xml";

            List<BookRecDTO> catalogDTO = FetchBookData(ResourcePath);
            BookListVM result = new BookListVM(catalogDTO);
            return result;
        }

        private List<BookRecDTO> FetchBookData(string ResourcePath)
        {
            Uri uri = new Uri(ResourcePath, UriKind.Relative);
            StreamResourceInfo info = Application.GetResourceStream(uri);

            CatalogReader catReader = new CatalogReader();

            List<BookRecDTO> catalogDTO = catReader.FetchBookData(info.Stream);
            info.Stream.Close();

            return catalogDTO;
        }
    }
}
