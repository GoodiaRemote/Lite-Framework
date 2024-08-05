using UnityEngine;

namespace LiteFramework.Runtime.Utils
{
    public static class TextureHelper
    {
        public static Texture2D ToTexture2D(this Sprite sprite)
        {
            var texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            var pixels = sprite.texture.GetPixels(
                    (int)sprite.textureRect.x, 
                    (int)sprite.textureRect.y, 
                    (int)sprite.textureRect.width, 
                    (int)sprite.textureRect.height
                );
            texture.SetPixels(pixels);
            texture.Apply();
            return texture;
        }
    }
}