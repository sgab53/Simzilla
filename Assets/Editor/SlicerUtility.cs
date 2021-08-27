using UnityEngine;
using UnityEditor;

public class SlicerUtility : EditorWindow
{
    Vector2 scrollPosition = Vector2.zero;

    Vector2 buttonSize = new Vector2(96, 24);

    [SerializeField] static ShatterExample shatter;
    [SerializeField] static GameObject shatterObject;

    [SerializeField] Material sliceMaterial;

    [MenuItem("Window/Slicer")]
    public static void ShowWindow()
    {
        GetWindow<SlicerUtility>("Slicer Utility");
        if (shatterObject == null)
        {
            shatterObject = new GameObject("Shatter Object");
        }

        shatter = shatterObject.AddComponent<ShatterExample>();
    }

    private void OnGUI()
    {

        scrollPosition = GUI.BeginScrollView(new Rect(0, 0, position.width, position.height * 0.5f), scrollPosition, new Rect(0, 0, position.width / 2, position.height));

        foreach (var go in Selection.gameObjects)
        {
            GUILayout.Label(go.name);
        }

        GUI.EndScrollView();

        if (GUI.Button(new Rect(position.width * 0.5f - buttonSize.x,
            position.height * 0.5f + 12, buttonSize.x, buttonSize.y), "Slice All") &&
            Selection.count > 0)
        {
            shatterObject = GameObject.Find("Shatter Object");

            shatter = shatterObject.GetComponent<ShatterExample>();

            shatter.StartCoroutine(shatter.SliceAll(Selection.gameObjects));
        }
    }
}
