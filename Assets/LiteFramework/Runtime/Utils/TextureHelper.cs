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
        
        public static Texture2D ColorToTexture2D(int width, int height, Color color)
        {
            Color[] pixels = new Color[width * height];

            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = color;
            }

            Texture2D texture = new Texture2D(width, height);

            texture.SetPixels(pixels);
            texture.Apply();

            return texture;
        }

        public static Texture2D ColorToTexture2D(int width, int height, Color color, Color borderColor, int borderWidth)
        {
            Color[] pixels = new Color[width * height];

            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = color;
            }
            var tex = new Texture2D(width, height);
            tex.SetPixels(pixels);
            for (int x = 0; x < tex.width; x++) {
                for (int y = 0; y < tex.height; y++) {
                    if (x < borderWidth || x> tex.width-1-borderWidth) tex.SetPixel(x, y, borderColor);
                    else if (y < borderWidth || y>tex.height-1-borderWidth) tex.SetPixel(x, y, borderColor);
                }
            }
            tex.Apply();

            return tex;
        }
    }
}