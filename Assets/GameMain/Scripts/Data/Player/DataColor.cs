using UnityEngine;
using GameFramework.Resource;
using UnityGameFramework.Runtime;

namespace Chameleon.Data
{
    public class DataColor : DataBase
    {
        private ColorList m_ColorList;
        protected override void OnInit()
        {
            m_ColorList = GameEntry.Scriptables.colorList;
        }
        public Color GetColor(int id)
        {
            return m_ColorList.GetColor(id);
        }
    }
}