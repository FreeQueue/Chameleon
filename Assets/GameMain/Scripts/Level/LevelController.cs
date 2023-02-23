using UnityEngine;
using System.Diagnostics;
using GameFramework.DataNode;
using System.Collections;
using UnityGameFramework.Runtime;
using GameFramework.Event;
using GameFramework;
using Chameleon.Data;

namespace Chameleon
{
    public class LevelController : IPause, IReference
    {
        private Stopwatch m_Stopwatch;
        private bool IsPause;
        private EntityLoader m_EntityLoader;
        private DataLevel m_DataLevel;
        private PlayerController m_PlayerController;
        private float EndDis, DeadLine;
        public static LevelController Create()
        {
            LevelController levelController = ReferencePool.Acquire<LevelController>();
            levelController.m_EntityLoader = EntityLoader.Create(levelController);
            levelController.m_Stopwatch = new Stopwatch();
            levelController.m_DataLevel = GameEntry.Data.GetData<DataLevel>();
            levelController.IsPause = true;
            return levelController;
        }
        public void InitLevel()
        {
            LevelScene levelScene = LevelScene.Create();
            levelScene.Load(m_DataLevel.CurrentLevelIndex);
            m_EntityLoader.HideAllEntity();
            m_EntityLoader.ShowEntity<Player>(EnumEntity.Player, (entity) =>
            {
                m_PlayerController = PlayerController.Create(entity.Logic as Player);
                m_PlayerController.Enter();
            }, EntityData.Create(levelScene.SpawnPoint));
            m_EntityLoader.ShowEntity<End>(EnumEntity.End, null, EntityData.Create(levelScene.End));
            EndDis = levelScene.End.x;
            DeadLine = levelScene.DeadLine;
            int sphereCounter = 0, cubeCounter = 0;
            foreach (var item in levelScene.LevelSceneDatas)
            {
                switch (item.EntityType)
                {
                    case EnumEntity.Ground:
                        m_EntityLoader.ShowEntity<Ground>(item.EntityType, (entity) =>
                        {
                            entity.transform.SetLocalScaleX(item.Length);
                            Ground ground = entity.Logic as Ground;
                            ground.SetColor(item.Color);
                        }, EntityData.Create(item.Position, Quaternion.Euler(0, 0, item.Rotation)));
                        break;
                    case EnumEntity.Sphere:
                        sphereCounter++;
                        m_EntityLoader.ShowEntity<Collection>(item.EntityType, null, EntityData.Create(item.Position));
                        break;
                    case EnumEntity.Cube:
                        cubeCounter++;
                        m_EntityLoader.ShowEntity<Collection>(item.EntityType, null, EntityData.Create(item.Position));
                        break;
                }
            }
            m_DataLevel.CurrentLevel.Start(sphereCounter, cubeCounter);
        }
        public void Enter()
        {
            GameEntry.Event.Subscribe(CollectEventArgs.EventId, OnCollect);
            Start();
        }

        public void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (IsPause) return;
            m_DataLevel.CurrentLevel.TimeSecond = m_Stopwatch.Elapsed.Seconds;
            m_DataLevel.CurrentLevel.TimeMillisecond = m_Stopwatch.Elapsed.Milliseconds;
            m_PlayerController.Update(elapseSeconds, realElapseSeconds);
            if (m_PlayerController.Player.transform.position.x >= EndDis)
                GameEntry.Event.Fire(this, LevelSucceedEventArgs.Create());
            if (m_PlayerController.Player.transform.position.y <DeadLine)
                GameEntry.Event.Fire(this, LevelFailEventArgs.Create());
        }
        public void Quit()
        {
            GameEntry.Event.Unsubscribe(CollectEventArgs.EventId, OnCollect);
        }
        private void OnCollect(object sender, GameEventArgs e)
        {
            Log.Debug("collect");
            m_EntityLoader.HideEntity((sender as Collection).Id);
        }
        public void Start()
        {
            GameEntry.UI.OpenUIForm(EnumUIForm.UIMask, UIMaskParams.Create(1, 1, OnStartCallBack));
        }
        private void OnStartCallBack()
        {
            IsPause = false;
            m_Stopwatch.Restart();
            InitLevel();
        }
        public void Pause()
        {
            IsPause = true;
            m_PlayerController.Pause();
            m_Stopwatch.Stop();
            GameEntry.UI.OpenUIForm(EnumUIForm.UIPause);
        }
        public void Resume()
        {
            IsPause = false;
            m_PlayerController.Resume();
            m_Stopwatch.Start();
        }
        public void Succeed()
        {
            m_DataLevel.CurrentLevel.LevelRecord();
            GameEntry.UI.OpenUIForm(EnumUIForm.UISuccess);
            End();

        }
        public void Fail()
        {
            End();
            Start();
        }
        public void End()
        {
            IsPause = true;
            m_DataLevel.CurrentLevel.End();
            m_Stopwatch.Stop();
            m_PlayerController.Quit();
            m_PlayerController.Clear();
            m_EntityLoader.HideAllEntity();
        }
        public void Clear()
        {

        }
    }
}