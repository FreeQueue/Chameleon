using UnityEngine;
using GameFramework;
using Chameleon.Data;
using GameFramework.Event;

namespace Chameleon
{
    public class Level : IReference
    {
        public LevelData levelData
        {
            get;
            private set;
        }
        private bool Change = true;
        public int TimeSecond, TimeMillisecond;
        private int CubeNum, SphereNum, GetCubeNum, GetSphereNum;
        public void LevelRecord()
        {
            if(levelData.Cube==false)
            levelData.Cube = GetCubeNum == CubeNum;
            if(levelData.Sphere==false)
            levelData.Sphere = GetSphereNum == SphereNum;
            if(levelData.Change==false)
            levelData.Change = Change;
            if (TimeSecond > levelData.TimeSecond) return;
            levelData.TimeSecond = TimeSecond;
            if (TimeMillisecond > levelData.TimeMillisecond) return;
            levelData.TimeMillisecond = TimeMillisecond;

            if (levelData.Id > GameEntry.Setting.GetInt("LevelPass", 0))
                GameEntry.Setting.SetInt("LevelPass", levelData.Id);
            GameEntry.Setting.Save();
        }
        public static Level Create(LevelData levelData)
        {
            Level level = ReferencePool.Acquire<Level>();
            level.levelData = levelData;
            return level;
        }
        public void Start(int sphereNum, int cubeNum)
        {
            GameEntry.Event.Subscribe(ColorChangeEventArgs.EventId, OnColorChange);
            GameEntry.Event.Subscribe(CollectEventArgs.EventId, OnCollect);
            SphereNum = sphereNum;
            CubeNum = cubeNum;
            GetCubeNum = 0;
            GetSphereNum = 0;
            Change = true;
            TimeSecond = 0;
            TimeMillisecond = 0;
        }
        public void End()
        {
            GameEntry.Event.Unsubscribe(ColorChangeEventArgs.EventId, OnColorChange);
            GameEntry.Event.Unsubscribe(CollectEventArgs.EventId, OnCollect);
        }
        private void OnCollect(object sender, GameEventArgs e)
        {
            var ne = e as CollectEventArgs;
            switch (ne.Type)
            {
                case EnumEntity.Cube:
                    GetCubeNum++;
                    break;
                case EnumEntity.Sphere:
                    GetSphereNum++;
                    break;
            }
        }
        private void OnColorChange(object sender, GameEventArgs e)
        {
            Change = false;
        }
        public void Clear()
        {
            levelData = null;
        }
    }
}