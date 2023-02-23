using UnityEngine;
using System.Collections.Generic;
using GameFramework.Event;
using System.IO;
using System.Text;
using UnityGameFramework.Runtime;
using GameFramework;
using System;

namespace Chameleon
{
    public class LevelScene : IReference
    {
        private LevelSceneSerializer m_Serializer = null;
        private List<LevelSceneData> m_LevelSceneDatas;
        public Vector2 SpawnPoint, End;
        public float DeadLine;
        public LevelSceneData[] LevelSceneDatas
        {
            get
            {
                return m_LevelSceneDatas.ToArray();
            }
        }
        private int levelIndex;
        private string m_FileName = Constant.FilePath.LevelPath;
        /// <summary>
        /// 获取实体信息文件路径。
        /// </summary>
        public virtual string FilePath
        {
            get
            {
                return Utility.Path.GetRegularPath(Path.Combine("Assets/GameMain/LevelData", Utility.Text.Format(m_FileName, levelIndex)));
            }
        }
        public virtual string PackageFilePath
        {
            get
            {
                return Utility.Path.GetRegularPath(Path.Combine("LevelData", Utility.Text.Format(m_FileName, levelIndex)));
            }
        }
        public static LevelScene Create()
        {
            LevelScene levelScene = ReferencePool.Acquire<LevelScene>();
            levelScene.Init();
            return levelScene;
        }
        public void Init()
        {
            m_Serializer = new LevelSceneSerializer();
            m_LevelSceneDatas = new List<LevelSceneData>();
            m_Serializer.RegisterSerializeCallback(0, SerializeSceneEntityCallback);
            m_Serializer.RegisterDeserializeCallback(0, DeserializeSceneEntityCallback);
        }
        public void Clear()
        {
        }
        public void Set(Vector2 spawnPoint, Vector2 end, float deadLine, LevelSceneData[] levelSceneDatas)
        {
            SpawnPoint = spawnPoint;
            End = end;
            DeadLine = deadLine;
            m_LevelSceneDatas.Clear();
            m_LevelSceneDatas.AddRange(levelSceneDatas);
        }
        /// <summary>
        /// 加载实体信息。
        /// </summary>
        /// <returns>是否加载实体信息成功。</returns>
        public bool Load(int level)
        {
            levelIndex = level;
            try
            {
                if (!File.Exists(FilePath))
                {
                    return true;
                }
                using (FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                {
                    m_Serializer.Deserialize(fileStream);
                    return true;
                }
            }
            catch (Exception exception)
            {
                Log.Warning($"Load SceneEntity failure with exception '{exception.ToString()}'.");
                return false;
            }
        }

        /// <summary>
        /// 保存实体信息。
        /// </summary>
        /// <returns>是否保存实体信息成功。</returns>
        public bool Save(int level)
        {
            levelIndex = level;
            try
            {
                using (FileStream fileStream = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
                {
                    return m_Serializer.Serialize(fileStream, this);
                }
            }
            catch (Exception exception)
            {
                Log.Warning($"Save SceneEntity failure with exception '{exception.ToString()}'.");
                return false;
            }
        }
        public bool RemoveFile(int level)
        {
            levelIndex = level;
            try
            {
                if (!File.Exists(FilePath))
                {
                    return true;
                }
                File.Delete(FilePath);
                return true;
            }
            catch (Exception exception)
            {
                Log.Warning($"Delete SceneEntity failure with exception '{exception.ToString()}'.");
                return false;
            }
        }
        private bool SerializeSceneEntityCallback(Stream stream, LevelScene levelScene)
        {
            Serialize(stream);
            return true;
        }

        private LevelScene DeserializeSceneEntityCallback(Stream stream)
        {
            Deserialize(stream);
            return this;
        }
        /// <summary>
        /// 序列化数据。
        /// </summary>
        /// <param name="stream">目标流。</param>
        public void Serialize(Stream stream)
        {
            using (BinaryWriter binaryWriter = new BinaryWriter(stream, Encoding.UTF8))
            {
                binaryWriter.Write(SpawnPoint.x);
                binaryWriter.Write(SpawnPoint.y);
                binaryWriter.Write(End.x);
                binaryWriter.Write(End.y);
                binaryWriter.Write(DeadLine);
                binaryWriter.Write7BitEncodedInt32(m_LevelSceneDatas.Count);
                foreach (var entityData in m_LevelSceneDatas)
                {
                    binaryWriter.Write7BitEncodedInt32(((int)entityData.EntityType));
                    binaryWriter.Write(entityData.Position.x);
                    binaryWriter.Write(entityData.Position.y);
                    if (entityData.EntityType == EnumEntity.Ground)
                    {
                        binaryWriter.Write(entityData.Rotation);
                        binaryWriter.Write(entityData.Length);
                        binaryWriter.Write7BitEncodedInt32((int)entityData.Color);
                    }
                }
            }
        }

        /// <summary>
        /// 反序列化数据。
        /// </summary>
        /// <param name="stream">指定流。</param>
        public void Deserialize(Stream stream)
        {
            m_LevelSceneDatas.Clear();
            using (BinaryReader binaryReader = new BinaryReader(stream, Encoding.UTF8))
            {
                SpawnPoint = binaryReader.ReadVector2();
                End = binaryReader.ReadVector2();
                DeadLine = binaryReader.ReadSingle();
                int entityCount = binaryReader.Read7BitEncodedInt32();
                for (int i = 0; i < entityCount; i++)
                {
                    LevelSceneData LevelSceneData = new LevelSceneData();
                    LevelSceneData.EntityType = (EnumEntity)binaryReader.Read7BitEncodedInt32();
                    LevelSceneData.Position = binaryReader.ReadVector2();
                    if (LevelSceneData.EntityType == EnumEntity.Ground)
                    {
                        LevelSceneData.Rotation = binaryReader.ReadSingle();
                        LevelSceneData.Length = binaryReader.ReadSingle();
                        LevelSceneData.Color = (EnumColor)binaryReader.Read7BitEncodedInt32();
                    }
                    m_LevelSceneDatas.Add(LevelSceneData);
                }
            }
        }
    }
}