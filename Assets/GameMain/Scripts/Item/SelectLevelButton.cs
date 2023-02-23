using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Chameleon
{
    public class SelectLevelButton : ItemLogicEx
    {
        [SerializeField]
        private Button m_Button;
        [SerializeField]
        private Text m_LevelIndex, m_LevelDescription, m_TimeText;
        [SerializeField]
        private Image Cube, Sphere, Change;
        [SerializeField]
        private GameObject Root, Start, Lock, Time;
        private int levelIndex;
        bool IsActive
        {
            get
            {
                return GameEntry.Setting.GetInt("LevelPass", 0) >= levelIndex - 1;
            }
        }
        bool IsPass
        {
            get
            {
                return GameEntry.Setting.GetInt("LevelPass", 0) >= levelIndex;
            }
        }
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_Button.onClick.AddListener(OnButtonClick);
        }
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            levelIndex = (int)userData;
            
            Root.SetActive(false);
            Time.SetActive(false);
            Start.SetActive(false);
            Lock.SetActive(false);
            Data.LevelData levelData = GameEntry.Data.GetData<Data.DataLevel>().GetLevelData(levelIndex);
            m_LevelIndex.text = levelIndex.ToString();
            m_LevelDescription.text = levelData.Description;
            if (IsActive)
            {
                if (IsPass)
                {
                    Root.SetActive(true);
                    Time.SetActive(true);
                    m_TimeText.text = $"{levelData.TimeSecond}\"{levelData.TimeMillisecond}";
                    Cube.SetAlpha(levelData.Cube ? 1f : 0.5f);
                    Sphere.SetAlpha(levelData.Sphere ? 1f : 0.5f);
                    Change.SetAlpha(levelData.Change ? 1f : 0.5f);
                }
                else
                {
                    Start.SetActive(true);
                }
            }
            else
            {
                Lock.SetActive(true);
            }
        }
        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            levelIndex = int.MaxValue;
        }
        private void OnButtonClick()
        {
            if (IsActive)
            {
                Log.Info($"Enter level {levelIndex}");
                GameEntry.Event.Fire(this, LoadLevelEventArgs.Create(levelIndex));
            }
        }
    }
}