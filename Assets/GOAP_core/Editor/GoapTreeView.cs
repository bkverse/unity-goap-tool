using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System.Windows.Forms.DataVisualization.Charting;

using UnityEditor;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

using Unity.GOAP.ActionBase;
using Unity.GOAP.Goal;
public class GoapTreeView : GraphView
{
    public new class UxmlFactory : UxmlFactory<GoapTreeView, GraphView.UxmlTraits>
    {

    }

    public Action<NodeView> OnNodeSelected;
    public GoapTreeView()
    {
        Insert(0, new GridBackground());

        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/GOAP_core/Editor/GOAPEditor.uss");
        styleSheets.Add(styleSheet);
    }

    private AgentView _view;
    internal void PopulateView(AgentView goap)
    {
        this._view = goap;

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        //Create Node View
        goap.actions.ForEach(n => CreateNodeView(n));
        goap.goals.ForEach(n => CreateNodeView(n));

        //Create edges
        goap.actions.ForEach(n =>
        {
            var children = _view.GetChildren(n);
            children.ForEach(c =>
            {
                NodeView parent = FindNodeView(n);
                NodeView child = FindNodeView(c);

                Edge edge = parent.output.ConnectTo(child.input);
                AddElement(edge);
            });
        });
    }

    NodeView FindNodeView(Node node)
    {
        return GetNodeByGuid(node.guid) as NodeView;
    }
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort =>
           endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphviewchange)
    {
        if (graphviewchange.elementsToRemove != null)
        {
            graphviewchange.elementsToRemove.ForEach(elem =>
            {
                NodeView nodeView = elem as NodeView;
                if (nodeView != null)
                {
                    _view.DeleteNode(nodeView.node);
                }

                Edge edge = elem as Edge;
                if (edge != null)
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;
                    _view.RemoveChild(parentView.node, childView.node);
                }
            });
        }

        if (graphviewchange.edgesToCreate != null)
        {
            graphviewchange.edgesToCreate.ForEach(edge =>
            {
                NodeView parentView = edge.output.node as NodeView;
                NodeView childView = edge.input.node as NodeView;
                _view.AddChild(parentView.node, childView.node);
            });
        }

        return graphviewchange;
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        //base.BuildContextualMenu(evt);
        {
            var types = TypeCache.GetTypesDerivedFrom<CActionBase>();
            foreach (var VARIABLE in types)
            {
                evt.menu.AppendAction($"[{VARIABLE.BaseType.Name}] {VARIABLE.Name}", (a) => CreatNode(VARIABLE));
            }
        }
        {
            var types = TypeCache.GetTypesDerivedFrom<CGoal>();
            foreach (var VARIABLE in types)
            {
                evt.menu.AppendAction($"[{VARIABLE.BaseType.Name}] {VARIABLE.Name}", (a) => CreatNode(VARIABLE));
            }
        }
    }

    void CreatNode(System.Type type)
    {
        Node node = _view.CreateNode(type);
        CreateNodeView(node);
    }

    void CreateNodeView(Node node)
    {
        NodeView nodeView = new NodeView(node);
        nodeView.OnNodeSelected = OnNodeSelected;
        AddElement(nodeView);

    }
}