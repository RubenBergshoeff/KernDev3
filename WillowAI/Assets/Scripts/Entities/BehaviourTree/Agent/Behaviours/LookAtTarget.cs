﻿using System;
using UnityEngine;
using BehaviourTree;

namespace BehaviourTree {
    namespace BehaviourAgent {

        public class LookAtTarget : InstanceBoundBehaviour<IBehaviourAgent> {

            public LookAtTarget(IBehaviourAgent target) : base(target) {
            }

            protected override void OnInitialize() {
                base.OnInitialize();
                currentNodeState = NodeStates.RUNNING;
            }

            protected override NodeStates MyAction(float deltaTime) {
                Vector3 lookDirection = target.TargetLookPosition - target.Position;
                lookDirection.Normalize();
                if (Vector3.Angle(target.Transform.forward, lookDirection) < 5) {
                    currentNodeState = NodeStates.SUCCESS;
                    return currentNodeState;
                }
                target.Transform.rotation = Quaternion.Slerp(target.Transform.rotation, Quaternion.LookRotation(lookDirection, Vector3.up), target.RotationSpeed * deltaTime);
                return currentNodeState;
            }
        }
    }
}