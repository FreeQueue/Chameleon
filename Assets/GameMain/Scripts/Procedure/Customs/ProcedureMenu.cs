using GameFramework.Event;
using UnityGameFramework.Runtime;
using GameFramework.Procedure;
using UnityEngine;
using Chameleon.Data;

using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Chameleon
{
    public class ProcedureMenu : ProcedureBase
    {
        private ProcedureOwner procedureOwner;
        private bool changeScene = false;

        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            this.procedureOwner = procedureOwner;
            this.changeScene = false;
            GameEntry.Event.Subscribe(LoadLevelEventArgs.EventId, OnLoadLevel);
            GameEntry.UI.OpenUIForm(EnumUIForm.UIMainMenu);
            GameEntry.Sound.PlayMusic(EnumSound.MenuBGM);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (changeScene)
            {
                ChangeState<ProcedureLoadingScene>(procedureOwner);
            }
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            GameEntry.Event.Unsubscribe(LoadLevelEventArgs.EventId, OnLoadLevel);
            GameEntry.Sound.StopMusic();
        }

        private void OnLoadLevel(object sender, GameEventArgs e)
        {
            LoadLevelEventArgs ne = (LoadLevelEventArgs)e;
            if (ne == null)
                return;
            
            GameEntry.Data.GetData<DataLevel>().LoadLevel(ne.LevelId);
            changeScene = true;
            procedureOwner.SetData<VarInt32>(Constant.ProcedureData.NextSceneId, GameEntry.Config.GetInt("Scene.Level"));
        }

    }
}

