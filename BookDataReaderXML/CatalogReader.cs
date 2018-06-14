using BookData;
using System;
using System.Collections.Generic;
using System.Xml;

namespace BookDataReaderXML
{
    public class CatalogReader
    {
        public List<BookRecDTO> FetchBookData(string path)
        {
            List<BookRecDTO> result = new List<BookRecDTO>();

            XmlTextReader reader = new XmlTextReader(path)
            {
                WhitespaceHandling = WhitespaceHandling.None
            };

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "catalog")
                    {
                        continue; // Skip this node, continue the while loop and read the next node.
                    }
                    else if(reader.Name == "book")
                    {
                        // Hand the reader to the Parse Book Rec routine.
                        result.Add(ParseBookRec(reader));
                    }
                }
            }

            reader.Close();
            return result;
        }

        private BookRecDTO ParseBookRec(XmlTextReader reader)
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

        private void HandleElementNode(XmlTextReader reader, BookRecDTO bookRec)
        {
            switch (reader.Name)
            {
                case "author": bookRec.Author = GetElementValue(reader); break;
                case "title": bookRec.Title = GetElementValue(reader); break;
                case "description": bookRec.Description = GetElementValue(reader); break;

                default:
                    break;
            }

        }

        private string GetElementValue(XmlTextReader reader)
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
