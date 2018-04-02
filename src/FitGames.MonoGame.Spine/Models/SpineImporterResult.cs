namespace FitGames.MonoGame.Spine.Models
{
    public sealed class SpineImporterResult
    {
        public string Atlas { get; private set; }
        public string Json { get; private set; }
        public string TempTexturePath { get; private set; }

        public SpineImporterResult(string atlas, string json, string texturePath)
        {
            Atlas = atlas;
            Json = json;
            TempTexturePath = texturePath;
        }
    }
}
