using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AI.SideKickStates
{
    public class ApproachPlayer : StateMachine<SideKick>.State
    {
        public class StateData
        {
        }

        private static ApproachPlayer _instance;

        public static ApproachPlayer Instance
        {
            get { return _instance ?? (_instance = new ApproachPlayer()); }
        }

        private ApproachPlayer()
        {
        }

        public void Enter(SideKick owner, params object[] args)
        {
        }

        public void Update(SideKick owner)
        {
            if (owner.IsPlayerInAttackRange())
            {
                owner.SwitchToAttackPlayerState();
            }
            else if (owner.IsPlayerInDetectionRange())
            {
                MoveTowardsPlayer(owner);
            }
            else
            {
                owner.SwitchToIdleState();
            }
        }

        public void Exit(SideKick owner)
        {
        }

        void MoveTowardsPlayer(SideKick owner)
        {
            Vector2 dir = LevelManager.Instance.GetPlayerGameObject().transform.position - owner.transform.position;
            dir.y = 0;

            owner.Controller.Move(dir);
        }
    }
}