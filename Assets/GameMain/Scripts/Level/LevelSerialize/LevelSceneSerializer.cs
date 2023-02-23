using UnityEngine;
using GameFramework;
namespace Chameleon
{
    public class LevelSceneSerializer : GameFrameworkSerializer<LevelScene>
    {
        private static readonly byte[] Header = new byte[] { (byte)'C', (byte)'L', (byte)'S' };
        /// <summary>
        /// 获取物品格信息头标识。
        /// </summary>
        /// <returns>物品格信息头标识。</returns>
        protected override byte[] GetHeader()
        {
            return Header;
        }
    }
}