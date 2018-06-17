using BookData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace BookDataReaderXML
{
    public class CatalogReader
    {
        public Task<List<BookRecDTO>> FetchBookDataAsync(Stream xmlCatalogStream, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                List<BookRecDTO> result = new List<BookRecDTO>();

                XmlReaderSettings readerSettings = new XmlReaderSettings
                {
                    CloseInput = true,
                    ConformanceLevel = ConformanceLevel.Document
                };

                XmlReader reader = XmlReader.Create(xmlCatalogStream, readerSettings);

                while (reader.Read())
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "catalog")
                        {
                            continue; // Skip this node, continue the while loop and read the next node.
                        }
                        else if (reader.Name == "book")
                        {
                            // Hand the reader to the Parse Book Rec routine.
                            Thread.Sleep(300);
                            result.Add(ParseBookRec(reader));
                        }
                    }
                }

                reader.Close();
                xmlCatalogStream.Close();

                return result;

            }, cancellationToken);
        }

        public List<BookRecDTO> FetchBookData(Stream xmlCatalogStream)
        {
            List<BookRecDTO> result = new List<BookRecDTO>();

            XmlReaderSettings readerSettings = new XmlReaderSettings
            {
                CloseInput = true,
                ConformanceLevel = ConformanceLevel.Document
            };

            XmlReader reader = XmlReader.Create(xmlCatalogStream, readerSettings);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "catalog")
                    {
                        continue; // Skip this node, continue the while loop and read the next node.
                    }
                    else if (reader.Name == "book")
                    {
                        // Hand the reader to the Parse Book Rec routine.
                        result.Add(ParseBookRec(reader));
                    }
                }
            }

            reader.Close();
            return result;
        }

        private BookRecDTO ParseBookRec(XmlReader reader)
        {
            BookRecDTO result = new BookRecDTO();

            string idPlusPrefix = reader.GetAttribute("id");
            result.Id = GetId(idPlusPrefix);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    HandleElementNode(reader, result);
                }
                else
                {
                    if(reader.NodeType == XmlNodeType.EndElement && reader.Name == "book")
                    {
                        break; // Exit while loop.
                    }
                }
            }

            return result;
        }

        private void HandleElementNode(XmlReader reader, BookRecDTO bookRec)
        {
            switch (reader.Name)
            {
                case "author": bookRec.Author = GetElementValue(reader); break;

                case "title": bookRec.Title = GetElementValue(reader); break;
                case "genre": bookRec.Genre = GetElementValue(reader); break;

                //case "genre":
                //    {
                //        string genre = GetElementValue(reader);
                //        genre = genre.Replace(" ", "");
                //        bookRec.Genre = (Genre)Enum.Parse(typeof(Genre), genre);
                //        break;
                //    }

                case "price": bookRec.Price = Decimal.Parse(GetElementValue(reader)); break;

                case "publish_date": bookRec.PublishDate = DateTime.Parse(GetElementValue(reader)); break;

                case "description":
                    {
                        string description = GetElementValue(reader);
                        bookRec.Description = Regex.Replace(description, @"\s+", " ");
                        break;
                    }

                default:
                    break;
            }

        }

        private string GetElementValue(XmlReader reader)
        {
            reader.Read();
            return reader.Value;
        }

        private int GetId(string idAndPrefix)
        {
            string numVal = idAndPrefix.Substring(2, 3);

            if(int.TryParse(numVal, out int result))
            {
                return result;
            }
            else
            {
                throw new InvalidOperationException($"Could not parse the third, forth and fifth characters of the string: {idAndPrefix} as in integer.");
            }
        }

    }
}
