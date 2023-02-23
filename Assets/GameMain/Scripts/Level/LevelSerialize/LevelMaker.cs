using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using System.Collections.Generic;
namespace Chameleon
{
    public class LevelMaker : MonoBehaviour
    {
        [SerializeField]
        private int m_Level;
        [SerializeField]
        private Transform m_SpawnPoint, m_End, m_DeadLine;
        [ContextMenu("Save LevelData")]
        public void MakeLevel()
        {
            if (m_Level <= 0 || m_SpawnPoint == null || m_End == null || m_DeadLine == null)
            {
                Debug.LogError("Invalid Params");
                return;
            }
            var collection = GetComponentsInChildren<LevelMakerHelper>();
            LevelSceneData[] levelSceneDatas = new LevelSceneData[collection.Length];
            int i = 0;

            foreach (var item in collection)
            {
                if (item.EntityType != EnumEntity.Ground && item.EntityType != EnumEntity.Cube && item.EntityType != EnumEntity.Sphere)
                {
                    Debug.LogError("Invalid Entity Params");
                    return;
                }
                levelSceneDatas[i] = new LevelSceneData(item);
                i++;
            }
            LevelScene levelScene = new LevelScene();
            levelScene.Init();
            levelScene.Set(m_SpawnPoint.position, m_End.position, m_DeadLine.position.y, levelSceneDatas);
            if (levelScene.Save(m_Level)) Debug.Log($"Make level '{m_Level}' success");
        }

    }
}