using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Event;
using GameFramework;

namespace Chameleon
{
    public class LevelFailEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(LevelFailEventArgs).GetHashCode();

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

        public static LevelFailEventArgs Create(object userData = null)
        {
            LevelFailEventArgs levelFailEventArgs = ReferencePool.Acquire<LevelFailEventArgs>();
            levelFailEventArgs.UserData = userData;
            return levelFailEventArgs;
        }

        public override void Clear()
        {
        }
    }

}

