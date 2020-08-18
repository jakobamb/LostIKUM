
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomEditor(typeof(OverlappingRedirector))]
public class OverlappingRedirectorEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        var baseUxml = base.CreateInspectorGUI();
        var visualTree = Resources.Load("SpaceExtenderTool/UXML/OverlappingRedirector_Inspector") as VisualTreeAsset;
        var uxml = visualTree.CloneTree();

        uxml.Add(baseUxml);

        return uxml;
    }
}