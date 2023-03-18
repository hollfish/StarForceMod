﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class Enemy : Aircraft
    {
        [SerializeField]
        private EnemyAircraftData m_EnemyAircraftData = null;


#if UNITY_2017_3_OR_NEWER
        protected override void OnInit(object userData)
#else
        protected internal override void OnInit(object userData)
#endif
        {
            base.OnInit(userData);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnShow(object userData)
#else
        protected internal override void OnShow(object userData)
#endif
        {
            base.OnShow(userData);

            m_EnemyAircraftData = userData as EnemyAircraftData;
            if (m_EnemyAircraftData == null)
            {
                Log.Error("Enemy aircraft data is invalid.");
                return;
            }
           
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#else
        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#endif
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            if(elapseSeconds == 1f)
            {
                Log.Error("gogogo");
            }

            for (int i = 0; i < m_Weapons.Count; i++)
            {
                m_Weapons[i].EnemyAttack();
            }

            CachedTransform.Translate(Vector3.back * m_EnemyAircraftData.Speed * elapseSeconds, Space.World);
        
        }
    }
}
