using UnityEngine;

namespace Chameleon
{
    public class Ground : EntityLogicWithData
    {
        private EnumColor m_EnumColor;
        [SerializeField]
        MeshRenderer m_MeshRenderer;
        static int colorPropertyId = Shader.PropertyToID("_Color");
        static MaterialPropertyBlock sharedPropertyBlock;
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
        }
        public void SetColor(EnumColor enumColor)
        {
            m_EnumColor = enumColor;
            Color color = GameEntry.Data.GetData<Data.DataColor>().GetColor((int)enumColor);
            if (sharedPropertyBlock == null)
            {
                sharedPropertyBlock = new MaterialPropertyBlock();
            }
            sharedPropertyBlock.SetColor(colorPropertyId, color);
            m_MeshRenderer.SetPropertyBlock(sharedPropertyBlock);
        }
        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<Player>().Destroy(m_EnumColor);
            }
        }
    }
}