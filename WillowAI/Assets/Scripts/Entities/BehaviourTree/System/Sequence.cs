﻿namespace BehaviourTree {

    public class Sequence : Composite {

        private int currentSubNode = 0;

        public Sequence(params Node[] nodes) : base(nodes) {

        }

        protected override void OnInitialize() {
            currentSubNode = 0;
        }

        public override NodeStates Evaluate(float deltaTime) {
            while (true) {
                // initialize subnode if it has not yet been initialized
                if (nodes[currentSubNode].CurrentNodeState == NodeStates.INVALID) {
                    nodes[currentSubNode].Initialize();
                }
                NodeStates currentSubNodeState = nodes[currentSubNode].Evaluate(deltaTime);

                switch (currentSubNodeState) {
                    case NodeStates.RUNNING:
                        currentNodeState = currentSubNodeState;
                        return currentNodeState;
                    case NodeStates.FAILURE:
                        OnTerminate();
                        return NodeStates.FAILURE;
                    case NodeStates.SUCCESS:
                        // if ran through all subnodes in sequence, return succes
                        if (currentSubNode + 1 == nodes.Length) {
                            OnTerminate();
                            return NodeStates.SUCCESS;
                        } else {
                            nodes[currentSubNode].Terminate();
                            currentSubNode++;
                        }
                        break;
                    case NodeStates.INVALID:
                        throw new System.Exception("This should never happen!");
                }
            }
        }

        protected override void OnTerminate() {
            if (nodes[currentSubNode].CurrentNodeState != NodeStates.INVALID) {
                nodes[currentSubNode].Terminate();
            }
        }
    }
}