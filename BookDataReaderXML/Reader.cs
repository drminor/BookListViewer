using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BookData;

namespace BookDataReaderXML
{
    public class Reader
    {

        public List<BookRec> FetchBookData(string path)
        {
            List<BookRec> result = new List<BookRec>();

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

        private BookRec ParseBookRec(XmlTextReader reader)
        {
            BookRec result = new BookRec();

            string idPlusPrefix = reader.GetAttribute("id");
            result.Id = GetId(idPlusPrefix);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "author")
                    {
                        reader.Read();
                        result.Author = reader.Value;
                    }
                    else if (reader.Name == "title")
                    {
                        reader.Read();
                        result.Title = reader.Value;
                    }
                    else
                    {
                        continue; // Continue the while loop (read and process the next node.)
                    }
                }
                else if(reader.NodeType == XmlNodeType.EndElement)
                {
                    if(reader.Name == "book")
                    {
                        break; // Exit while loop.
                    }
                }
                else
                {
                    continue; // Continue the while loop (read and process the next node.)
                }
            }

            return result;
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
