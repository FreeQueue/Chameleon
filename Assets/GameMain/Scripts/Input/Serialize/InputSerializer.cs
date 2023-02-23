using UnityEngine;
using GameFramework;
namespace Chameleon
{
    public class InputSettingSerializer : GameFrameworkSerializer<InputSetting>,IReference
    {
        private static readonly byte[] Header = new byte[] { (byte)'C', (byte)'I', (byte)'D' };
        /// <summary>
        /// 获取物品格信息头标识。
        /// </summary>
        /// <returns>物品格信息头标识。</returns>
        protected override byte[] GetHeader()
        {
            return Header;
        }
        public static InputSettingSerializer Create(){
            return ReferencePool.Acquire<InputSettingSerializer>();
        }
        public void Clear()
        {
            
        }
    }
}