using UnityEngine;
using UnityEngine.UI;
using Chameleon.Data;

namespace Chameleon
{
    public class UISuccess : UGuiFormEx
    {
        [SerializeField]
        private Text m_LevelIndex, m_LevelDescription, m_TimeText;
        [SerializeField]
        private Image Cube, Sphere, Change;
        [SerializeField]
        private Button m_BackButton, m_RestartButton;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_RestartButton.onClick.AddListener(OnRestartButtonClick);
            m_BackButton.onClick.AddListener(OnBackButtonClick);
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            int levelIndex=GameEntry.Data.GetData<DataLevel>().CurrentLevelIndex;
            LevelData levelData = GameEntry.Data.GetData<DataLevel>().GetLevelData(levelIndex);
            m_LevelIndex.text = levelIndex.ToString();
            m_LevelDescription.text = levelData.Description;
            m_TimeText.text = $"{levelData.TimeSecond}\"{levelData.TimeMillisecond}";
            Cube.SetAlpha(levelData.Cube ? 1f : 0.5f);
            Sphere.SetAlpha(levelData.Sphere ? 1f : 0.5f);
            Change.SetAlpha(levelData.Change ? 1f : 0.5f);
        }
        private void OnRestartButtonClick()
        {
            GameEntry.Event.Fire(this, LevelStartEventArgs.Create(GameEntry.Data.GetData<DataLevel>().CurrentLevelIndex));
            Close();
        }
        private void OnBackButtonClick()
        {
            GameEntry.Event.Fire(this, ChangeSceneEventArgs.Create(GameEntry.Config.GetInt("Scene.Menu")));
            Close();
        }

    }
}