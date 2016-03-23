using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace KyntoLib.Data
{
    /// <summary>
    /// Handles serialization and deserialization of XML formatted data.
    /// </summary>
    public static class XML
    {
        /// <summary>
        /// Serializes an object into an XML structure.
        /// </summary>
        /// <typeparam name="T">The type to serialize into.</typeparam>
        /// <param name="XmlObject">The object to serialize.</param>
        /// <returns>A string formatted as XML representing the input XMLObject</returns>
        public static string Serializer<T>(T XmlObject)
        {
            // Create our streams.
            MemoryStream Stream = null;
            TextWriter Writer = null;

            try
            {
                // Serialize...
                Stream = new MemoryStream();
                Writer = new StreamWriter(Stream, Encoding.ASCII);
                XmlSerializer Serializer = new XmlSerializer(typeof(T));
                Serializer.Serialize(Writer, XmlObject);
                int Count = (int)Stream.Length;
                byte[] Arr = new byte[Count];
                Stream.Seek(0, SeekOrigin.Begin);
                Stream.Read(Arr, 0, Count);

                // Return the XML string encoded as ASCII.
                return Encoding.ASCII.GetString(Arr).Trim();
            }
            finally
            {
                // Finally close our stream if it hasnt already been.
                if (Stream != null)
                {
                    Stream.Close();
                }

                // Close our writer if it hasnt already been.
                if (Writer != null)
                {
                    Writer.Close();
                }
            }
        }

        /// <summary>
        /// DeSerializes an object from an XML string.
        /// </summary>
        /// <typeparam name="T">The type to deserialize from.</typeparam>
        /// <param name="XmlString">The string representing the object as XML.</param>
        /// <returns>An object representing the input string.</returns>
        public static T DeSerialize<T>(string XmlString)
        {
            // Create our streams.
            StringReader Stream = null;
            XmlTextReader Reader = null;

            try
            {
                // DeSerialize...
                XmlSerializer Serializer = new XmlSerializer(typeof(T));
                Stream = new StringReader(XmlString);
                Reader = new XmlTextReader(Stream);

                // Return the deserialized object.
                return (T)Serializer.Deserialize(Reader);
            }
            finally
            {
                // Finally close our stream if it hasnt already been.
                if (Stream != null)
                {
                    Stream.Close();
                }

                // Close our writer if it hasnt already been.
                if (Reader != null)
                {
                    Reader.Close();
                }
            }
        }
    }
}
