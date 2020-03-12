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

    bool fold;

    GameObject go;
    Transform parent;

    float decalage;

    bool showRota;
    float minVal, maxVal;

    bool showScale;
    float minValS, maxValS;

    bool showPlayer;
    GameObject player;

    CreateNode planet;
    bool showPlanet;
    bool bary;

    void OnEnable()
    {
        Instance = this;
        trigger = false;
        SceneView.duringSceneGui += OnSceneGUI;
        maxVal = 360;
        maxValS = 10;
        player = GameObject.FindGameObjectWithTag("Player");
        planet = GameObject.FindObjectOfType<CreateNode>();
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
        #region Spawn Object
        fold = EditorGUILayout.Foldout(fold, "Spawn");
        if (fold)
        {
            showPlayer = false;

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
            GUILayout.Space(20);
            showRota = EditorGUILayout.Foldout(showRota, "Random Rotation");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (showRota)
            {
                GUILayout.Space(50);
                //GUILayout.Label(Mathf.Ceil(minVal).ToString());
                minVal = Mathf.Clamp(int.Parse(GUILayout.TextField(Mathf.Ceil(minVal).ToString())), 0, maxVal);
                EditorGUILayout.MinMaxSlider(ref minVal, ref maxVal, 0, 360);
                //GUILayout.Label(Mathf.Ceil(maxVal).ToString());
                maxVal = Mathf.Clamp(int.Parse(GUILayout.TextField(Mathf.Ceil(maxVal).ToString())), minVal, 360);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(20);
            showScale = EditorGUILayout.Foldout(showScale, "Random Scale");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (showScale)
            {
                GUILayout.Space(50);
                //GUILayout.Label(Mathf.Ceil(minVal).ToString());
                minValS = Mathf.Clamp(float.Parse(GUILayout.TextField(minValS.ToString("F1"))), 0, maxValS);
                EditorGUILayout.MinMaxSlider(ref minValS, ref maxValS, 0, 10);
                //GUILayout.Label(Mathf.Ceil(maxVal).ToString());
                maxValS = Mathf.Clamp(float.Parse(GUILayout.TextField(maxValS.ToString("F1"))), minValS, 10);
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);
        }
        #endregion

        #region Move Object

        showPlayer = EditorGUILayout.Foldout(showPlayer, "Move Object");
        if (showPlayer)
        {
            fold = false;
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Object", EditorStyles.boldLabel);
            player = (GameObject)EditorGUILayout.ObjectField(player, typeof(GameObject), true);
            EditorGUILayout.EndHorizontal();

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
            GUILayout.Space(20);
            showRota = EditorGUILayout.Foldout(showRota, "Random Rotation");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (showRota)
            {
                GUILayout.Space(50);
                //GUILayout.Label(Mathf.Ceil(minVal).ToString());
                minVal = Mathf.Clamp(int.Parse(GUILayout.TextField(Mathf.Ceil(minVal).ToString())), 0, maxVal);
                EditorGUILayout.MinMaxSlider(ref minVal, ref maxVal, 0, 360);
                //GUILayout.Label(Mathf.Ceil(maxVal).ToString());
                maxVal = Mathf.Clamp(int.Parse(GUILayout.TextField(Mathf.Ceil(maxVal).ToString())), minVal, 360);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(20);
            showScale = EditorGUILayout.Foldout(showScale, "Random Scale");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (showScale)
            {
                GUILayout.Space(50);
                //GUILayout.Label(Mathf.Ceil(minVal).ToString());
                minValS = Mathf.Clamp(float.Parse(GUILayout.TextField(minValS.ToString("F1"))), 0, maxValS);
                EditorGUILayout.MinMaxSlider(ref minValS, ref maxValS, 0, 10);
                //GUILayout.Label(Mathf.Ceil(maxVal).ToString());
                maxValS = Mathf.Clamp(float.Parse(GUILayout.TextField(maxValS.ToString("F1"))), minValS, 10);
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);
        }
        #endregion

        #region Gestion Node;

        showPlanet = EditorGUILayout.Foldout(showPlanet, "Gestion Node");

        if(showPlanet)
        {
            if(planet == null)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.HelpBox("Missing GameObject with script 'Create Node'", MessageType.Warning);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Refresh Planet"))
                {
                    planet = GameObject.FindObjectOfType<CreateNode>();
                }
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                if (GUILayout.Button("Spawn Node"))
                {
                    //planet.GetComponent<CreateNode>().spawn = true;
                    planet.Spawn();
                }
                if (GUILayout.Button("Clear Node"))
                {
                    planet.ClearListEditor();
                }
                if (GUILayout.Button("Toggle Barycentre (" + planet.GetComponent<CreateNode>().barycentre + ")"))
                {
                    planet.barycentre = !planet.barycentre;
                }
                if (GUILayout.Button("Keep Force Node (" + planet.GetComponent<CreateNode>().keepForce + ")"))
                {
                    planet.keepForce = !planet.keepForce;
                }
            }
        }

        #endregion
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

        if (e.button == 0 && !Event.current.alt && e.type == EventType.MouseDown)
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
                        case PrefabAssetType.Model:
                            newProp = Instantiate(go);
                            break;
                    }
                }
                else if(player != null && showPlayer)
                {
                    newProp = player;
                }
                if (newProp != null)
                {
                    newProp.name = newProp.name.Replace("(Clone)", "");
                    newProp.transform.parent = parent;
                    newProp.transform.position = hit.point + newProp.transform.up * decalage;
                    newProp.transform.eulerAngles = Vector3.zero;
                    OrientBody(newProp.transform, hit.normal);
                    if (showRota)
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
                    if (showScale)
                    {
                        float randomScale = Mathf.Round(Random.Range(minValS, maxValS) * 10f) / 10f;
                        newProp.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
                    }
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