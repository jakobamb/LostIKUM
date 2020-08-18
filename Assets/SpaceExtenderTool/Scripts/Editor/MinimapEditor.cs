using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(Minimap))]
public class MinimapEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
    public override VisualElement CreateInspectorGUI()
    {
        var visualTree = Resources.Load("SpaceExtenderTool/UXML/Minimap_Inspector") as VisualTreeAsset;
        var uxml = visualTree.CloneTree();
        uxml.Q<Button>("regenerate-map").clickable.clicked += () => {
            (target as Minimap).RegenerateMap();
        };

        return uxml;
    }
}