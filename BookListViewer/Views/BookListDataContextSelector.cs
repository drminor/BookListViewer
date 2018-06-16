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
        public const string DEFAULT_RESOURCE_NAME = "Books";

        public object GetDataContext(string resourceName)
        {
            // Use the default value if no value was provided and
            // remove the .xml extension if included.
            resourceName = NormalizeResourceName(resourceName);

            // Parse the XML data file into a list of BookRecDTO objects.
            List<BookRecDTO> catalogDTO;
            using (Stream stream = GetXmlDataStream(resourceName))
            {
                CatalogReader catReader = new CatalogReader();
                catalogDTO = catReader.FetchBookData(stream);
            }

            // Create a View Model using the list of BookRecDTO objects
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

        private string NormalizeResourceName(string resourceName)
        {
            // Usually the result will be argument, unchanged.
            string result = resourceName;

            if (resourceName == null || resourceName == string.Empty)
            {
                // If no value was specified, use the default.
                result = DEFAULT_RESOURCE_NAME;
            }
            else
            {
                // We are expecting a Resource Name with no file extension.
                // If the caller includes the ".xml" extension,
                // remove it -- since later we are going to append it unconditionally.
                if (resourceName.ToLower().EndsWith(".xml"))
                {
                    result = resourceName.Substring(0, resourceName.Length - 4);
                }
            }

            return result;
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
