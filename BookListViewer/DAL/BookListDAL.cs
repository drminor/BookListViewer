using BookData;
using BookDataReaderXML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Resources;

namespace BookListViewer.DAL
{
    public class BookListDAL
    {
        public const string DEFAULT_RESOURCE_NAME = "Books";

        public Task<List<BookRecDTO>> FetchBookDataAsync(string resourceName, bool useSampleData, CancellationToken cancellationToken)
        {
            // Use the default value if no value was provided and
            // remove the .xml extension if included.
            resourceName = NormalizeResourceName(resourceName);

            // Open the XML Data Stream for reading.
            Stream stream = GetXmlDataStream(resourceName, useSampleData);

            // Create an asynchronous task that will...
            // parse the XML data file into a list of BookRecDTO objects.
            CatalogReader catReader = new CatalogReader();
            Task<List<BookRecDTO>> result = catReader.FetchBookDataAsync(stream, cancellationToken);

            return result;
        }

        public List<BookRecDTO> FetchBookData(string resourceName, bool useSampleData)
        {
            // Use the default value if no value was provided and
            // remove the .xml extension if included.
            resourceName = NormalizeResourceName(resourceName);

            // Open the XML Data Stream for reading.
            Stream stream = GetXmlDataStream(resourceName, useSampleData);

            // Create an asynchronous task that will...
            // parse the XML data file into a list of BookRecDTO objects.
            CatalogReader catReader = new CatalogReader();
            List<BookRecDTO> result = catReader.FetchBookData(stream);

            return result;
        }

        private Stream GetXmlDataStream(string resourceName, bool inDesignMode)
        {
            Stream result;

            if (inDesignMode)
            {
                result = GetXmlDataFromFile(resourceName);
            }
            else
            {
                result = GetXmlDataFromResource(resourceName);
            }
            return result;
        }

        private Stream GetXmlDataFromResource(string resourceName)
        {
            string resourcePath = $"Data\\{resourceName}.xml";
            Uri uri = new Uri(resourcePath, UriKind.Relative);
            StreamResourceInfo info = Application.GetContentStream(uri);

            if (info == null)
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
    }
}
