using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor.Experimental.GraphView;
using UnityEngine;


using Unity.GOAP.ActionBase;
using Unity.GOAP.Goal;
public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public Node node;

    public Port input;
    public Port output;
    public NodeView(Node node)
    {
        this.node = node;
        this.title = node.name;
        this.viewDataKey = node.guid;
        style.left = node.position.x;
        style.top = node.position.y;

        CreateInputPorts();
        CreateOutputPorts();
    }

    private void CreateOutputPorts()
    {
        if (node is CActionBase)
        {
            input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
        }
        else if (node is CGoal)
        {
            input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
        }

        if (input != null)
        {
            input.portName = "";
            inputContainer.Add(input);
        }
    }

    private void CreateInputPorts()
    {
        if (node is CActionBase)
        {
            output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
        }
        else if (node is CGoal)
        {
            
        }
        
        if (output != null)
        {
            output.portName = "";
            outputContainer.Add(output);
        }
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        node.position.x = newPos.xMin;
        node.position.y = newPos.yMin;
    }

    public Action<NodeView> OnNodeSelected;

    public override void OnSelected()
    {
        base.OnSelected();

        if (OnNodeSelected != null)
        {
            OnNodeSelected.Invoke(this);
        }
    }
}
