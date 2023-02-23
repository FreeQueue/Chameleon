using UnityEngine;
using UnityGameFramework.Runtime;

namespace Chameleon
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        public static BuiltinDataComponent BuiltinData
        {
            get;
            private set;
        }

        public static ItemComponent Item
        {
            get;
            private set;
        }

        public static DataComponent Data
        {
            get;
            private set;
        }
        public static InputComponent Input
        {
            get;
            private set;
        }
        public static ScriptablesComponent Scriptables
        {
            get; private set;
        }
        private static void InitCustomComponents()
        {
            Scriptables = UnityGameFramework.Runtime.GameEntry.GetComponent<ScriptablesComponent>();
            BuiltinData = UnityGameFramework.Runtime.GameEntry.GetComponent<BuiltinDataComponent>();
            Item = UnityGameFramework.Runtime.GameEntry.GetComponent<ItemComponent>();
            Data = UnityGameFramework.Runtime.GameEntry.GetComponent<DataComponent>();
            Input = UnityGameFramework.Runtime.GameEntry.GetComponent<InputComponent>();
        }
    }
}
