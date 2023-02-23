using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Data;
using GameFramework.DataTable;
using UnityGameFramework.Runtime;
using GameFramework.Event;
using GameFramework;

namespace Chameleon.Data
{
    public sealed class DataLevel : DataBase
    {
        private IDataTable<DRLevel> dtLevel;
        private Dictionary<int, LevelData> dicLevelData;
        public int MaxLevel{
            get;
            private set;
        }=0;
        public int CurrentLevelIndex{
            get;
            private set;
        }
        public Level CurrentLevel{
            get;
            private set;
        }
        protected override void OnPreload()
        {
            LoadDataTable("Level");
        }

        protected override void OnLoad()
        {
            dtLevel = GameEntry.DataTable.GetDataTable<DRLevel>();
            if (dtLevel == null)
                throw new System.Exception("Can not get data table Level");
            dicLevelData = new Dictionary<int, LevelData>();
            DRLevel[] dRLevels = dtLevel.GetAllDataRows();
            foreach (var dRLevel in dRLevels)
            {
                SceneData sceneData = GameEntry.Data.GetData<DataScene>().GetSceneData(dRLevel.SceneId);
                LevelData levelData = new LevelData(dRLevel);
                dicLevelData.Add(dRLevel.Id, levelData);
                if (dRLevel.Id > MaxLevel)
                    MaxLevel = dRLevel.Id;
            }
        }
        public LevelData GetLevelData(int id)
        {
            if (dicLevelData.ContainsKey(id))
            {
                return dicLevelData[id];
            }

            return null;
        }
        public LevelData[] GetAllLevelData()
        {
            int index = 0;
            LevelData[] results = new LevelData[dicLevelData.Count];
            foreach (var levelData in dicLevelData.Values)
            {
                results[index++] = levelData;
            }

            return results;
        }
        public void ClearRecord()
        {
            foreach (var item in dicLevelData.Values)
            {
                item.Change = false;
                item.Cube = false;
                item.Sphere = false;
                item.TimeSecond = 999;
                item.TimeMillisecond = 999;
            }
            GameEntry.Setting.Save();
        }
        public void LoadLevel(int levelIndex){
            CurrentLevelIndex = levelIndex;
            CurrentLevel = Level.Create(GetLevelData(levelIndex));
        }
        protected override void OnShutdown()
        {
        }
    }

}