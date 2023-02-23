using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Chameleon
{
    public class UIInput : UGuiFormEx
    {
        [SerializeField]
        private Button m_ResetButton, m_BackButton;
        [SerializeField]
        private Toggle m_MouseToggle;
        [SerializeField]
        private Transform m_Root;
        private List<InputSetter> m_InputSetters;
        private InputSetting inputSetting;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_InputSetters = new List<InputSetter>();
            m_ResetButton.onClick.AddListener(OnResetButtonClick);
            m_BackButton.onClick.AddListener(OnBackButtonClick);
            m_MouseToggle.onValueChanged.AddListener(OnMouseToggleValueChange);
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            inputSetting = GameEntry.Input.InputSetting;
            inputSetting.Read();
            ShowItem<InputSetter>(EnumItem.InputSetter, (item) =>
            {
                item.transform.SetParent(m_Root);
                m_InputSetters.Add(item.Logic as InputSetter);
            }, EnumInput.Jump);
            ShowItem<InputSetter>(EnumItem.InputSetter, (item) =>
            {
                item.transform.SetParent(m_Root);
                m_InputSetters.Add(item.Logic as InputSetter);
                Refresh();
            }, EnumInput.Change);
        }
        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            inputSetting.Save();
            GameEntry.Input.RegisterKeys();
            m_InputSetters.Clear();
        }
        private void Refresh(){
            m_MouseToggle.isOn = inputSetting.IsMouseControl;
            foreach (var item in m_InputSetters)
            {
                item.Refresh();
            }
        }
        private void OnResetButtonClick()
        {
            inputSetting.Reset();
            GameEntry.Input.RegisterKeys();
            Refresh();
        }
        private void OnBackButtonClick()
        {
            Close();
        }
        private void OnMouseToggleValueChange(bool value)
        {
            inputSetting.IsMouseControl = value;
        }
    }
}