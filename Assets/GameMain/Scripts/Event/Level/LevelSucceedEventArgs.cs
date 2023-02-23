using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Event;
using GameFramework;

namespace Chameleon
{
    public class LevelSucceedEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(LevelSucceedEventArgs).GetHashCode();

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

        public static LevelSucceedEventArgs Create(object userData = null)
        {
            LevelSucceedEventArgs levelSucceedEventArgs = ReferencePool.Acquire<LevelSucceedEventArgs>();
            levelSucceedEventArgs.UserData = userData;
            return levelSucceedEventArgs;
        }

        public override void Clear()
        {
        }
    }

}

