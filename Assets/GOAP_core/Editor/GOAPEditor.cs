using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class GOAPEditor : EditorWindow
{
    [MenuItem("Window/GOAP/GOAPEditor")]
    public static void OpenWindow()
    {
        GOAPEditor wnd = GetWindow<GOAPEditor>();
        wnd.titleContent = new GUIContent("GOAPEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/GOAP_core/Editor/GOAPEditor.uxml");
        visualTree.CloneTree(root);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/GOAP_core/Editor/GOAPEditor.uss");
        root.styleSheets.Add(styleSheet);

        _goapView = root.Q<GoapTreeView>();
        _inspectorView = root.Q<InspectorView>();
        _goapView.OnNodeSelected = OnNodeSelectionChanged;
        OnSelectionChange();
    }

    private GoapTreeView _goapView;
    private InspectorView _inspectorView;
    private void OnSelectionChange()
    {
        AgentView goap = Selection.activeObject as AgentView;
        if (goap != null)
        {
            _goapView.PopulateView(goap);
        }
    }

    void OnNodeSelectionChanged(NodeView node)
    {
        _inspectorView.UpdateSelection(node);
    }
}