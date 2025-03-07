﻿using System;
using BehaviourTree;

namespace BehaviourTree {
    namespace BehaviourWhisp {

        public class HasFledRecently : InstanceBoundCondition<Whisp> {

            public HasFledRecently(Whisp target) : base(target) {
            }

            protected override bool MyCondition() {
                return MainController.Instance.GameTime - target.LastFleeTime < 5;
            }
        }
    }
}