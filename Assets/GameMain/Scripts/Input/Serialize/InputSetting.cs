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
    public class InputSetting : IReference
    {
        public bool IsMouseControl
        {
            get;
            set;
        }
        private InputSettingSerializer m_Serializer = null;
        public Dictionary<EnumInput,KeyCode> InputSettingDatas{
            get;
            private set;
        }
        private string m_FileName = Constant.FilePath.LevelPath;
        /// <summary>
        /// 获取实体信息文件路径。
        /// </summary>
        public virtual string FilePath
        {
            get
            {
                return Utility.Path.GetRegularPath(Path.Combine(Application.persistentDataPath, m_FileName));
            }
        }
        public static InputSetting Create()
        {
            InputSetting inputSetting = ReferencePool.Acquire<InputSetting>();
            inputSetting.InputSettingDatas = new Dictionary<EnumInput, KeyCode>();
            inputSetting.Init();
            return inputSetting;
        }
        public void ChangeKey(EnumInput enumInput,KeyCode keyCode)
        {
            InputSettingDatas[enumInput]=keyCode;
        }
        public void Init()
        {
            m_Serializer = InputSettingSerializer.Create();
            InputSettingDatas.Clear();
            m_Serializer.RegisterSerializeCallback(0, SerializeSceneEntityCallback);
            m_Serializer.RegisterDeserializeCallback(0, DeserializeSceneEntityCallback);
        }
        public void Clear()
        {
            m_Serializer.Clear();
            m_Serializer = null;
            InputSettingDatas = null;
        }
        public void Read()
        {
            InputSettingDatas.Clear();
            IsMouseControl=false;
            foreach (EnumInput enumInput in Enum.GetValues(typeof(EnumInput)))
            {
                foreach (var keyCode in GameEntry.Input.GetButtonKey(enumInput))
                {
                    Debug.Log($"{enumInput},{keyCode}");
                    if (keyCode.ToString().Contains("Mouse"))
                    {
                        IsMouseControl = true;
                        continue;
                    }
                    InputSettingDatas[enumInput]=keyCode;
                }
            }
        }
        public void Reset()
        {
            IsMouseControl = false;
            InputSettingDatas.Clear();
            foreach (var item in GameEntry.Scriptables.DefaultInputList.GetKeys())
            {
                InputSettingDatas[item.enumInput]=item.keyCode;
            }
        }
        /// <summary>
        /// 加载实体信息。
        /// </summary>
        /// <returns>是否加载实体信息成功。</returns>
        public bool Load()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    Reset();
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
                Log.Warning($"Load InputSetting failure with exception '{exception.ToString()}'.");
                return false;
            }
        }

        /// <summary>
        /// 保存实体信息。
        /// </summary>
        /// <returns>是否保存实体信息成功。</returns>
        public bool Save()
        {
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
        public bool RemoveFile()
        {
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
        private bool SerializeSceneEntityCallback(Stream stream, InputSetting levelScene)
        {
            Serialize(stream);
            return true;
        }

        private InputSetting DeserializeSceneEntityCallback(Stream stream)
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
                binaryWriter.Write(IsMouseControl);
                binaryWriter.Write7BitEncodedInt32(InputSettingDatas.Count);
                foreach (var item in InputSettingDatas)
                {
                    binaryWriter.Write7BitEncodedInt32((int)item.Key);
                    binaryWriter.Write7BitEncodedInt32((int)item.Value);
                }
            }
        }

        /// <summary>
        /// 反序列化数据。
        /// </summary>
        /// <param name="stream">指定流。</param>
        public void Deserialize(Stream stream)
        {
            InputSettingDatas.Clear();
            using (BinaryReader binaryReader = new BinaryReader(stream, Encoding.UTF8))
            {
                IsMouseControl = binaryReader.ReadBoolean();
                int count = binaryReader.Read7BitEncodedInt32();
                InputPair pair = new InputPair();
                for (int i = 0; i < count; i++)
                {
                    pair.enumInput = (EnumInput)binaryReader.Read7BitEncodedInt32();
                    pair.keyCode = (KeyCode)binaryReader.Read7BitEncodedInt32();
                    InputSettingDatas[pair.enumInput]=pair.keyCode;
                }
            }
        }
    }
}