using UnityEngine;

namespace Chameleon
{
    public class End : EntityLogicEx
    {
        [SerializeField]
        Transform m_Root;
        [SerializeField]
        float m_RotateSpeed;
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
        }
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            m_Root.Rotate(new Vector3(0, m_RotateSpeed, 0));
        }
    }
}