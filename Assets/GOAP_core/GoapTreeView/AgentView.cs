using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


using Unity.GOAP.ActionBase;
using Unity.GOAP.Goal;

[CreateAssetMenu()]
public class AgentView : ScriptableObject
{
    public Node rootNode;
    public List<Node> actions = new List<Node>();
    public List<Node> goals = new List<Node>();

    public Node CreateNode(System.Type type)
    {
        Node node = ScriptableObject.CreateInstance(type) as Node;
        node.name = type.Name;
        node.guid = GUID.Generate().ToString();
        
        var actionsType = TypeCache.GetTypesDerivedFrom<CActionBase>();
        if (actionsType.Contains(type))
        {
            actions.Add(node);
        }
        var goalTypes = TypeCache.GetTypesDerivedFrom<CGoal>();
        if (goalTypes.Contains(type))
        {
            goals.Add(node);
        }
        
        AssetDatabase.AddObjectToAsset(node, this);
        AssetDatabase.SaveAssets();
        return node;
    }

    public void DeleteNode(Node node)
    {
        if (actions.Contains(node))
        {
            actions.Remove(node);
        }
        else if (goals.Contains(node))
        {
            goals.Remove(node);
        }
        AssetDatabase.RemoveObjectFromAsset(node);
        AssetDatabase.SaveAssets();
    }

    public void AddChild(Node parent, Node child)
    {
        CActionBase action = parent as CActionBase;
        if (action)
        {
            action.childiren.Add(child);
        }
    }

    public void RemoveChild(Node parent, Node child)
    {
        CActionBase action = parent as CActionBase;
        if (action)
        {
            action.childiren.Remove(child);
        }
    }

    public List<Node> GetChildren( Node parent)
    {
        CActionBase action = parent as CActionBase;
        if (action)
        {
            return action.childiren;
        }

        return null;
    }
}
