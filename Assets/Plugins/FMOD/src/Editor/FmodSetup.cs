using UnityEditor;
using UnityEngine;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;

public class FmodSetup : EditorWindow
{
    public static FmodSetup Instance { get; private set; }
    public static bool IsOpen
    {
        get { return Instance != null; }
    }

    static bool trigger;

    const string CacheAssetName = "FMODStudioCache";
    const string CacheAssetFullName = "Assets/Plugins/FMOD/Resources/" + CacheAssetName + ".asset";
    EventCache events;

    int index;

    List<string> refString = new List<string>();

    GameObject go;

    void OnEnable()
    {
        Instance = this;
        trigger = false;

        events = AssetDatabase.LoadAssetAtPath(CacheAssetFullName, typeof(EventCache)) as EventCache;
        refString.Add("");
        foreach (var item in events.EditorEvents)
        {
            refString.Add(item.ToString().Replace(" (FMODUnity.EditorEventRef)", "")); 
        }
    }

    [MenuItem("Window/Fmod Editor %f")]
    public static void ShowWindow()
    {
        FmodSetup window = (FmodSetup)EditorWindow.GetWindow(typeof(FmodSetup));
        if (!FmodSetup.trigger)
        {
            window.Show();
        }
        else
        {
            window.Close();
        }
        FmodSetup.trigger = !FmodSetup.trigger;
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Event to Apply", EditorStyles.boldLabel);
        index = EditorGUILayout.Popup(index, refString.ToArray());
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Event Apply to prefab", EditorStyles.boldLabel);
        go = (GameObject)EditorGUILayout.ObjectField(go, typeof(GameObject), true);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Apply Event"))
        {
            go.GetComponent<soundFmod>().sounds.Event = refString[index];
        }
        EditorGUILayout.EndHorizontal();
    }
}
