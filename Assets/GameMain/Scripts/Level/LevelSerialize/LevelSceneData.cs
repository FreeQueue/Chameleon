using UnityEngine;

namespace Chameleon
{
    public class LevelSceneData
    {
        public EnumEntity EntityType;
        public Vector2 Position;
        public EnumColor Color;
        public float Rotation, Length;
        public LevelSceneData(LevelMakerHelper levelMakerHelper)
        {
            EntityType = levelMakerHelper.EntityType;
            Position = levelMakerHelper.transform.position;
            if (EntityType == EnumEntity.Ground)
            {
                Rotation = levelMakerHelper.transform.rotation.eulerAngles.z;
                Length = levelMakerHelper.transform.localScale.x;
                Color = levelMakerHelper.Color;
            }
        }
        public LevelSceneData()
        {

        }
    }
}