using UnityEngine;

namespace LiteFramework.Runtime.Base
{
    public abstract class BaseMonoBehaviour : MonoBehaviour
    {
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }

        public Transform Parent
        {
            get => transform.parent;
            set => transform.SetParent(value);
        }

        public Vector3 LocalScale
        {
            get => transform.localScale;
            set => transform.localScale = value;
        }
        
        public void SetScale(Vector3 scale)
        {
            transform.localScale = scale;
        }

        public void SetScale(float scale)
        {
            transform.localScale = Vector3.one * scale;
        }

        public void SetNodeActive(bool value)
        {
            gameObject.SetActive(value);
        } 
    }
}