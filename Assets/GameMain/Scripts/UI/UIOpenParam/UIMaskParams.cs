//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;

namespace Chameleon
{
    /// <summary>
    /// 对话框显示数据。
    /// </summary>
    public class UIMaskParams:IReference
    {
        public float WaitTime
        {
            get; private set;
        }
        public float Alpha
        {
            get; private set;
        }
        /// <summary>
        /// 确定按钮回调。
        /// </summary>
        public GameFrameworkAction OnFinishCallBack
        {
            get;
            private set;
        }
        public void Clear()
        {

        }
        public static UIMaskParams Create(float waitTime, float alpha, GameFrameworkAction onFinishCallBack = null)
        {
            UIMaskParams uIMaskParams = ReferencePool.Acquire<UIMaskParams>();
            uIMaskParams.WaitTime = waitTime;
            uIMaskParams.Alpha = alpha;
            uIMaskParams.OnFinishCallBack = onFinishCallBack;
            return uIMaskParams;
        }
    }
}
