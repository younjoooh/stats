using System;
using System.Diagnostics;

/*
 * Stats        List of StatIds we can operate on.
 * StatUtils    Functions to read/write DB-backed stats.
 * 
 * @rolivares
 */
namespace estem
{
    /*
     * Database IDs for supported statistics.
     * 
     * @rolivares
     */
    class Stats
    {
        public const string login_time_per_date = "login.time.per.date";                        // Time played during the games runtime (dont include video/narrative, etc)
        public const string logout_time_per_date = "logout.time.per.date";                      // Time played during the games runtime (dont include video/narrative, etc)

        public const string gameplay_seconds_per_date = "gameplay.seconds.per.date";            // Total Time played (per session) (dont include video/narrative, etc)
        public const string gameplay_seconds_per_lesson = "gameplay.seconds.per.lesson";        // Time played in each lesson area (per session)

        public const string completions_per_lesson = "completions.per.lesson";                  // Reported each time they complete a lesson
        public const string completion_seconds_per_lesson = "completion.seconds.per.lesson";    // Reported each time they complete a lesson

        public static string[] allIds()
        {
            return new string[] {
                Stats.login_time_per_date,
                Stats.logout_time_per_date,
                Stats.gameplay_seconds_per_date,
                Stats.gameplay_seconds_per_lesson,
                Stats.completions_per_lesson,
                Stats.completion_seconds_per_lesson
            };
        }
    }

    /*
     * Core functions for reading/writing stats to the database.
     * See ClientUtils and Stats class for how to get required Ids.
     * 
     * @rolivares
     */
    class StatUtils
    {
        public static string getStat(string userId, string statId, object x)
        {
            string clientId = ClientUtils.getClientId();            // Apollo's ClientID
            string xId = FirestoreUtils.toFirestoreValueString(x);    // Convert the "x" value to the ID of the doc (converts datetimes to UTC)
            string path = "users;clients;metrics;entries";          // Location in DB
            string[] keys = { userId, clientId, statId, xId };    // Keys used in location
            string jsonResponse = FirestoreUtils.getFirestoreJson(path, keys);
            //FirebaseMetric metric = new FirebaseMetric(metricId, x, y);     // Create a metric object
            return jsonResponse;
        }

        /*
         * Creates a new metric data point in FireStore.
         */
        public static string postStat(string userId, string statId, object x, object y)
        {
            string clientId = ClientUtils.getClientId();                // Apollo's ClientID
            string path = "users;clients;metrics;entries";              // Location in DB
            string[] keys = { userId, clientId, statId, null };       // Keys used in location; new key will be returned in response
            FirestoreStat metric = new FirestoreStat(statId, x, y); // Create a metric object
            string json = metric.ToJson();                              // Convert it to the typed JSON required by Firebase
            string jsonResponse = FirestoreUtils.postFirestoreJson(path, keys, json); // Send it to Firebase
            return jsonResponse; // Return FireStore's response, which has the new ID of the doc it created.
        }

        /*
         * Overwrites the value of an existing metric data point.
         */
        public static string putStat(string userId, string statId, object x, object y)
        {
            string clientId = ClientUtils.getClientId();            // Apollo's ClientID
            string xId = FirestoreUtils.toFirestoreValueString(x);    // Convert the "x" value to the ID of the doc
            string path = "users;clients;metrics;entries";          // Location in DB
            string[] keys = { userId, clientId, statId, xId };    // Keys used in location
            FirestoreStat stat = new FirestoreStat(statId, x, y);     // Create a metric object
            string json = stat.ToJson();  // Convert it to the typed JSON required by Firebase
            string jsonResponse = FirestoreUtils.putFirestoreJson(path, keys, json); // Send it to Firebase
            return jsonResponse;
        }
    }

    /*  Encapsulates a metric "data point"
     *  Has an x, y value.
     *  Can be converted to the special JSON required by FireStore.
     *  @rolivares
     */
    public class FirestoreStat
    {
        public string metricName;   // Not used, but helpful for keeping code easy to read
        public object x;            // Translates to the "label: xxx" field in FireStore docs.
        public object y;            // Translates to the "value: yyy" field in FireStore docs.

        public FirestoreStat(String name, object x, object y)
        {
            this.metricName = name;
            this.x = x;
            this.y = y;
        }

        public string ToJson()
        {
            object[] fields = ClientUtils.getClientStampFieldsAnd(
                "label", x,
                "value", y
                );
            string json = FirestoreUtils.toFirestoreJson(fields);
            return json;
        }
    }

    public class DebugUtils
    {
        public static void debug(string s)
        {
            //   Debug.WriteLine(s);
            UnityEngine.Debug.Log(s);
        }
    }

    public class StatTests : DebugUtils
    {
        // =============================================================================================================================
        // UNIT TESTS
        // =============================================================================================================================

        public static void testCompleteGameClientDataFlow()
        {
            string userId = "jerron-test-student-" + FirestoreUtils.toFirestoreValueString(DateTime.Now);
            string createJson = ClientUtils.createUserAccount(userId); // <-- should only be done through website

            // Call at login
            DateTime now = DateTime.Now;
            ClientUtils.loginUser(userId); //-> exception means user not found; stats won't write.
            StatUtils.postStat(userId, Stats.login_time_per_date, now.Date, now);

            // Call upon leaving a lesson area
            StatUtils.postStat(userId, Stats.gameplay_seconds_per_lesson, 1, 20);
            //StatUtils.postStat(userId, Stats.gameplay_seconds_per_lesson, 1, 40);

            StatUtils.postStat(userId, Stats.gameplay_seconds_per_lesson, 2, 50);
            //StatUtils.postStat(userId, Stats.gameplay_seconds_per_lesson, 2, 70);

            // Call upon completing a lesson
            int lessonNumber = 1;
            int always1 = 1;
            StatUtils.postStat(userId, Stats.completions_per_lesson, lessonNumber, always1);
            StatUtils.postStat(userId, Stats.completion_seconds_per_lesson, 1, 60);

            //StatUtils.postStat(userId, Stats.completions_per_lesson, 2, 1);
            //StatUtils.postStat(userId, Stats.completion_seconds_per_lesson, 2, 70);

            // Call upon logout / game shutdown
            StatUtils.postStat(userId, Stats.logout_time_per_date, now.Date, now);

            // Call after logout
            StatUtils.postStat(userId, Stats.gameplay_seconds_per_date, now, 360);

            //string deleteJson = FirestoreUtils.deleteDocument(
            //    ClientUtils.Path.forUsers,
            //    ClientUtils.Keys.forUser(userId)
            //);
        }

        public static void testFirebaseJson()
        {
            DateTime now = DateTime.Now;
            debug(FirestoreUtils.toFirestoreJson());
            debug(FirestoreUtils.toFirestoreJson("label", 1, "value", 100));
            debug(FirestoreUtils.toFirestoreJson("label", 1.12345, "value", 100.12345));
            debug(FirestoreUtils.toFirestoreJson("label", "label-string", "value", "value-string"));
            debug(FirestoreUtils.toFirestoreJson("label", now, "value", now));
            debug(FirestoreUtils.toFirestoreJson(
                "label", "test",
                "value", now,
                "x", 1,
                "y", 100.12345
            ));
        }

        public static void testCreateReadAndDeleteUser()
        {
            string userId = "csharp-test-user" + FirestoreUtils.toFirestoreValueString(DateTime.Now);
            string createJson = ClientUtils.createUserAccount(userId);
            string userJson = ClientUtils.loginUser(userId);
            string deleteJson = FirestoreUtils.deleteDocument(
                ClientUtils.Path.forUsers,
                ClientUtils.Keys.forUser(userId)
            );
        }

        public static void testDeleteUsers()
        {
            string junkString =
@"
csharp-test-student-2020-01-25T21:27:41.238489Z
csharp-test-student-2020-01-25T21:32:39.614761Z
csharp-test-student-2020-01-25T21:15:08.620773Z
csharp-test-student-2020-01-25T21:17:59.877119Z
csharp-test-student-2020-01-25T21:18:44.214676Z
csharp-test-student-2020-01-25T21:20:46.769522Z
";
            string[] junkIds = junkString.Split('\n');
            foreach (string id in junkIds)
            {
                if (id.Trim() == "")
                    continue;
                FirestoreUtils.deleteDocument(
                    ClientUtils.Path.forUsers,
                    ClientUtils.Keys.forUser(id)
                );
            }
        }

        public static void testGetPostGetGameplaySecondsPerDate()
        {
            try
            {
                String userId = ClientUtils.getUserId(); // E.g. "yoh"
                String metricId = Stats.gameplay_seconds_per_date;

                DateTime x = DateTime.Now;  // X-VALUE ON THE DATA POINT DOC IS THE CURRENT TIMESTAMP
                int y = x.Second;           // Y-VALUE IS THE TIMESTAMP'S SECOND VALUE

                // TRY TO GET, THEN CREATE, THEN GET DOCUMENTS FROM THE DB
                debug("\nTest GET/POST/GET Metric Document: {" + userId + ", " + ClientUtils.getClientId() + ", " + metricId + ", " + FirestoreUtils.toFirestoreValueString(x));

                // GET THE 'template' METRIC DOC FROM THE DB
                string metricJson = StatUtils.getStat(userId, metricId, "template");
                debug("\nGET 'template' document - Response JSON:\n" + metricJson);

                // POST A NEW METRIC DOC TO THE DB
                String postResponseJson = StatUtils.postStat(userId, metricId, x, y);
                debug("\nPOST new document - Response JSON:\n" + postResponseJson);

                // GET THE NEW ID OF THE METRIC DOC WE JUST POSTED TO THE DB
                string newId = FirestoreUtils.getNewDocumentIdFromPostResponse(postResponseJson);
                debug("\nNew Document's ID: " + newId);

                // READ THE METRIC DOC WE JUST POSTED TO THE DB
                metricJson = StatUtils.getStat(userId, metricId, newId);
                debug("\nGET Response JSON:\n" + metricJson);
            }
            catch (Exception e)
            {
                debug("Exception:\n" + e.ToString());
            }
        }
       
    } // END TESTS

} // END NAMESPACE
