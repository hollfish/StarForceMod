//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using GameFramework.Event;
using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class ScoreItem : UGuiForm
    {
        [SerializeField]
        private Text m_ScoreText = null;

        private int Score;
        private ProcedureMain m_ProcedureMain;

#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);
            m_ProcedureMain = (ProcedureMain)userData;
            if (m_ProcedureMain == null)
            {
                Log.Warning("GameBase is invalid when open MenuForm.");
                return;
            }
            m_ScoreText.text = "score:0";
            GameEntry.Event.Subscribe(DefeatedEnemyEventArgs.EventId, OnEnemyDefeated);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnClose(bool isShutdown, object userData)
#else
        protected internal override void OnClose(bool isShutdown, object userData)
#endif
        {
            base.OnClose(isShutdown, userData);
            GameEntry.Event.Unsubscribe(DefeatedEnemyEventArgs.EventId, OnEnemyDefeated);
        }


        private void OnEnemyDefeated(object sender, GameEventArgs e)
        {
            DefeatedEnemyEventArgs ne = (DefeatedEnemyEventArgs)e;
            Score += ne.Score;
            m_ScoreText.text = string.Format("score:{0}", Score);
        }
    }
}
