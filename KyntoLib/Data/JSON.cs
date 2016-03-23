using System;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Reflection;

namespace KyntoLib.Data
{
    /// <summary>
    /// Handles serialization and deserialization of JSON formatted data.
    /// </summary>
    public static class JSON
    {
        /// <summary>
        /// Serializes an object into an JSON structure.
        /// </summary>
        /// <typeparam name="T">The type to serializer into.</typeparam>
        /// <param name="JsonObject">The object to serialize.</param>
        /// <returns>The string representing the input JSON object.</returns>
        public static string Serializer<T>(T JsonObject)
        {
            // Clean the fields in the json object to serealize.
            foreach (FieldInfo Field in JsonObject.GetType().GetFields())
            {
                // Check the field value.
                object FieldValue = Field.GetValue(JsonObject);

                // If it is a string, clean it.
                if (FieldValue is string)
                {
                    Field.SetValue(JsonObject, HttpUtility.HtmlEncode(Regex.Replace((string)FieldValue, "<(.|\n)*?>", "").Replace("#", "")));
                }
            }

            // Serialize the data.
            JavaScriptSerializer JsonSerializer = new JavaScriptSerializer();
            String ParsedJsonString = JsonSerializer.Serialize(JsonObject);

            // Return the string.
            return ParsedJsonString;
        }

        /// <summary>
        /// DeSerializes an object from an JSON string.
        /// </summary>
        /// <typeparam name="T">The type to deserialize from.</typeparam>
        /// <param name="JsonString">The string representing the object as JSON.</param>
        /// <returns>An object representing the input string.</returns>
        public static T DeSerialize<T>(string JsonString)
        {
            // DeSerialize the input string.
            JavaScriptSerializer JsonSerializer = new JavaScriptSerializer();
            T ParsedJsonObject = JsonSerializer.Deserialize<T>(JsonString);

            // Clean all the input fields.
            foreach (FieldInfo Field in ParsedJsonObject.GetType().GetFields())
            {
                // Check the field value.
                object FieldValue = Field.GetValue(ParsedJsonObject);

                // If it is a string, clean it.
                if (FieldValue is string)
                {
                    Field.SetValue(ParsedJsonObject, HttpUtility.HtmlDecode(Regex.Replace((string)FieldValue, "<(.|\n)*?>", "").Replace("#", "")));
                }
            }

            // Return the object.
            return ParsedJsonObject;
        }
    }
}
