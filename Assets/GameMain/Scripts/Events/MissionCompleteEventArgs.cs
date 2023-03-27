using GameFramework.Event;
using GameFramework;
using UnityEngine.SocialPlatforms.Impl;

namespace StarForce
{
    /// <summary>
    /// 敌人被击败事件。
    /// </summary>
    public class MissionCompleteEventArgs : GameEventArgs
    {
        /// <summary>
        /// 敌人被击败事件编号。
        /// </summary>
        public static readonly int EventId = typeof(MissionCompleteEventArgs).GetHashCode();

        /// <summary>
        /// 初始化敌人被击败事件的新实例。
        /// </summary>
        public MissionCompleteEventArgs()
        {
            Complete = true;
        }

        /// <summary>
        /// 获取任务完成事件编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        /// <summary>
        /// 获取任务完成状态。
        /// </summary>
        public bool Complete
        {
            get;
            private set;
        }

        /// <summary>
        /// 创建敌人被击败事件。
        /// </summary>
        /// <param name="e">内部事件。</param>
        /// <returns>创建的敌人被击败事件。</returns>
        public static MissionCompleteEventArgs Create()
        {
            // 使用引用池技术，避免频繁内存分配
            MissionCompleteEventArgs e = ReferencePool.Acquire<MissionCompleteEventArgs>();
            return e;
        }

        /// <summary>
        /// 清理敌人被击败事件。
        /// </summary>
        public override void Clear()
        {
            // 使用引用池技术，注意清理事件实例
            Complete = false;
        }
    }
}