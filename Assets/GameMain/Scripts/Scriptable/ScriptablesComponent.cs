using UnityEngine;
using UnityGameFramework.Runtime;
namespace Chameleon
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Scriptable")]
    public class ScriptablesComponent : GameFrameworkComponent
    {
        [SerializeField]
        public InputList DefaultInputList,MouseInputList;
        [SerializeField]
        public ColorList colorList;
    }
}