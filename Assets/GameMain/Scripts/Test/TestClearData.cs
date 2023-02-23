using UnityEngine;

namespace Chameleon.Test
{
    public class TestClearData : MonoBehaviour
    {
        [ContextMenu("Clear Data(In Run)")]
        public void ClearData()
        {
            GameEntry.Data.GetData<Data.DataLevel>().ClearRecord();
            GameEntry.Setting.SetInt("LevelPass", 0);
        }
    }
}