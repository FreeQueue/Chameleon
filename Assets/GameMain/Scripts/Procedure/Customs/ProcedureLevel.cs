using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using GameFramework.Procedure;
using UnityEngine;
using GameFramework;

using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Chameleon
{
    public class ProcedureLevel : ProcedureBase
    {
        private ProcedureOwner procedureOwner;
        private LevelController m_LevelController;
        private bool changeScene = false;
        private int m_UIMainGameIndex;
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.Sound.PlayMusic(EnumSound.GameBGM);
            changeScene = false;
            this.procedureOwner = procedureOwner;
            GameEntry.Event.Subscribe(ChangeSceneEventArgs.EventId, OnChangeScene);
            GameEntry.Event.Subscribe(LevelRestartEventArgs.EventId, OnLevelRestart);
            GameEntry.Event.Subscribe(LevelStartEventArgs.EventId, OnLevelStart);
            GameEntry.Event.Subscribe(LevelFailEventArgs.EventId, OnLevelFail);
            GameEntry.Event.Subscribe(LevelSucceedEventArgs.EventId, OnLevelSucceed);
            GameEntry.Event.Subscribe(LevelPauseEventArgs.EventId, OnLevelPause);
            GameEntry.Event.Subscribe(LevelResumeEventArgs.EventId, OnLevelResume);
            m_UIMainGameIndex = (int)GameEntry.UI.OpenUIForm(EnumUIForm.UIMainGame);
            m_LevelController = LevelController.Create();
            m_LevelController.Enter();
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (changeScene)
            {
                ChangeState<ProcedureLoadingScene>(procedureOwner);
            }
            if (m_LevelController != null)
                m_LevelController.Update(elapseSeconds, realElapseSeconds);
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            GameEntry.Event.Unsubscribe(ChangeSceneEventArgs.EventId, OnChangeScene);
            GameEntry.Event.Unsubscribe(LevelRestartEventArgs.EventId, OnLevelRestart);
            GameEntry.Event.Unsubscribe(LevelStartEventArgs.EventId, OnLevelStart);
            GameEntry.Event.Unsubscribe(LevelFailEventArgs.EventId, OnLevelFail);
            GameEntry.Event.Unsubscribe(LevelSucceedEventArgs.EventId, OnLevelSucceed);
            GameEntry.Event.Unsubscribe(LevelPauseEventArgs.EventId, OnLevelPause);
            GameEntry.Event.Unsubscribe(LevelResumeEventArgs.EventId, OnLevelResume);

            GameEntry.Sound.StopMusic();
            m_LevelController.Quit();
            ReferencePool.Release(m_LevelController);
            m_LevelController = null;
        }
        private void OnChangeScene(object sender, GameEventArgs e)
        {
            ChangeSceneEventArgs ne = e as ChangeSceneEventArgs;
            if (ne == null)
                return;
            changeScene = true;
            GameEntry.Entity.HideAllLoadedEntities();
            procedureOwner.SetData<VarInt32>(Constant.ProcedureData.NextSceneId, ne.SceneId);
        }
        private void OnLevelStart(object sender, GameEventArgs e)
        {
            m_LevelController.Start();
        }
        private void OnLevelRestart(object sender, GameEventArgs e)
        {
            m_LevelController.End();
            m_LevelController.Start();
        }
        private void OnLevelPause(object sender, GameEventArgs e)
        {
            m_LevelController.Pause();
        }
        private void OnLevelResume(object sender, GameEventArgs e)
        {
            m_LevelController.Resume();
        }
        private void OnLevelSucceed(object sender, GameEventArgs e)
        {
            m_LevelController.Succeed();
        }
        private void OnLevelFail(object sender, GameEventArgs e)
        {
            m_LevelController.Fail();
        }
    }
}

