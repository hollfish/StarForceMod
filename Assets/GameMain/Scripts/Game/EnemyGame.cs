//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using GameFramework.DataTable;
using UnityEngine;
using UnityEngine.UIElements;

namespace StarForce
{
    public class EnemyGame : GameBase
    {
        private float m_ElapseSeconds = 0f;

        public override GameMode GameMode
        {
            get
            {
                return GameMode.Enemy;
            }
        }

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            base.Update(elapseSeconds, realElapseSeconds);

            m_ElapseSeconds += elapseSeconds;
            if (m_ElapseSeconds >= 1f)
            {
                m_ElapseSeconds = 0f;
                IDataTable<DRAircraft> dtAsteroid = GameEntry.DataTable.GetDataTable<DRAircraft>();
                float randomPositionX = SceneBackground.EnemySpawnBoundary.bounds.min.x + SceneBackground.EnemySpawnBoundary.bounds.size.x * (float)Utility.Random.GetRandomDouble();
                float randomPositionZ = SceneBackground.EnemySpawnBoundary.bounds.min.z + SceneBackground.EnemySpawnBoundary.bounds.size.z * (float)Utility.Random.GetRandomDouble();
                GameEntry.Entity.ShowEnemy(new EnemyAircraftData(GameEntry.Entity.GenerateSerialId(), 10001)
                {
                    Position = new Vector3(randomPositionX, 0f, randomPositionZ),
                    Rotation = new Quaternion(0,-180,0,0),
                    Name = "enemy"
                });
            }
        }
    }
}
