//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Event;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace StarForce
{
    public class ProcedureMain : ProcedureBase
    {
        private const float GameOverDelayedSeconds = 2f;

        private readonly Dictionary<GameMode, GameBase> m_Games = new();
        private GameBase m_CurrentGame = null;
        private bool m_GotoMenu = false;
        private float m_GotoMenuDelaySeconds = 0f;
        private ScoreItem m_ScoreItem = null;
        private int GameLevel = 0;

        public override bool UseNativeDialog
        {
            get
            {
                return false;
            }
        }

        public void GotoMenu()
        {
            m_GotoMenu = true;
        }

        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);

            m_Games.Add(GameMode.Survival, new SurvivalGame());
            m_Games.Add(GameMode.Enemy,new EnemyGame());
            
        }

        protected override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);

            m_Games.Clear();
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            GameEntry.Event.Subscribe(CloseUIFormCompleteEventArgs.EventId, OnCloseUIFormComplete);
            m_GotoMenu = false;
            GameMode gameMode = (GameMode)procedureOwner.GetData<VarByte>("GameMode").Value;
            m_CurrentGame = m_Games[gameMode];
            m_CurrentGame.Initialize();
            if (gameMode != GameMode.Survival)
            {
                GameLevel = procedureOwner.GetData<VarInt32>("GameLevel").Value;
                m_CurrentGame.GameLevel = GameLevel;           
            }
            
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {

            base.OnLeave(procedureOwner, isShutdown);
            if (m_CurrentGame != null)
            {
                m_CurrentGame.Shutdown();
                m_CurrentGame = null;
            }
            
            if (m_ScoreItem != null)
            {
                m_ScoreItem.Close(isShutdown);
                m_ScoreItem = null;
            }
            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            GameEntry.Event.Unsubscribe(CloseUIFormCompleteEventArgs.EventId, OnCloseUIFormComplete);


        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (m_CurrentGame != null && !m_CurrentGame.GameOver && !m_CurrentGame.MissionComplete)
            {
                m_CurrentGame.Update(elapseSeconds, realElapseSeconds);
                
                return;
            }

            if (m_CurrentGame.MissionComplete)
            {
                m_CurrentGame.MissionComplete = false;
                procedureOwner.SetData<VarInt32>("GameLevel", GameLevel + 1);

                ChangeState<ProcedureChangeLevel>(procedureOwner);
            }

            if (!m_GotoMenu)
            {
                m_GotoMenu = true;
                m_GotoMenuDelaySeconds = 0;
            }

            m_GotoMenuDelaySeconds += elapseSeconds;
            if (m_GotoMenuDelaySeconds >= GameOverDelayedSeconds)
            {
                procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Menu"));
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }

        }
        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            m_ScoreItem = (ScoreItem)ne.UIForm.Logic;
        }

        private void OnCloseUIFormComplete(object sender, GameEventArgs e)
        {
            GameEntry.UI.OpenUIForm(UIFormId.ScoreItem, this);
        }

    }
}
