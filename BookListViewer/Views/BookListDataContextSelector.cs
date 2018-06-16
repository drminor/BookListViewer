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
        public const string DEFAULT_RESOURCE_NAME = "Books";

        public object GetDataContext()
        {
            return GetDataContext(null);
        }

        public object GetDataContext(string resourceName)
        {
            BookListVM result;

            //string ResourcePath = @"Data\Books.xml";
            if (resourceName == null || resourceName == string.Empty)
            {
                resourceName = DEFAULT_RESOURCE_NAME;
            }

            if (InDesignMode)
            {
                string filePath = $"C:\\DEV\\BookListViewer\\BookListViewer\\Data\\{resourceName}.xml";
                List<BookRecDTO> catalogDTO = FetchBookDataFromFile(filePath);
                result = new BookListVM(catalogDTO);
            }
            else
            {
                string ResourcePath = $"Data\\{resourceName}.xml";

                List<BookRecDTO> catalogDTO = FetchBookDataFromResource(ResourcePath);
                result = new BookListVM(catalogDTO);
            }

            return result;
        }

        private List<BookRecDTO> FetchBookDataFromFile(string filePath)
        {
            System.IO.Stream sr = new System.IO.FileStream
                (
                filePath,
                System.IO.FileMode.Open,
                System.IO.FileAccess.Read,
                System.IO.FileShare.Read
                );

            CatalogReader catReader = new CatalogReader();

            List<BookRecDTO> catalogDTO = catReader.FetchBookData(sr);
            sr.Close();

            return catalogDTO;
        }

        private List<BookRecDTO> FetchBookDataFromResource(string resourcePath)
        {
            Uri uri = new Uri(resourcePath, UriKind.Relative);
            StreamResourceInfo info = Application.GetResourceStream(uri);

            CatalogReader catReader = new CatalogReader();

            List<BookRecDTO> catalogDTO = catReader.FetchBookData(info.Stream);
            info.Stream.Close();

            return catalogDTO;
        }

        private bool InDesignMode
        {
            get
            {
                bool designTime = System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject());
                return designTime;
            }
        }
    }
}
