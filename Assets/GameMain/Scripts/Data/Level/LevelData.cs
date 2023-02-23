using UnityEngine;

namespace Chameleon.Data
{
    public class LevelData
    {
        private DRLevel dRLevel;
        public int Id
        {
            get
            {
                return dRLevel.Id;
            }
        }
        public string Name
        {
            get
            {
                return GameEntry.Localization.GetString(dRLevel.NameId);
            }
        }

        public string Description
        {
            get
            {
                return GameEntry.Localization.GetString(dRLevel.DescriptionId);
            }
        }
        public int TimeSecond{
            set{
                GameEntry.Setting.SetInt($"Level{Id}.TimeSecond",value);
            }
            get{
                return GameEntry.Setting.GetInt($"Level{Id}.TimeSecond",999);
            }
        }
        public int TimeMillisecond{
            set{
                GameEntry.Setting.SetInt($"Level{Id}.TimeMillisecond",value);
            }
            get{
                return GameEntry.Setting.GetInt($"Level{Id}.TimeMillisecond",999);
            }
        }
        public bool Cube{
            set{
                GameEntry.Setting.SetBool($"Level{Id}.Cube",value);
            }
            get{
                return GameEntry.Setting.GetBool($"Level{Id}.Cube",false);
            }
        }
        public bool Sphere{
            set{
                GameEntry.Setting.SetBool($"Level{Id}.Sphere",value);
            }
            get{
                return GameEntry.Setting.GetBool($"Level{Id}.Sphere",false);
            }
        }
        public bool Change{
            set{
                GameEntry.Setting.SetBool($"Level{Id}.Change",value);
            }
            get{
                return GameEntry.Setting.GetBool($"Level{Id}.Change",false);
            }
        }
        public int SceneId{
            get => dRLevel.SceneId;
        }
        public LevelData(DRLevel dRLevel)
        {
            this.dRLevel = dRLevel;
        }
    }
}