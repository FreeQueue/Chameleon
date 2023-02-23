using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Event;
using GameFramework;

namespace Chameleon
{
    public class LevelMakeEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(LevelMakeEventArgs).GetHashCode();

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

        public static LevelMakeEventArgs Create(object userData = null)
        {
            LevelMakeEventArgs levelMakeEventArgs = ReferencePool.Acquire<LevelMakeEventArgs>();
            levelMakeEventArgs.UserData = userData;
            return levelMakeEventArgs;
        }

        public override void Clear()
        {
        }
    }

}

