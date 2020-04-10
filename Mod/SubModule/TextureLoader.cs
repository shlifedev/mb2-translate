using TaleWorlds.Library;
using TaleWorlds.TwoDimension;
/// <summary>
/// 모드파일/소스코드 무단수정 배포 금지합니다.
/// Writer : shlifedev@gmail.com 
/// </summary>
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