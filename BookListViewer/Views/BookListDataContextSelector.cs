using BookData;
using BookDataReaderXML;
using BookListViewer.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Resources;
using System.IO;

namespace BookListViewer.Views
{
    public class BookListDataContextSelector
    {
        public const string DEFAULT_RESOURCE_NAME = "BooksXXX";

        // The default, parameterless constructor is provided so that the XAML editor
        // recognizes this class as a candidate to serve as an ObjectDataProvider.
        // This is required even though the caller should specify a method name, e.g. GetDataContext.
        public object GetDataContext()
        {
            return GetDataContext(null);
        }

        public object GetDataContext(string resourceName)
        {
            if (resourceName == null || resourceName == string.Empty)
            {
                resourceName = DEFAULT_RESOURCE_NAME;
            }
            else
            {
                // We are expecting a Resource Name with no file extension.
                // If the caller includes the ".xml" extension,
                // remove it -- since later we are going to append it unconditionally.
                if(resourceName.ToLower().EndsWith(".xml"))
                {
                    resourceName = resourceName.Substring(0, resourceName.Length - 4);
                }
            }

            List<BookRecDTO> catalogDTO;
            using (Stream stream = GetXmlDataStream(resourceName))
            {
                CatalogReader catReader = new CatalogReader();
                catalogDTO = catReader.FetchBookData(stream);
            }

            BookListVM result = new BookListVM(catalogDTO);
            return result;
        }

        private Stream GetXmlDataStream(string resourceName)
        {
            Stream result;

#if DEBUG
            // Only check to see if we are in design mode if we are running in DEBUG.
            if (InDesignMode)
            {
                result = GetXmlDataFromFile(resourceName);
            }
            else
            {
                result = GetXmlDataFromResource(resourceName);
            }
#else
            result = GetXMLDataFromResource(resourceName);
#endif
            return result;
        }

        private Stream GetXmlDataFromResource(string resourceName)
        {
            string resourcePath = $"Data\\{resourceName}.xml";
            Uri uri = new Uri(resourcePath, UriKind.Relative);
            StreamResourceInfo info = Application.GetContentStream(uri);

            if(info == null)
            {
                throw new InvalidOperationException($"Cannot find content file at path: {uri}.");
            }

            return info.Stream;
        }

        private Stream GetXmlDataFromFile(string filename)
        {
            string pathToDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(pathToDesktop, filename);
            filePath = $"{filePath}.xml";

            try
            {
                Stream result = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                return result;
            }
            catch
            {
                throw new InvalidOperationException($"Cannot find design-time XML file at path: {filePath}.");
            }
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
