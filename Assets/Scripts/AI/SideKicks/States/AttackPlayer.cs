using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AI.SideKickStates
{
    public class AttackPlayer : StateMachine<SideKick>.State
    {
        public class StateData
        {

        }

        private static AttackPlayer _instance;

        public static AttackPlayer Instance
        {
            get { return _instance ?? (_instance = new AttackPlayer()); }
        }

        private AttackPlayer()
        {
        }

        public void Enter(SideKick owner, params object[] args)
        {

        }

        public void Update(SideKick owner)
        {
            if (owner.IsPlayerInAttackRange())
            {
                Debug.Log("Attacking Player!!!");
            }
            else
            {
                owner.SwitchToApproachPlayerState();
            } 
        }

        public void Exit(SideKick owner)
        {

        }
    }
}
