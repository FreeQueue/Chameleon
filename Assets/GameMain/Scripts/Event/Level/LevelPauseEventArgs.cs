using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Event;
using GameFramework;

namespace Chameleon
{
    public class LevelPauseEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(LevelPauseEventArgs).GetHashCode();

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

        public static LevelPauseEventArgs Create(object userData = null)
        {
            LevelPauseEventArgs levelPauseEventArgs = ReferencePool.Acquire<LevelPauseEventArgs>();
            levelPauseEventArgs.UserData = userData;
            return levelPauseEventArgs;
        }

        public override void Clear()
        {
        }
    }

}

