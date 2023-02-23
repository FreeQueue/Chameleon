using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Event;
using GameFramework;

namespace Chameleon
{
    public class LoadLevelEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(LoadLevelEventArgs).GetHashCode();

        public LoadLevelEventArgs()
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

        public static LoadLevelEventArgs Create(int levelId, object userData = null)
        {
            LoadLevelEventArgs loadLevelEventArgs = ReferencePool.Acquire<LoadLevelEventArgs>();
            loadLevelEventArgs.LevelId = levelId;
            loadLevelEventArgs.UserData = userData;
            return loadLevelEventArgs;
        }

        public override void Clear()
        {
            LevelId = -1;
        }
    }

}

