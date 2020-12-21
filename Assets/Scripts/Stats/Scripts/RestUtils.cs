using System.Net;
using System;
using System.IO;
using System.Text;
//using UnityEngine.CoreModule;

//using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

/*
    RUNNING THIS FILE:

        /usr/local/share/dotnet

        /git-react/rest-test]$ dotnet run

    Errors:

        dyld: lazy symbol binding failed: Symbol not found: _utimensat
        Referenced from: /usr/local/share/dotnet/shared/Microsoft.NETCore.App/3.1.0/System.Native.dylib
        Expected in: /usr/lib/libSystem.B.dylib

            MacOS needs to be 10.13 or higher for some core functionality.
            https://github.com/dotnet/core/issues/3148
    
*/
namespace estem
{
    class RestUtils
    {
        // =============================================================================================================================
        // RAW REST GET/PUT/PATCH/DELETE METHODS
        // =============================================================================================================================

        /*  Sends a REST "GET" request to a URL and returns the result.
        */
        public static string restGet(string url, int timeoutMs)
        {
            return restRequest(url, "GET", null, timeoutMs);
        }

        /*  Sends a REST "PUT" request to a URL and returns the result.
        */
        public static string restPut(string url, string jsonData, int timeoutMs)
        {
            return restRequest(url, "PUT", jsonData, timeoutMs);
        }

        public static string restPost(string url, string jsonData, int timeoutMs)
        {
            return restRequest(url, "POST", jsonData, timeoutMs);
        }

        /*  Sends a REST "PATCH" request to a URL and returns the result.
        */
        public static string restPatch(string url, string jsonData, int timeoutMs)
        {
            return restRequest(url, "PATCH", jsonData, timeoutMs);
        }

        /*  Sends a REST "DELETE" request to a URL and returns the result.
        */
        public static string restDelete(string url, int timeoutMs)
        {
            return restRequest(url, "DELETE", null, timeoutMs);
        }

        /*
            verb        GET, PUT, PATCH, DELETE
        */
        public static string restRequest(string url, string verb, string jsonData, int timeoutMs)
        {
            // Send request
            DebugUtils.debug("restRequest(): " + verb + " " + url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            Encoding encoding = Encoding.Default; // Encoding.utf8?
            request.Method = verb;
            request.ContentType = "application/json; charset=utf-8";
            if (verb == "GET" || verb == "DELETE") {
                // no need to set body on request
            } else if (jsonData != null) {
                byte[] buffer = encoding.GetBytes(jsonData);
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(buffer, 0, buffer.Length);
                dataStream.Close();
            }
            // Read response
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);
                string jsonResponse = reader.ReadToEnd();
                return jsonResponse;

            } catch (Exception e)
            {
                DebugUtils.debug("FAILED:  " + curlCommandFor(url, verb, "" + jsonData));
                //DebugUtils.debug("FAILED:  " + curlCommandFor(url, verb, jsonData));
                throw e;
            }
        }

        public static string curlCommandFor(string url, string verb, string jsonData)
        {
            string json = jsonData.Replace("\n", "").Replace("  ", "");
            return $"curl -X {verb} -H \"Content-Type: application/json\" -d \"{json}\" \"{url}\"";
        }

        /* Deserializes JSON to an actual object. 
        */

        /*
        public static T jsonToObject<T>(string json)
        {
            // https://www.c-sharpcorner.com/article/json-serialization-and-deserialization-in-c-sharp/
            JavaScriptSerializer js = new JavaScriptSerializer();
            JavaScriptConverter datetimeConverter = new DateTimeToGMTConverter();
            js.RegisterConverters(new JavaScriptConverter[] { datetimeConverter });
            T result = js.Deserialize<T>(json);
            return result;
        }
        */

        /*
         * https://docs.microsoft.com/en-us/dotnet/api/system.web.script.serialization.javascriptserializer?redirectedfrom=MSDN&view=netframework-4.8
         * Note: Properties on object must be PUBLIC to be serialized.
         */


        //public static string objectToJson<T>(T obj)
        //{
            // https://www.c-sharpcorner.com/article/json-serialization-and-deserialization-in-c-sharp/
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //JavaScriptConverter datetimeConverter = new DateTimeToGMTConverter();
            //  /*already commented out*/ JavaScriptConverter stringConverter = new FirebaseStringConverter();
            //js.RegisterConverters(new JavaScriptConverter[] { datetimeConverter /*, stringConverter  */ });
            //string json = js.Serialize(obj);
            //string prettyJson = prettyPrintJson(json);
            //return prettyJson;
        //}
        

        private const string JSON_INDENT_STRING = "    ";

        public static string prettyPrintJson(string json)
        {
            int indentation = 0;
            int quoteCount = 0;
            var result =
                from ch in json
                let quotes = ch == '"' ? quoteCount++ : quoteCount
                let lineBreak = ch == ',' && quotes % 2 == 0 ? ch + Environment.NewLine + String.Concat(Enumerable.Repeat(JSON_INDENT_STRING, indentation)) : null
                let openChar = ch == '{' || ch == '[' ? ch + Environment.NewLine + String.Concat(Enumerable.Repeat(JSON_INDENT_STRING, ++indentation)) : ch.ToString()
                let closeChar = ch == '}' || ch == ']' ? Environment.NewLine + String.Concat(Enumerable.Repeat(JSON_INDENT_STRING, --indentation)) + ch : ch.ToString()
                select lineBreak == null
                            ? openChar.Length > 1
                                ? openChar
                                : closeChar
                            : lineBreak;

            return String.Concat(result);
        }

        /*
         * Needed since default JSON serialization converts a DateTime object to a string like "Date(12766898)" which is C# specific.
         * This converter converts the DateTime objects to standard "2020-01-04T07:36:34.477787Z" format.
         * It also appends this value as a timestampValue subelement to the field, which is how firebase writes datetime values.
         */
        /*
        public class DateTimeToGMTConverter : JavaScriptConverter
        {

            public override IEnumerable<Type> SupportedTypes
            {
                //Define the ListItemCollection as a supported type.
                get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(DateTime) })); }
            }

            public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
            {
                DateTime date = (DateTime)obj;
                Dictionary<string, object> result = new Dictionary<string, object>();
                // Convert to UTC time (vs time in this zone) and write out in standard format "2020-01-04T07:36:34.477787Z"
                string dateString = date.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ");
                // Firebase expects this in a subfield called "timestampValue"
                result["timestampValue"] = dateString;
                return result;
            }

            public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
            {
                // TODO - NOT FULLY IMPLEMENTED!!
                if (type == typeof(DateTime))
                {
                    string dateString = (String)dictionary["timestampValue"];
                    DateTime date = DateTime.Parse(dateString);
                    return date;
                }
                return null;
            }
        }
        */
    }
}
