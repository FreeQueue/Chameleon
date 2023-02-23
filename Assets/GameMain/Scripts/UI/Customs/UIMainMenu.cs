using UnityEngine;
using UnityEngine.UI;
using GameFramework.Localization;
using UnityGameFramework.Runtime;

namespace Chameleon
{
    class UIMainMenu : UGuiFormEx
    {
        [SerializeField]
        private Button m_StartButton, m_LanguageButton, m_InputButton, m_ExitButton;
        [SerializeField]
        private Toggle m_MusicToggle;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_StartButton.onClick.AddListener(OnStartButtonClick);
            m_LanguageButton.onClick.AddListener(OnLanguageButtonClick);
            m_InputButton.onClick.AddListener(OnInputButtonClick);
            m_MusicToggle.onValueChanged.AddListener(OnMusicToggleValueChanged);
            m_ExitButton.onClick.AddListener(OnExitButtonClick);
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            m_MusicToggle.isOn = GameEntry.Setting.GetBool("Music", false);
        }
        private void OnStartButtonClick()
        {
            GameEntry.UI.OpenUIForm(EnumUIForm.UISelectLevel);
        }
        private void OnMusicToggleValueChanged(bool value)
        {
            GameEntry.Setting.SetBool("Music", value);
            if (value == false)
            {
                GameEntry.Sound.SetVolume("Music", 1);
            }
            else
            {
                GameEntry.Sound.SetVolume("Music", 0);
            }
        }
        private void OnExitButtonClick()
        {
            UnityGameFramework.Runtime.GameEntry.Shutdown(ShutdownType.Quit);
        }
        private void OnInputButtonClick()
        {
            GameEntry.UI.OpenUIForm(EnumUIForm.UIInput);
        }
        private void OnLanguageButtonClick()
        {
            Language language;
            if (GameEntry.Setting.GetInt(Constant.Setting.Language) == (int)Language.ChineseSimplified)
            {
                language = Language.English;
            }
            else
            {
                language = Language.ChineseSimplified;
            }
            GameEntry.Setting.SetInt(Constant.Setting.Language, (int)language);
            GameEntry.Setting.Save();
            UnityGameFramework.Runtime.GameEntry.Shutdown(ShutdownType.Restart);
        }
    }
}