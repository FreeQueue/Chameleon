using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityGameFramework.Runtime;

namespace Chameleon
{
    public class InputSetter : ItemLogicEx
    {
        [SerializeField]
        Text m_KeyText, m_Title;
        [SerializeField]
        Button m_Button;
        private EnumInput m_EnumInput;
        private bool IsCheckKey;
        private int m_UIMaskIndex;
        private KeyCode m_Key;
        private KeyCode Key
        {
            set
            {
                m_Key = value;
                GameEntry.Input.InputSetting.ChangeKey(m_EnumInput,m_Key);
                m_KeyText.text = value.ToString();
            }
            get
            {
                return m_Key;
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
            m_EnumInput = (EnumInput)userData;
            m_Title.text = GameEntry.Localization.GetString(m_EnumInput.ToString());
            IsCheckKey = false;
            Refresh();
        }
        public void Refresh()
        {
            foreach (var item in GameEntry.Input.GetButtonKey(m_EnumInput))
            {
                if (item.ToString().Contains("Mouse")) continue;
                m_Key = item;
                break;
            }
            m_KeyText.text = Key.ToString();
        }
        private void OnButtonClick()
        {
            m_UIMaskIndex = (int)GameEntry.UI.OpenUIForm(EnumUIForm.UIMask,UIMaskParams.Create(0,0.2f));
            IsCheckKey = true;
        }

        private void OnGUI()
        {
            if (IsCheckKey)
            {
                Event e = Event.current;//获取当前事件
                if (e != null && e.isKey && e.keyCode != KeyCode.None )
                {
                    KeyCode currentKey = e.keyCode;
                    //当前按键等于 ESC 时 恢复上一次按键
                    if (currentKey == KeyCode.Escape || currentKey == m_Key)
                    {
                        m_KeyText.text = m_Key.ToString();
                    }
                    else
                    {
                        Key = currentKey;
                    }

                    IsCheckKey = false;
                    GameEntry.UI.CloseUIForm(m_UIMaskIndex);
                }
            }
        }
    }
}