using GameFramework.Event;
using GameFramework;
using UnityEngine.SocialPlatforms.Impl;

namespace StarForce
{
    /// <summary>
    /// 敌人被击败事件。
    /// </summary>
    public class DefeatedEnemyEventArgs : GameEventArgs
    {
        /// <summary>
        /// 敌人被击败事件编号。
        /// </summary>
        public static readonly int EventId = typeof(DefeatedEnemyEventArgs).GetHashCode();

        /// <summary>
        /// 初始化敌人被击败事件的新实例。
        /// </summary>
        public DefeatedEnemyEventArgs()
        {
            Score = 0;
        }

        /// <summary>
        /// 获取敌人被击败事件编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        /// <summary>
        /// 获取得分。
        /// </summary>
        public int Score
        {
            get;
            private set;
        }

        /// <summary>
        /// 创建敌人被击败事件。
        /// </summary>
        /// <param name="e">内部事件。</param>
        /// <returns>创建的敌人被击败事件。</returns>
        public static DefeatedEnemyEventArgs Create(int score)
        {
            // 使用引用池技术，避免频繁内存分配
            DefeatedEnemyEventArgs e = ReferencePool.Acquire<DefeatedEnemyEventArgs>();
            e.Score = score;
            return e;
        }

        /// <summary>
        /// 清理敌人被击败事件。
        /// </summary>
        public override void Clear()
        {
            // 使用引用池技术，注意清理事件实例
            Score = 0;
        }
    }
}