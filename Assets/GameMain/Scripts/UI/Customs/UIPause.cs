using UnityEngine;
using UnityEngine.UI;

namespace Chameleon
{
    public class UIPause : UGuiFormEx
    {
        [SerializeField]
        private Button m_ResumeButton, m_BackButton, m_RestartButton;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_ResumeButton.onClick.AddListener(OnResumeButtonClick);
            m_BackButton.onClick.AddListener(OnBackButtonClick);
            m_RestartButton.onClick.AddListener(OnRestartButtonClick);
        }
        private void OnResumeButtonClick()
        {
            GameEntry.Event.Fire(this, LevelResumeEventArgs.Create());
            Close();
        }
        private void OnBackButtonClick()
        {
            GameEntry.Event.Fire(this, ChangeSceneEventArgs.Create(GameEntry.Config.GetInt("Scene.Menu")));
            Close();
        }
        private void OnRestartButtonClick()
        {
            GameEntry.Event.Fire(this, LevelRestartEventArgs.Create());
            Close();
        }
    }
}