using UnityEngine;
using System;
using UnityGameFramework.Runtime;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using System.IO;
using System.Linq;
namespace Chameleon
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Input")]
    public class InputComponent : GameFrameworkComponent
    {
        private Dictionary<EnumInput, Dictionary<EnumButtonState, Action>> m_ButtonActionDic;
        private Dictionary<EnumInput, VirtualButton> m_VirtualButtonDic;
        private Action m_AnyKeyAction;
        public InputSetting InputSetting{
            get;
            private set;
        }
        protected override void Awake()
        {
            base.Awake();
            InputSetting = InputSetting.Create();
            m_VirtualButtonDic = new Dictionary<EnumInput, VirtualButton>();
            m_ButtonActionDic = new Dictionary<EnumInput, Dictionary<EnumButtonState, Action>>();
            foreach (EnumInput enumInput in Enum.GetValues(typeof(EnumInput)))
            {
                m_ButtonActionDic[enumInput] = new Dictionary<EnumButtonState, Action>();
                m_VirtualButtonDic[enumInput] = new VirtualButton();
            }
            Init();
        }
        public void RegisterKeys(){
            ClearKeys();
            foreach (var item in InputSetting.InputSettingDatas)
            {
                RegisterKey(item.Key, item.Value);
            }
            if(InputSetting.IsMouseControl)
            {
                foreach(var item in GameEntry.Scriptables.MouseInputList.GetKeys())
                {
                    RegisterKey(item.enumInput, item.keyCode);
                }
            }
        }
        public KeyCode[] GetButtonKey(EnumInput enumInput)
        {
            return m_VirtualButtonDic[enumInput].GetKeys();
        }
        public void Reset()
        {
            InputSetting.Reset();
            RegisterKeys();
        }
        public void Init()
        {
            InputSetting.Load();
            RegisterKeys();
        }
        public void ClearKeys()
        {
            foreach (var item in m_VirtualButtonDic.Values)
            {
                item.ClearKeys();
            }
        }
        public void Update()
        {
            if (m_AnyKeyAction != null && Input.anyKeyDown)
            {
                m_AnyKeyAction.Invoke();
                return;
            }
            foreach (var Dic in m_ButtonActionDic)
            {
                if (Dic.Value.ContainsKey(EnumButtonState.Down) && m_VirtualButtonDic[Dic.Key].IsButtonDown)
                {
                    Dic.Value[EnumButtonState.Down].Invoke();
                    return;
                }
                if (Dic.Value.ContainsKey(EnumButtonState.Up) && m_VirtualButtonDic[Dic.Key].IsButtonUp)
                {
                    Dic.Value[EnumButtonState.Up].Invoke();
                }
                if (Dic.Value.ContainsKey(EnumButtonState.Hover) && m_VirtualButtonDic[Dic.Key].IsButtonHover)
                {
                    Dic.Value[EnumButtonState.Hover].Invoke();
                }
            }
        }
        public void RegisterAnyKey(Action action)
        {
            m_AnyKeyAction = action;
        }
        public void UnregisterAnyKey(Action action)
        {
            m_AnyKeyAction = null;
        }
        public void RegisterAction(EnumInput enumInput, EnumButtonState enumButtonState, Action action)
        {
            m_ButtonActionDic[enumInput][enumButtonState] = action;
        }
        public void UnregisterAction(EnumInput enumInput, EnumButtonState enumButtonState)
        {
            m_ButtonActionDic[enumInput].Remove(enumButtonState);
        }
        public void RegisterKey(EnumInput enumInput, KeyCode keyCode)
        {
            m_VirtualButtonDic[enumInput].RegisterKey(keyCode);
        }
        public void UnregisterKey(EnumInput enumInput, KeyCode keyCode)
        {
            m_VirtualButtonDic[enumInput].UnregisterKey(keyCode);
        }
        class VirtualButton
        {
            HashSet<KeyCode> m_Keys = new HashSet<KeyCode>();
            public bool IsButtonHover
            {
                get
                {
                    foreach (var key in m_Keys)
                    {
                        if (Input.GetKey(key))
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
            public bool IsButtonDown
            {
                get
                {
                    foreach (var key in m_Keys)
                    {
                        if (Input.GetKeyDown(key))
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
            public bool IsButtonUp
            {
                get
                {
                    foreach (var key in m_Keys)
                    {
                        if (Input.GetKeyUp(key))
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
            public KeyCode[] GetKeys()
            {
                KeyCode[] keyCodes=new KeyCode[m_Keys.Count];
                m_Keys.CopyTo(keyCodes);
                return keyCodes;
            }
            public void RegisterKey(KeyCode keyCode)
            {
                m_Keys.Add(keyCode);
            }
            public void UnregisterKey(KeyCode keyCode)
            {
                m_Keys.Remove(keyCode);
            }
            public void ClearKeys()
            {
                m_Keys.Clear();
            }
        }
    }

}