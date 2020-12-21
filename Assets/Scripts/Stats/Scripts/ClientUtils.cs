using Microsoft.Win32;
using System;
using System.Linq;

namespace estem
{
    /*
     * Functions the Game Client should use to interact with backend users & data.
     * 
     * - User Login / Validation / Logout routines.
     * - Generation of data data paths and keys for areas of the database.
     * - Information about the game client device and platform.
     * 
     * @rolivares
     */
    class ClientUtils
    {
        // Needs to be set by the game client every time A NEW USER LOGS IN
        public static DateTime SessionLoginTime { get; set; } = DateTime.Now;

        // Throws an exception if login fails; otherwise returns the user's info
        // Call on Start
        public static string loginUser(string userId)
        {
            string responseJson = "";
            responseJson = FirestoreUtils.getFirestoreJson(
                Path.forUsers,
                Keys.forUser(userId)
            );
            SessionLoginTime = DateTime.Now;
            return responseJson;
        }

        // Experimental. Create an account and the necessary subdocuments / collections.
        public static String createUserAccount(string proposedUserId)
        {
            string responseJson = "";

            DebugUtils.debug("CREATE ACCOUNT: ");

            // Create user doc in user collection
            responseJson += FirestoreUtils.postFirestoreJson(
                Path.forUsers, Keys.forUser(proposedUserId),
                FirestoreUtils.toFirestoreJson(
                    getClientStampFields()
                )
            );
            DebugUtils.debug("1: " + responseJson);

            // Create clients collection and client doc
            responseJson += FirestoreUtils.postFirestoreJson(
                Path.forClients, new string[] { proposedUserId, getClientId() }, "{ 'fields': {} }"
            );
            DebugUtils.debug("2: " + responseJson);

            // Create metrics collection and each metric doc
            int i = 3;
            string[] metricIds = Stats.allIds();
            foreach (string metricId in metricIds)
            {
                // Emitting stats will create & populate the entries collection as a side effect.
                responseJson += FirestoreUtils.postFirestoreJson(
                    Path.forMetrics, new string[] { proposedUserId, getClientId(), metricId },
                    FirestoreUtils.toFirestoreJson()
                );
                DebugUtils.debug($"{i++}: " + responseJson);
            }

            return responseJson;
        }

        // FireStore DB paths
        public class Path
        {
            public const string forUsers = "users";
            public const string forClients = "users;clients";
            public const string forMetrics = "users;clients;metrics";
            public const string forEntries = "users;clients;metrics;entries";
        }

        // Keys for the logged in user
        public class Keys
        {
            public static string[] forUser()
            {
                return new string[] { getUserId() };
            }

            public static string[] forUser(string userId)
            {
                return new string[] { userId };
            }

            public static string[] forMetric(string metricId)
            {
                return new string[] { getUserId(), getClientId(), metricId };
            }

            public static string[] forEntry(string metricId, string entryId)
            {
                return new string[] { getUserId(), getClientId(), metricId, entryId };
            }
        }


        // The string representation of the username and login time; stamped on all stats emitted during a gaming session
        public static string getSessionId()
        {
            return "userId=" + getUserId() + ";login=" + FirestoreUtils.toFirestoreTimestampString(SessionLoginTime);
        }

        public static string getUserId()
        {
            return "yoh";
        }

        public static string getClientId()
        {
            return "apollo";
        }

        public static string getClientVersion()
        {
            return "1.0";
        }

        public static string getBuildNumber()
        {
            return "1"; // TODO - UPDATE WITH COMPILER DIRECTIVE?
        }

        public static string getBuildDate()
        {
            return "1/10/2020 9:00:00.0000am PST"; // TODO - UPDATE WITH COMPILER DIRECTIVE?
        }

        public static string getClientIdAndVersion()
        {
            return
                "id=" + getClientId() +
                ";version=" + getClientVersion() +
                ";build=" + getBuildNumber() +
                ";buildDate=" + getBuildDate();
        }

        // https://stackoverflow.com/questions/99880/generating-a-unique-machine-id
        // https://stackoverflow.com/questions/10926634/how-can-i-get-windows-product-key-in-c
        // https://www.nextofwindows.com/the-best-way-to-uniquely-identify-a-windows-machine
        public static string getDeviceId()
        {
            // TODO - Fix for Unity/iOS/Android/MacOS, etc.

            // https://stackoverflow.com/questions/9491958/registry-getvalue-always-return-null
            RegistryKey localKey;
            if (Environment.Is64BitOperatingSystem)
                localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            else
                localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);

            RegistryKey cryptoKey = localKey.OpenSubKey(@"SOFTWARE\Microsoft\Cryptography");

            string osType = "osType=" + "Windows"; // TODO - Need to get Android/iOS/MacOS, etc.
            string machineGuid = "deviceUuid=" + (String)cryptoKey.GetValue("MachineGuid");
            string[] parts = { osType, machineGuid };
            return String.Join(";", parts);
        }

        // https://answers.unity.com/questions/305490/how-to-tell-if-the-device-is-android-or-ios.html
        public static string getDevicePlatform()
        {
            // TODO - Fix for Unity/iOS/Android/MacOS, etc.
            string osNameAndVersion = "os=" + Environment.OSVersion.VersionString;

            // https://docs.unity3d.com/ScriptReference/SystemInfo.html
            string deviceNameAndVersion = "device=" + "null";  // UnityEngine.CoreModule.SystemInfo.deviceModel https://docs.unity3d.com/ScriptReference/SystemInfo-deviceModel.html

            string networkName = "networkName=" + Environment.MachineName;

            string[] parts = { networkName, osNameAndVersion, deviceNameAndVersion };
            return String.Join(";", parts);
        }

        public static object[] getClientStampFields()
        {
            return new object[] {
                "_sessionId", ClientUtils.getSessionId(),
                "_clientId",  ClientUtils.getClientIdAndVersion(),
                "_deviceId",  ClientUtils.getDeviceId(),
                "_platform",  ClientUtils.getDevicePlatform()
            };
        }

        public static object[] getClientStampFieldsAnd(params object[] keyValuePairs)
        {
            object[] fields = ClientUtils.getClientStampFields();
            return fields.Concat(keyValuePairs).ToArray();
        }
    }
}
