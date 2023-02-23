using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Event;
using GameFramework;

namespace Chameleon
{
    public class LevelStartEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(LevelStartEventArgs).GetHashCode();

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public object UserData
        {
            get;
            private set;
        }

        public static LevelStartEventArgs Create(object userData = null)
        {
            LevelStartEventArgs levelStartEventArgs = ReferencePool.Acquire<LevelStartEventArgs>();
            levelStartEventArgs.UserData = userData;
            return levelStartEventArgs;
        }

        public override void Clear()
        {
        }
    }

}

