//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class MissionCompleteItem : UGuiForm
    {
        [SerializeField]
        private Text m_Text = null;

        
        private float m_ElapseSeconds = 0f;
        private ProcedureChangeLevel m_ProcedureChangeLevel = null;

#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);
            m_Text.text = "congratulations";
            
            m_ProcedureChangeLevel = (ProcedureChangeLevel)userData;
            if (m_ProcedureChangeLevel == null)
            {
                Log.Warning("m_ProcedureChangeLevel is invalid when open MenuForm.");
                return;
            }
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnClose(bool isShutdown, object userData)
#else
        protected internal override void OnClose(bool isShutdown, object userData)
#endif
        {
            base.OnClose(isShutdown, userData);
            m_ProcedureChangeLevel = null;
            m_ElapseSeconds = 0f;
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            m_ElapseSeconds += elapseSeconds;
            if(m_ElapseSeconds >= 2f)
            {
                m_ProcedureChangeLevel.SetComplete();
                return;
            }
        }
    }
}
