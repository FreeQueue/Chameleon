using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Event;

namespace Chameleon
{
    public class Player : EntityLogicEx, IPause
    {
        private bool IsAlive, IsGrounded, m_IsRush, JumpTwice;

        private bool IsRush
        {
            get => m_IsRush;
            set
            {
                m_IsRush = value;
                m_Animator.SetBool("Rush", value);
            }
        }
        [SerializeField]
        private Renderer m_BodyRenderer;
        [SerializeField]
        private Rigidbody2D m_Rigidbody;
        [SerializeField]
        private Animator m_Animator;

        [SerializeField]
        private float m_JumpForce, m_JumpHoverForce, m_RunSpeed, m_RushSpeed;
        private Material m_Material;
        public EnumColor m_EnumColor;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_Material = m_BodyRenderer.material;
        }
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            SetColor(EnumColor.Orange);
            m_RushTimer = m_RushTime;
            IsGrounded = false;
            m_IsRush = false;
            JumpTwice = false;
            IsAlive = true;
        }
        public void update(float elapseSeconds, float realElapseSeconds)
        {
            RushTimer(realElapseSeconds);
            transform.AddLocalPositionX(realElapseSeconds * (IsRush ? m_RushSpeed : m_RunSpeed));
        }
        public void Pause()
        {
            m_Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        public void Resume()
        {
            m_Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        public void ChangeColor()
        {
            if (m_EnumColor == EnumColor.Orange)
            {
                SetColor(EnumColor.Purple);
            }
            else
            {
                SetColor(EnumColor.Orange);
            }
            GameEntry.Event.Fire(this,ColorChangeEventArgs.Create(m_EnumColor));
        }
        private void SetColor(EnumColor enumColor)
        {
            m_EnumColor = enumColor;
            m_Material.SetColor("_Color", GameEntry.Data.GetData<Data.DataColor>().GetColor((int)enumColor));
        }
        public void Rush()
        {
            IsRush = true;
        }
        [SerializeField]
        private float m_RushTime = 5;
        private float m_RushTimer;
        private void RushTimer(float realElapseSeconds)
        {
            if (IsRush)
            {
                m_RushTimer -= realElapseSeconds;
                if (m_RushTimer <= 0) IsRush = false;
            }
        }
        public void Jump()
        {
            if (IsGrounded)
            {
                IsGrounded = false;
                JumpTwice = true;
                m_Animator.SetBool("Jump1", true);
                m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, m_JumpForce);
            }
            else if (JumpTwice)
            {
                JumpTwice = false;
                m_Animator.SetTrigger("Jump2");
                m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, m_JumpForce);
            }
        }
        public void JumpHover()
        {
            if (!IsGrounded) m_Rigidbody.velocity += new Vector2(0, m_JumpHoverForce);
        }
        public void Destroy(EnumColor enumColor)
        {
            if (IsAlive&&enumColor != m_EnumColor)
            {
                IsAlive = false;
                GameEntry.Event.Fire(this, LevelFailEventArgs.Create());
            }
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Ground")
            {
                IsGrounded = true;
                m_Animator.SetBool("Jump1", false);
            }
        }
    }
}