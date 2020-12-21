using System;
using System.Linq;

namespace estem
{
    // =============================================================================================================================
    // FIRESTORE METHODS
    // =============================================================================================================================
    class FirestoreUtils
    {
        /*
            FIRESTORE DOCUMENTATION:

                https://firebase.google.com/docs/firestore/use-rest-api

                https://firebase.google.com/docs/firestore/reference/rest#rest-resource:-v1.projects.databases.documents

            C# FIRESTORE DOCUMENTATION (NOT USED SINCE WE NEED THE REST CLIENT FOR C++ ANYWAY)

                https://googleapis.github.io/google-cloud-dotnet/docs/Google.Cloud.Firestore/

            FireStore Configuration:

                https://firestore.googleapis.com/v1/projects/dash-520a7/databases/(default)/documents/users

                NOTE: URLS with "firebase.io" in them are NOT for FireStore.

            Step by Step Code Example of FireStore Auth (FOR LATER)
                
                https://stackoverflow.com/questions/56397947/creating-new-collection-and-document-with-firestore-rest-api-returning-http-400

            WORKING FIRESTORE REST API REQUESTS - USING CURL COMMANDLINE

                GET     read     curl -X GET "https://firestore.googleapis.com/v1/projects/dash-520a7/databases/(default)/documents/users/Kyk3743TKj0HhRG1Eyop/clients/1/metrics/lesson.completion.time/lessons/1"

                POST    create   curl -X POST -H "Content-Type: application/json" -d "{ 'fields': { 'label': { 'stringValue': 'posted' } } }" "https://firestore.googleapis.com/v1/projects/dash-520a7/databases/(default)/documents/users/yoh/clients/Apollo/metrics/gameplay.seconds.per.date/entries"
                
                PATCH   update   curl -X PATCH -H "Content-Type: application/json" -d "{ 'fields': { 'label': { 'stringValue': 'patched3' } } }" "https://firestore.googleapis.com/v1/projects/dash-520a7/databases/(default)/documents/users/yoh/clients/Apollo/metrics/gameplay.seconds.per.date/entries/template?updateMask.fieldPaths=label"
                        
                                    Requirements:   "?updateMask.fieldPaths=XXXX&updateMask.fieldPaths=YYYY" postfix for all fields being updated 

            HOW TO HAVE CURL SEND A JSON FILE TO A REST API:

                curl -X POST "url" -H "Content-Type: text/xml" --data-binary "@path/to/file"
            
                NOTE: THE @ SIGN IS REQUIRED TO READ FROM THE FILE.

            SAMPLE GET RESPONSE FROM FIRESTORE:

                * NOTE: FIRESTORE ADDS THE NAME, CREATETIME, UPDATETIME FIELDS.
                * NOTE: FIRESTORE REQUIRES THE KEY/VALUE PAIRS TO BE WRITTEN AS SUBVALUES OF THE "FIELDS" VALUE.
                * NOTE: FIRESTORE REQUIRES EACH SUBVALUE TO HAVE A SUBVALUE DENOTING IT'S TYPE AND VALUE.

                {
                  "name": "projects/dash-520a7/databases/(default)/documents/users/yoh/clients/Apollo/metrics/gameplay.seconds.per.date/entries/6odvzGlG4a9OVn0dEKfT",
                  "fields": {
                    "deviceId": {
                      "stringValue": "osType=Windows;deviceUuid=b62d11cb-4f18-4d97-ac82-a0790bb4446c"
                    },
                    "platform": {
                      "stringValue": "networkName=DESKTOP-REFJUPB;os=Microsoft Windows NT 6.2.9200.0;device=null"
                    },
                    "clientId": {
                      "stringValue": "id=Apollo;version=1.0"
                    },
                    "label": {
                      "timestampValue": "2020-01-23T20:45:16.709517Z"
                    },
                    "value": {
                      "integerValue": "16"
                    }
                  },
                  "createTime": "2020-01-23T20:45:16.685288Z",
                  "updateTime": "2020-01-23T20:45:16.685288Z"
                }
        */


        /*  Reads a document from Firebase. Each document contains key/value pairs.

            path:   path within firebase            (e.g. 'users;clients;metrics;lessons')
            keys:   key to each segment of path     (e.g. ['user1', 'client1', 'metric1', 'lesson1'])
        */
        //public static Dictionary<String, Object> getFirestoreDocument(string path, string[] keys)
        //{
        //    string json = getFirestoreJson(path, keys);
        //    object result = RestUtils.jsonToObject();
        //    return jsonResponse;
        //}

        public static string getFirestoreJson(string path, string[] keys)
        {
            int timeoutMs = 2000;
            string url = getFirestoreUrl(path, keys);
            string jsonResponse = RestUtils.restGet(url, timeoutMs); // Send it to Firebase
            return jsonResponse;
        }

        public static string putFirestoreJson(string path, string[] keys, string json)
        {
            int timeoutMs = 1000 * 5;
            string url = getFirestoreUrlShortenedBy(path, keys, 1) + ".json";
            string restJson = "{ \"" + keys.Last() + "\": " + json + " }";
            string jsonResponse = RestUtils.restPut(url, restJson, timeoutMs);
            return jsonResponse;
        }

        // If the last key is null, then we let FireStore give us a key for the new document.
        // Otherwise, we try to use the last key as the new key.
        public static string postFirestoreJson(string path, string[] keys, string json)
        {
            int timeoutMs = 1000 * 5;
            string url = getFirestoreUrlShortenedBy(path, keys, 1);                  // Don't use the last key, since we're targetting the collection
            string lastValueString = firestoreValueOf(keys.Last());                  // If they provided the last key, that will become the key for the document
            url += (keys.Last() == null) ? "" : "?documentId=" + lastValueString;   // If that key was provided, ask FireStore to use it as the key
            string jsonResponse = RestUtils.restPost(url, json, timeoutMs);
            return jsonResponse;
        }

        // https://firebase.google.com/docs/firestore/reference/rest/v1/projects.databases.documents/delete
        public static string deleteDocument(string path, string[] keys)
        {
            int timeoutMs = 1000 * 5;
            string url = getFirestoreUrl(path, keys);
            string jsonResponse = RestUtils.restDelete(url, timeoutMs);
            return jsonResponse;
        }

        // Creates the JSON representation of a document's key/value pairs needed by firestore.
        public static string toFirestoreJson(params object[] keyValuePairs)
        {
            string json = "{ 'fields': { \n";
            // https://angularfirebase.com/snippets/how-to-format-document-data-for-the-firestore-rest-api/
            // https://cloud.google.com/firestore/docs/reference/rest/v1/Value
            for (int i = 0; i < keyValuePairs.Length; i += 2)
            {
                string key = (String)keyValuePairs[i];
                object value = keyValuePairs[i + 1];
                string valueType = firestoreTypeOf(value);
                string valueString = firestoreValueOf(value);
                json += $"      '{key}': {{ '{valueType}': '{valueString}' }},\n";
            }

            json += "}}";
            return json;
        }

        private static string firestoreValueOf(object value)
        {
            https://angularfirebase.com/snippets/how-to-format-document-data-for-the-firestore-rest-api/
            String valueString =
                value is DateTime ? FirestoreUtils.toFirestoreTimestampString((DateTime)value)
                : "" + value;
            return valueString;
        }

        private static string firestoreTypeOf(object value)
        {
            // https://cloud.google.com/firestore/docs/reference/rest/v1/Value
            String subkey =
                value is DateTime ? "timestampValue" :
                value is bool ? "booleanValue" :
                value is float ? "doubleValue" :
                value is double ? "doubleValue" :
                value is Int32 ? "integerValue" :
                value is Int64 ? "integerValue" :
                "stringValue";
            return subkey;
        }

        /*
   Expands a REST API path to include the provided key/id for each segment.
   Input:  { "users;clients;metrics;lessons", ["...", "...", "...", "..."] } 
   Output:   getApiRoot() + "users/Kyk3743TKj0HhRG1Eyop/clients/1/metrics/lesson.completion.time/lessons/1"
*/
        private static string getFirestoreUrl(string path, string[] keys)
        {
            string[] segments = path.Split(';');
            string result = "";
            for (int i = 0; i < segments.Length; i++)
            {
                string segmentSeparator = (i != segments.Length - 1) ? "/" : "";
                result += segments[i] += "/" + keys[i] + segmentSeparator;
            }
            return getFirestoreProjectPrefix() + result;
        }

        // Removes a few segments from the tail of the generated URL. Useful for accessing collections vs. documents.
        private static string getFirestoreUrlShortenedBy(string path, string[] keys, int tailSegmentsToRemove)
        {
            string url = getFirestoreUrl(path, keys);
            string result = url.Substring(0, url.LastIndexOf("/"));
            return result;
        }

        // The base URL for accessing our project's FireStore API.
        private static string getFirestoreProjectPrefix()
        {
            return "https://firestore.googleapis.com/v1/projects/dash-520a7/databases/(default)/documents/";
        }

        public static string toFirestoreValueString(object value)
        {
            String valueString = value is DateTime
                ? FirestoreUtils.toFirestoreTimestampString((DateTime)value)
                : "" + value;
            return valueString;
        }

        public static string toFirestoreTimestampString(DateTime date)
        {
            return date.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ");
        }

        public static string getNewDocumentIdFromPostResponse(string jsonResponse)
        {
            string prefix = ": \"";
            int start = jsonResponse.IndexOf(prefix) + prefix.Length;
            int length = jsonResponse.IndexOf("\"", start) - start;
            string newPath = jsonResponse.Substring(start, length);
            string newId = newPath.Substring(newPath.LastIndexOf("/") + 1);
            return newId;
        }
    }
}
