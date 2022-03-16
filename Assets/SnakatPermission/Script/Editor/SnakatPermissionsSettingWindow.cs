using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Snakat.Editor
{
    [Serializable]
    internal class PermissionInfo
    {
        public string Name;
        public bool Enabled = false;
        public string Flag;
        public string[] UsageDescriptionKeys;
        public string[] UsageDescriptionValues;
    }

    [Serializable]
    internal class SettingInfo
    {
        public PermissionInfo[] Permissions;

        public static readonly SettingInfo @default = new SettingInfo ()
        {
            Permissions = new PermissionInfo[] {
                new PermissionInfo ()
                {
                    Name = "Bluetooth",
                    Flag = "PERMISSION_BLUETOOTH",
                },
                new PermissionInfo ()
                {
                    Name = "Camera",
                    Flag = "PERMISSION_CAMERA",
                    UsageDescriptionKeys = new string[] { "NSCameraUsageDescription" },
                    UsageDescriptionValues = new string[] { "" },
                },
                new PermissionInfo ()
                {
                    Name = "Microphone",
                    Flag = "PERMISSION_MICROPHONE",
                    UsageDescriptionKeys = new string[] { "NSMicrophoneUsageDescription" },
                    UsageDescriptionValues = new string[] { "" },
                },
                new PermissionInfo ()
                {
                    Name = "Contacts",
                    Flag = "PERMISSION_CONTACTS",
                    UsageDescriptionKeys = new string[] { "NSContactsUsageDescription" },
                    UsageDescriptionValues = new string[] { "" },
                },
                new PermissionInfo ()
                {
                    Name = "Events",
                    Flag = "PERMISSION_EVENTS",
                    UsageDescriptionKeys = new string[] { "NSCalendarsUsageDescription" },
                    UsageDescriptionValues = new string[] { "" },
                },
                new PermissionInfo ()
                {
                    Name = "Reminders",
                    Flag = "PERMISSION_REMINDERS",
                    UsageDescriptionKeys = new string[] { "NSRemindersUsageDescription" },
                    UsageDescriptionValues = new string[] { "" },
                },
                new PermissionInfo ()
                {
                    Name = "Location",
                    Flag = "PERMISSION_LOCATION",
                    UsageDescriptionKeys = new string[] { "NSLocationAlwaysAndWhenInUseUsageDescription", "NSLocationWhenInUseUsageDescription" },
                    UsageDescriptionValues = new string[] { "", "" },
                },
                new PermissionInfo ()
                {
                    Name = "Location",
                    Flag = "PERMISSION_LOCATION",
                    UsageDescriptionKeys = new string[] { "NSLocationAlwaysUsageDescription" },
                    UsageDescriptionValues = new string[] { "" },
                },
                new PermissionInfo ()
                {
                    Name = "Motion",
                    Flag = "PERMISSION_MOTION",
                    UsageDescriptionKeys = new string[] { "NSMotionUsageDescription" },
                    UsageDescriptionValues = new string[] { "" },
                },
                new PermissionInfo ()
                {
                    Name = "Photos",
                    Flag = "PERMISSION_PHOTOS",
                    UsageDescriptionKeys = new string[] { "NSPhotoLibraryUsageDescription" },
                    UsageDescriptionValues = new string[] { "" },
                },
                new PermissionInfo ()
                {
                    Name = "MediaLibrary",
                    Flag = "PERMISSION_MEDIA_LIBRARY",
                    UsageDescriptionKeys = new string[] { "NSAppleMusicUsageDescription" },
                    UsageDescriptionValues = new string[] { "" },
                },
                new PermissionInfo ()
                {
                    Name = "SpeechRecognizer",
                    Flag = "PERMISSION_SPEECH_RECOGNIZER",
                    UsageDescriptionKeys = new string[] { "NSSpeechRecognitionUsageDescription", "NSMicrophoneUsageDescription" },
                    UsageDescriptionValues = new string[] { "", "" },
                },
                new PermissionInfo ()
                {
                    Name = "Siri",
                    Flag = "PERMISSION_SIRI",
                    UsageDescriptionKeys = new string[] { "NSSiriUsageDescription" },
                    UsageDescriptionValues = new string[] { "" },
                },
                new PermissionInfo ()
                {
                    Name = "AppTrackingTransparency",
                    Flag = "PERMISSION_APP_TRACKING_TRANSPARENCY",
                    UsageDescriptionKeys = new string[] { "NSUserTrackingUsageDescription" },
                    UsageDescriptionValues = new string[] { "" },
                },
                new PermissionInfo ()
                {
                    Name = "Notifications",
                    Flag = "PERMISSION_NOTIFICATIONS",
                },
            },
        };

        internal PermissionInfo[] GetEnabledPermissions ()
        {
            return Permissions.Where (it => it.Enabled).ToArray ();
        }

        private const string FILE_PATH = "Assets/Plugins/iOS/Editor/SnakatPermissionSetting.json";

        public static SettingInfo Load ()
        {
            return Load (true);
        }

        public static SettingInfo Load (bool returnDefault)
        {
            if (File.Exists (FILE_PATH)) {
                var json = File.ReadAllText (FILE_PATH);
                return JsonUtility.FromJson<SettingInfo> (json);
            }

            if (returnDefault) {
                return @default;
            }

            return null;
        }

        public void Save ()
        {
            var dirPath = Path.GetDirectoryName (FILE_PATH);
            if (!Directory.Exists (dirPath)) {
                Directory.CreateDirectory (FILE_PATH);
            }
            File.WriteAllText (FILE_PATH, JsonUtility.ToJson (this, true));
        }
    }

    internal class SnakatPermissionSettingWindow : EditorWindow
    {
        [MenuItem ("Snakat/Permissions/Settings")]
        public static void Open ()
        {
            var window = GetWindow<SnakatPermissionSettingWindow> (true, "SnakatPermissionSetting");
            window.Init ();
        }

        private const string DEPENDENCIES_TEMPLATE = @"
<dependencies>
  <iosPods>{0}
  </iosPods>
</dependencies>";
        private const string IOS_POD_TEMPLATE = @"
    <iosPod name=""Permission/{0}"" addToAllTargets=""false"">
      <sources>
        <source>https://github.com/snakat/CocoaPods.git</source>
      </sources>
    </iosPod>";

        private SettingInfo _SettingInfo;

        private void Init ()
        {
            _SettingInfo = SettingInfo.Load ();
        }

        private void UpdateDependenciesXml ()
        {
            var xmlPath = Path.Combine ("Assets/Plugins/iOS/Editor/", "SnakatPermissionDependencies.xml");

            var iosPods = "";
            foreach (var item in _SettingInfo.GetEnabledPermissions ()) {
                var name = item.Name;
                if (string.IsNullOrEmpty (name)) {
                    continue;
                }

                iosPods += string.Format (IOS_POD_TEMPLATE, name);
            }

            File.WriteAllText (xmlPath, string.Format (DEPENDENCIES_TEMPLATE, iosPods));

            AssetDatabase.Refresh ();
        }

        public Vector2 scrollPosition = Vector2.zero;
        void OnGUI ()
        {
            if (_SettingInfo == null) {
                return;
            }

            GUILayout.BeginVertical ();
            GUILayout.Space (20);
            scrollPosition = GUILayout.BeginScrollView (scrollPosition);
            GUILayout.BeginVertical ();
            foreach (var item in _SettingInfo.Permissions) {
                GUI.enabled = true;
                GUILayout.Space (10);
                GUILayout.BeginHorizontal ();
                GUILayout.Space (10);
                item.Enabled = GUILayout.Toggle (item.Enabled, item.Name);
                GUILayout.EndHorizontal ();
                if (item.Enabled && item.UsageDescriptionKeys != null) {
                    for (var i = 0; i < item.UsageDescriptionKeys.Length; i++) {
                        var usageDescriptionKey = item.UsageDescriptionKeys[i].Trim ();
                        if (!string.IsNullOrEmpty (usageDescriptionKey)) {
                            GUILayout.BeginHorizontal ();
                            GUILayout.Space (40);
                            GUILayout.Label (string.Format ("{0}:", usageDescriptionKey));
                            GUILayout.EndHorizontal ();
                            GUILayout.BeginHorizontal ();
                            GUILayout.Space (50);
                            item.UsageDescriptionValues[i] = GUILayout.TextField (item.UsageDescriptionValues[i]);
                            GUILayout.Space (10);
                            GUILayout.EndHorizontal ();
                        }
                    }
                }
            }
            GUILayout.EndVertical ();
            GUILayout.EndScrollView ();

            GUI.enabled = true;
            GUILayout.Space (20);
            if (GUILayout.Button ("Save")) {
                _SettingInfo.Save ();
                UpdateDependenciesXml ();
            }
            if (GUILayout.Button ("Reset")) {
                _SettingInfo = SettingInfo.Load ();
            }
            if (GUILayout.Button ("Default")) {
                _SettingInfo = SettingInfo.@default;
            }
            GUILayout.Space (20);
            GUILayout.EndVertical ();
        }
    }
}
