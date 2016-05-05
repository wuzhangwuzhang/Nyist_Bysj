using UnityEngine;
using UnityEditor;

public class CommandBuild
{
    /// <summary>
    /// 场景名称列表
    /// </summary>
    private static string[] ms_scenes =
    {
        "Assets/Scenes/first.unity",
        "Assets/Scenes/menue.unity",
        "Assets/Scenes/moive.unity",
        "Assets/Scenes/share.unity",
        "Assets/Scenes/Bmob.unity",
    };

    private static bool ms_isDebugBuild = false;
    private static BuildTarget ms_buildTarget = BuildTarget.Android;

    private static string XCODE_PROJECT_NAME = "XCodeProject";
    private static string BUILD_OUTPUT_ANDROID = "Bin/Android/";

    /// <summary>
    /// Apk的名字
    /// </summary>
    private static string APK_NAME = "SmartBaby";
    private static void UpdateBuildFlag()
    {
        string[] args = System.Environment.GetCommandLineArgs();
        foreach (string oneArg in args)
        {
            if (oneArg != null && oneArg.Length > 0)
            {
                if (oneArg.ToLower().Contains("-debug"))
                {
                    Debug.Log("\"-debug\" is detected, switch to debug build.");
                    ms_isDebugBuild = true;
                    return;
                }
                else if (oneArg.ToLower().Contains("-release"))
                {
                    Debug.Log("\"-release\" is detected, switch to release build.");
                    ms_isDebugBuild = false;
                    return;
                }
            }
        }

        if (ms_isDebugBuild)
        {
            Debug.Log("neither \"-debug\" nor \"-release\" is detected, current is to debug build.");
        }
        else
        {
            Debug.Log("neither \"-debug\" nor \"-release\" is detected, current is to release build.");
        }
    }
    private static void UpdateBuildTarget()
    {
        string[] args = System.Environment.GetCommandLineArgs();
        foreach (string oneArg in args)
        {
            if (oneArg != null && oneArg.Length > 0)
            {
                if (oneArg.ToLower().Contains("-android"))
                {
                    Debug.Log("\"-android\" is detected, switch build target to android.");
                    ms_buildTarget = BuildTarget.Android;
                    return;
                }
                else if (oneArg.ToLower().Contains("-iphone"))
                {
                    Debug.Log("\"-iphone\" is detected, switch build target to iphone.");
                    ms_buildTarget = BuildTarget.iPhone;
                    return;
                }
                else if (oneArg.ToLower().Contains("-ios"))
                {
                    Debug.Log("\"-ios\" is detected, switch build target to iphone.");
                    ms_buildTarget = BuildTarget.iPhone;
                    return;
                }
            }
        }

        Debug.Log("neither \"-android\", \"-ios\" nor \"-iphone\" is detected, current build target is: " + ms_buildTarget);
    }
    public static void PreBuild()
    {
        Debug.Log("PreBuild");
        UpdateBuildFlag();
        SetKgfDebugActive(ms_isDebugBuild);
    }
    public static void Build()
    {
        Debug.Log("Build");
        UpdateBuildTarget();

        BuildOptions buildOption = BuildOptions.None;
        if (ms_isDebugBuild)
        {
            buildOption |= BuildOptions.Development;
            buildOption |= BuildOptions.AllowDebugging;
            buildOption |= BuildOptions.ConnectWithProfiler;
        }
        else
        {
            buildOption |= BuildOptions.None;
        }

        string locationPathName;
        if (BuildTarget.iPhone == ms_buildTarget)
        {
            locationPathName = XCODE_PROJECT_NAME;
        }
        else
        {
            locationPathName = BUILD_OUTPUT_ANDROID;
            System.DateTime time = System.DateTime.Now;
            locationPathName +=time.Year.ToString("D2") + time.Month.ToString("D2") + time.Day.ToString("D2") +
            "_" + APK_NAME + ".apk";
        }
        BuildPipeline.BuildPlayer(ms_scenes, locationPathName, ms_buildTarget, buildOption);
    }
    public static void PostBuild()
    {
        Debug.Log("PostBuild");
    }

    private static void SetKgfDebugActive(bool activated)
    {
        Debug.Log("Debug Model");
    }
}