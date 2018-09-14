using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI.SideKickStates
{
    public class Idle : StateMachine<SideKick>.State
    {
        public class StateData
        {

        }

        private static Idle _instance;

        public static Idle Instance
        {
            get { return _instance ?? (_instance = new Idle()); }
        }

        private Idle()
        {
        }

        public void Enter(SideKick owner, params object[] args)
        {

        }

        public void Update(SideKick owner)
        {

        }

        public void Exit(SideKick owner)
        {

        }
    }
}
