using UnityEngine;

namespace Chameleon
{
    public class Collection : EntityLogicWithData
    {
        [SerializeField]
        private EnumEntity EntityType;
        [SerializeField]
        MeshRenderer m_MeshRenderer;
        [SerializeField]
        Color color;
        static int colorPropertyId = Shader.PropertyToID("_Color");
        static MaterialPropertyBlock sharedPropertyBlock;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            SetColor(color);
        }
        private void Collect()
        {
            GameEntry.Event.Fire(this, CollectEventArgs.Create(EntityType));
        }
        public void SetColor(Color color)
        {
            if (sharedPropertyBlock == null)
            {
                sharedPropertyBlock = new MaterialPropertyBlock();
            }
            sharedPropertyBlock.SetColor(colorPropertyId, color);
            m_MeshRenderer.SetPropertyBlock(sharedPropertyBlock);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                Collect();
            }
        }
    }
}