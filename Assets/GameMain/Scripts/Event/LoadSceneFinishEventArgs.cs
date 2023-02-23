using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Event;
using GameFramework;

namespace Chameleon
{
    public class LoadSceneFinishEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(LoadSceneFinishEventArgs).GetHashCode();

        public LoadSceneFinishEventArgs()
        {
            LevelId = -1;
        }

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public int LevelId
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public static LoadSceneFinishEventArgs Create(int levelId, object userData = null)
        {
            LoadSceneFinishEventArgs loadSceneFinishEventArgs = ReferencePool.Acquire<LoadSceneFinishEventArgs>();
            loadSceneFinishEventArgs.LevelId = levelId;
            loadSceneFinishEventArgs.UserData = userData;
            return loadSceneFinishEventArgs;
        }

        public override void Clear()
        {
            LevelId = -1;
        }
    }

}

