using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;

namespace Snakat.Editor
{
    public class SnakatPermissionsPostprocessBuild : IPostprocessBuildWithReport
    {
        private const string _PERMISSION_FLAGS = "PERMISSION_FLAGS";

        public int callbackOrder {
            get {
                return 45;
            }
        }

        public void OnPostprocessBuild (BuildReport report)
        {
            var platform = report.summary.platform;
            if (platform != BuildTarget.iOS) {
                return;
            }

            var settings = SettingInfo.Load (false);
            var permissions = settings.GetEnabledPermissions ();

            var outputPath = report.summary.outputPath;

            UpdatePBXProject (outputPath, permissions);
            UpdatePlistDocument (outputPath, permissions);
        }

        private static void UpdatePBXProject (string buildPath, PermissionInfo[] permissions)
        {
            var projPath = Path.Combine (buildPath, "Unity-iPhone.xcodeproj/project.pbxproj");
            var proj = new PBXProject ();
            proj.ReadFromFile (projPath);

#if UNITY_2019_3_OR_NEWER
            var appTarget = proj.GetUnityFrameworkTargetGuid ();
#else
            var appTarget = proj.TargetGuidByName ("UnityFramework");
#endif

            var permissionFlags = "";
            foreach (var permission in permissions) {
                var flag = permission.Flag;
                proj.SetBuildProperty (appTarget, flag, flag);
                permissionFlags += string.Format ("$({0}) ", flag);
            }
            proj.SetBuildProperty (appTarget, _PERMISSION_FLAGS, permissionFlags);

            foreach (var buildConfigName in proj.BuildConfigNames ()) {
                var buildConfig = proj.BuildConfigByName (appTarget, buildConfigName);
                var current = proj.GetBuildPropertyForConfig (buildConfig, "SWIFT_ACTIVE_COMPILATION_CONDITIONS");
                if (current != null && current.Contains (string.Format ("$({0})", _PERMISSION_FLAGS))) {
                    continue;
                }
                proj.SetBuildPropertyForConfig (buildConfig, "SWIFT_ACTIVE_COMPILATION_CONDITIONS", string.Format ("$({0}) {1}", _PERMISSION_FLAGS, current));
            }

            proj.WriteToFile (projPath);
        }

        private static void UpdatePlistDocument (string buildPath, PermissionInfo[] permissions)
        {
            var plistPath = Path.Combine (buildPath, "Info.plist");
            var plist = new PlistDocument ();
            plist.ReadFromFile (plistPath);

            foreach (var permission in permissions) {
                if (permission.UsageDescriptionKeys != null) {
                    for (var i = 0; i < permission.UsageDescriptionKeys.Length; i++) {
                        var key = permission.UsageDescriptionKeys[i].Trim ();
                        if (string.IsNullOrEmpty (key)) {
                            continue;
                        }
                        var value = permission.UsageDescriptionValues[i].Trim ();
                        plist.root.SetString (key, value);
                    }
                }
            }

            plist.WriteToFile (plistPath);
        }
    }
}
