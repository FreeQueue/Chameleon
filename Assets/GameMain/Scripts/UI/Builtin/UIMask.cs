using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.Event;
using UnityEngine.UI;

namespace Chameleon
{
    public class UIMask : UGuiFormEx
    {
        [SerializeField]
        Image m_Image;
        private UIMaskParams uIMaskParams;
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            uIMaskParams = userData as UIMaskParams;
            if (uIMaskParams == null) return;
            m_Image.color = new Color(0, 0, 0, uIMaskParams.Alpha);
            if (uIMaskParams.WaitTime <= 0) return;
            StartCoroutine(WaitCon(uIMaskParams.WaitTime));
        }
        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }
        private IEnumerator WaitCon(float time)
        {
            yield return new WaitForSeconds(time);
            if (uIMaskParams.OnFinishCallBack != null)
                uIMaskParams.OnFinishCallBack.Invoke();
            uIMaskParams.Clear();
            Close();
        }
    }

}