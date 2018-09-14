using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AI.SideKickStates
{
    public class Patrol : StateMachine<SideKick>.State
    {
        public class StateData
        {
            public Vector3 CurDir;
        }

        private static Patrol _instance;

        public static Patrol Instance
        {
            get { return _instance ?? (_instance = new Patrol()); }
        }

        private Patrol()
        {
        }

        public void Enter(SideKick owner, params object[] args)
        {
            Vector3 startDir = (Vector3) args[0];
            owner.PatrolStateData = new StateData {CurDir = startDir};
        }

        public void Update(SideKick owner)
        {
            owner.Controller.Move(owner.PatrolStateData.CurDir);
        }

        public void Exit(SideKick owner)
        {

        }
    }
}
