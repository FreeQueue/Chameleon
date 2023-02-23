using UnityEngine;
using System.Collections.Generic;

namespace Chameleon
{
    [CreateAssetMenu(fileName = "New InputList", menuName = "Scriptable/InputList", order = 1)]
    public class InputList : ScriptableObject
    {
        [SerializeField]
        private List<InputPair> m_Keys;
        public InputPair[] GetKeys(){
            return m_Keys.ToArray();
        }
    }
}