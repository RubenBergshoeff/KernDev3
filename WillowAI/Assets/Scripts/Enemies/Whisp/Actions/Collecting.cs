﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhispActions {
    public class Collecting : ActionNode<Whisp> {

        public bool IsCollectingFragment { get; private set; }
        private bool isWithinFragmentRange = false;
        private float timeWaiting;
        private Fragment targetFragment;

        public Collecting(Whisp target) : base(target) {
        }

        public override NodeStates MyAction(Whisp target, float deltaTime) {
            if (IsCollectingFragment == false) {
                List<Fragment> fragmentsInRange = target.FragmentController.GetFragmentsInRange(target.Position, target.FragmentViewRange);
                if (fragmentsInRange.Count == 0) {
                    return NodeStates.FAILURE;
                }
                else {
                    StartCollecting(fragmentsInRange[UnityEngine.Random.Range(0, fragmentsInRange.Count)]);
                    return NodeStates.SUCCESS;
                }
            }

            if (isWithinFragmentRange) {
                timeWaiting += deltaTime;
                if (timeWaiting >= target.FragmentPickupTime) {
                    IsCollectingFragment = false;
                    isWithinFragmentRange = false;
                    timeWaiting = 0;
                }
            }
            return NodeStates.RUNNING;
        }

        private void StartCollecting(Fragment fragment) {
            targetFragment = fragment;
            isWithinFragmentRange = false;
            timeWaiting = 0;
            target.PathFindingAgent.MoveTowards(fragment.transform.position);
            target.PathFindingAgent.OnDestinationReachedAction += OnDestinationReached;
            IsCollectingFragment = true;
        }

        private void OnDestinationReached() {
            target.PathFindingAgent.OnDestinationReachedAction -= OnDestinationReached;
            isWithinFragmentRange = true;
            targetFragment.Pickup();
            targetFragment = null;
        }

        public override void CancelNode() {
            if (IsCollectingFragment && isWithinFragmentRange == false) {
                target.PathFindingAgent.OnDestinationReachedAction -= OnDestinationReached;
            }
            IsCollectingFragment = false;
            isWithinFragmentRange = false;
        }
    }
}