using Unity.Collections;
using UnityEngine;

namespace OdysseyPatch.OutfitStandBodyType
{
    public static class TextureUtility
    {
        public struct TextureOptions
        {
            public Texture2D tex;
            public Vector2 offset;
        }

        public static Texture2D Combine(params TextureOptions[] textures)
        {
            int width = textures[0].tex.width;
            int height = textures[0].tex.height;
            NativeArray<byte> baseData = GetReadableTexture(textures[0].tex).GetPixelData<byte>(0);
            for (int i = 1; i < textures.Length; i++)
            {
                NativeArray<byte> data = GetReadableTexture(textures[i].tex).GetPixelData<byte>(0);
                Vector2 offset = textures[i].offset;
                for (int j = 3; j < data.Length; j += 4)
                {
                    if (data[j] > 0)
                    {
                        int dataOffset = (int)(offset.x * width) * 4 + (int)(offset.y * height) * 4 * width;
                        baseData[j + dataOffset - 3] = data[j - 3];
                        baseData[j + dataOffset - 2] = data[j - 2];
                        baseData[j + dataOffset - 1] = data[j - 1];
                        baseData[j + dataOffset] = data[j];
                    }
                }
            }
            Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, true);
            texture.SetPixelData(baseData, 0);
            texture.filterMode = FilterMode.Point;
            texture.Apply();
            return texture;
        }

        private static Texture2D GetReadableTexture(Texture2D source)
        {
            RenderTexture renderTex = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
            Graphics.Blit(source, renderTex);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTex;
            Texture2D output = new Texture2D(source.width, source.height);
            output.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
            output.Apply();
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTex);
            return output;
        }
    }
}
