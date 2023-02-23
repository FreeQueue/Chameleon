using System;
using GameFramework;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// System.TimeSpan 变量类。
    /// </summary>
    public sealed class VarTimeSpan : Variable<TimeSpan>
    {
        /// <summary>
        /// 初始化 System.TimeSpan 变量类的新实例。
        /// </summary>
        public VarTimeSpan()
        {
        }

        /// <summary>
        /// 从 System.TimeSpan 到 System.TimeSpan 变量类的隐式转换。
        /// </summary>
        /// <param name="value">值。</param>
        public static implicit operator VarTimeSpan(TimeSpan value)
        {
            VarTimeSpan varValue = ReferencePool.Acquire<VarTimeSpan>();
            varValue.Value = value;
            return varValue;
        }

        /// <summary>
        /// 从 System.TimeSpan 变量类到 System.TimeSpan 的隐式转换。
        /// </summary>
        /// <param name="value">值。</param>
        public static implicit operator TimeSpan(VarTimeSpan value)
        {
            return value.Value;
        }
    }
}
