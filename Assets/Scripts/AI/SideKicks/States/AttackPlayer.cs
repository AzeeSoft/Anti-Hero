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
            public float lastShootTime = 0;
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
            if (owner.AttackStateData == null)
            {
                owner.AttackStateData = new StateData();
            }
        }

        public void Update(SideKick owner)
        {
            if (owner.IsPlayerInAttackRange())
            {
                if (Time.time - owner.AttackStateData.lastShootTime > owner.shootInterval)
                {
                    owner.ShootBullet();
                    owner.AttackStateData.lastShootTime = Time.time;
                }
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
