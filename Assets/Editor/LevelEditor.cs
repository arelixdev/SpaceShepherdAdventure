using UnityEditor;
using UnityEngine;

public class LevelEditor : EditorWindow
{
    public static LevelEditor Instance { get; private set; }
    public static bool IsOpen
    {
        get { return Instance != null; }
    }

    static bool trigger;

    GameObject go;
    Transform parent;

    float decalage;

    bool showRota;
    float minVal, maxVal;

    bool showScale;
    float minValS, maxValS;

    bool showPlayer;
    GameObject player;

    void OnEnable()
    {
        Instance = this;
        trigger = false;
        SceneView.duringSceneGui += OnSceneGUI;
        maxVal = 360;
        maxValS = 10;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/Level Editor %g")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        //EditorWindow.GetWindow(typeof(PropSprinklerEditor));
        //EditorWindow.GetWindow<LevelEditor>("Draw Ray");
        LevelEditor window = (LevelEditor)EditorWindow.GetWindow(typeof(LevelEditor));
        if (!LevelEditor.trigger)
        {
            window.Show();
        }
        else
        {
            window.Close();
        }
        LevelEditor.trigger = !LevelEditor.trigger;
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        var centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.UpperCenter;
        centeredStyle.fontStyle = FontStyle.Bold;
        GUILayout.Label("Spawn Prefab On Scene", centeredStyle);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Prefab to spawn", EditorStyles.boldLabel);
        go = (GameObject)EditorGUILayout.ObjectField(go, typeof(GameObject), true);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        parent = (Transform)EditorGUILayout.ObjectField(parent, typeof(Transform), true);
        if (GUILayout.Button("Reset Parent"))
        {
            parent = null;
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        decalage = EditorGUILayout.FloatField("Offset Pivot", decalage);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Random Rotation");
        showRota = GUILayout.Toggle(showRota, "");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (showRota)
        {
            //GUILayout.Label(Mathf.Ceil(minVal).ToString());
            minVal = Mathf.Clamp(int.Parse(GUILayout.TextField(Mathf.Ceil(minVal).ToString())), 0, maxVal);
            EditorGUILayout.MinMaxSlider(ref minVal, ref maxVal, 0, 360);
            //GUILayout.Label(Mathf.Ceil(maxVal).ToString());
            maxVal = Mathf.Clamp(int.Parse(GUILayout.TextField(Mathf.Ceil(maxVal).ToString())), minVal, 360);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Random Scale");
        showScale = GUILayout.Toggle(showScale, "");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (showScale)
        {
            //GUILayout.Label(Mathf.Ceil(minVal).ToString());
            minValS = Mathf.Clamp(float.Parse(GUILayout.TextField(minValS.ToString())), 0, maxValS);
            EditorGUILayout.MinMaxSlider(ref minValS, ref maxValS, 0, 10);
            //GUILayout.Label(Mathf.Ceil(maxVal).ToString());
            maxValS = Mathf.Clamp(float.Parse(GUILayout.TextField(maxValS.ToString())), minValS, 10);
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Move Object");
        showPlayer = GUILayout.Toggle(showPlayer, "");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (showPlayer)
        {
            GUILayout.Label("Object", EditorStyles.boldLabel);
            player = (GameObject)EditorGUILayout.ObjectField(player, typeof(GameObject), true);
        }
        EditorGUILayout.EndHorizontal();
    }

    void OnSceneGUI(SceneView SceneView)
    {
        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        Event e = Event.current;
        Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 10))
        {
            Handles.color = Color.red;
            Handles.DrawLine(hit.point, hit.point + hit.normal * 5f);
            Handles.DrawWireDisc(hit.point, hit.normal, 1f);
            HandleUtility.Repaint();
        }

        if (e.type == EventType.MouseDown)
        {
            if (true)
            {
                GameObject newProp = null;

                if (go != null && !showPlayer)
                {
                    switch (PrefabUtility.GetPrefabAssetType(go))
                    {
                        case PrefabAssetType.Regular:
                            newProp = (GameObject)PrefabUtility.InstantiatePrefab(go);
                            break;
                        case PrefabAssetType.NotAPrefab:
                            newProp = Instantiate(go);
                            break;
                    }
                }
                else if(player != null && showPlayer)
                {
                    newProp = player;
                }

                newProp.transform.parent = parent;
                newProp.transform.position = hit.point + newProp.transform.up * decalage;
                newProp.transform.eulerAngles = Vector3.zero;
                OrientBody(newProp.transform, hit.normal);
                if(showRota)
                {
                    if (newProp.GetComponent<SnapPlanet>())
                    {
                        newProp.GetComponent<SnapPlanet>().AngleWanted = Mathf.Ceil(Random.Range(minVal, maxVal));
                    }
                    else
                    {
                        Quaternion rotationY = Quaternion.AngleAxis(Mathf.Ceil(Random.Range(minVal, maxVal)), newProp.transform.up);
                        newProp.transform.rotation = rotationY * newProp.transform.rotation;
                    }
                }
                if(showScale)
                {
                    float randomScale = Random.Range(minValS, maxValS);
                    newProp.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
                }
            }
        }
    }

    void OrientBody(Transform attractedBody, Vector3 surfaceNorm)
    {
        Quaternion targetRotation = Quaternion.FromToRotation(attractedBody.transform.up, surfaceNorm);
        attractedBody.transform.rotation = targetRotation;
    }
}