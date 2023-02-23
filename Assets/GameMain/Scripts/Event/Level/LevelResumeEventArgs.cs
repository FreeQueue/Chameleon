using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Event;
using GameFramework;

namespace Chameleon
{
    public class LevelResumeEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(LevelResumeEventArgs).GetHashCode();

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

        public static LevelResumeEventArgs Create(object userData = null)
        {
            LevelResumeEventArgs levelResumeEventArgs = ReferencePool.Acquire<LevelResumeEventArgs>();
            levelResumeEventArgs.UserData = userData;
            return levelResumeEventArgs;
        }

        public override void Clear()
        {
        }
    }

}

