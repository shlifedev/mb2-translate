using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace MBKoreanFont
{
    public class TextureLoader : ITwoDimensionResourceContext
    {
        private readonly TaleWorlds.TwoDimension.Texture texture; 
        public TextureLoader(TaleWorlds.TwoDimension.Texture texture)
        {
            this.texture = texture;
        } 
        public TaleWorlds.TwoDimension.Texture LoadTexture(ResourceDepot resourceDepot, string name)
        {
            return texture;
        }
    }
}