using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using GameFramework.Localization;
using UnityGameFramework.Runtime;
using Chameleon.Data;
namespace Chameleon
{
    class UIMainGame : UGuiFormEx
    {
        [SerializeField]
        private Button m_PauseButton;
        [SerializeField]
        private Text m_Time;
        private DataLevel m_DataLevel;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_PauseButton.onClick.AddListener(OnPauseButtonClick);
            m_DataLevel= GameEntry.Data.GetData<DataLevel>();
        }
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            m_Time.text = string.Format("{0:00}\"{1:00}",m_DataLevel.CurrentLevel.TimeSecond,m_DataLevel.CurrentLevel.TimeMillisecond/10);
        }
        private void OnPauseButtonClick(){
            GameEntry.Event.Fire(this, LevelPauseEventArgs.Create());
        }
    }
}