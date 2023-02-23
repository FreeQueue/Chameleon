using UnityEngine;
using System;
namespace Chameleon
{
    [Serializable]
    public struct InputPair
    {
        [SerializeField]
        public EnumInput enumInput;
        [SerializeField]
        public KeyCode keyCode;
        public InputPair(EnumInput enumInput,KeyCode keyCode)
        {
            this.enumInput = enumInput;
            this.keyCode = keyCode;
        }
    }
}