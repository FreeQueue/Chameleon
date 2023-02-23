using UnityEngine;
using UnityEngine.UI;
using Chameleon.Data;

namespace Chameleon
{
    public class UISelectLevel : UGuiFormEx
    {
        [SerializeField]
        private Button m_BackButton;
        [SerializeField]
        private Transform m_Root;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_BackButton.onClick.AddListener(OnBackButtonClick);
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            DataLevel dataLevel = GameEntry.Data.GetData<DataLevel>();
            for (int i = 1; i <= dataLevel.MaxLevel; i++)
                ShowItem<SelectLevelButton>(EnumItem.SelectLevelButton, (item) =>
                {
                    item.transform.SetParent(m_Root);
                }, i);
        }
        private void OnBackButtonClick()
        {
            Close();
        }
    }
}