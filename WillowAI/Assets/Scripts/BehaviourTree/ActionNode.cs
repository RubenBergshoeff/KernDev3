﻿using System;
using UnityEngine;
using System.Collections;

public abstract class ActionNode<T> : Node {
    protected T target;

    /* Because this node contains no logic itself, 
     * the logic must be passed in in the form of  
     * a delegate. As the signature states, the action 
     * needs to return a NodeStates enum */
    public ActionNode(T target) {
        this.target = target;
    }

    /* Evaluates the node using the passed in delegate and  
     * reports the resulting state as appropriate */
    public override NodeStates Evaluate(float deltaTime) {
        switch (MyAction(target, deltaTime)) {
            case NodeStates.SUCCESS:
                m_nodeState = NodeStates.SUCCESS;
                return m_nodeState;
            case NodeStates.FAILURE:
                m_nodeState = NodeStates.FAILURE;
                return m_nodeState;
            case NodeStates.RUNNING:
                m_nodeState = NodeStates.RUNNING;
                return m_nodeState;
            default:
                m_nodeState = NodeStates.FAILURE;
                return m_nodeState;
        }
    }

    public abstract NodeStates MyAction(T target, float deltaTime);
}