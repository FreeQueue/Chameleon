using UnityEngine;
using System.Collections.Generic;

namespace Chameleon
{
    [CreateAssetMenu(fileName = "ColorList", menuName = "Scriptable/ColorList", order = 0)]
    public class ColorList : ScriptableObject
    {
        [SerializeField]
        private List<Color> m_Colors;
        public Color GetColor(int id){
            return m_Colors[id];
        }
    }
}