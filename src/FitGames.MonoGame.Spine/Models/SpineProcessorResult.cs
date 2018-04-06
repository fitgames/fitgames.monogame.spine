using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Spine.Monogame.Pipeline;

namespace FitGames.MonoGame.Spine.Models
{
    public sealed class SpineProcessorResult
    {
        public ContentAtlas Atlas { get; private set; }
        public Texture2DContent Texture { get; internal set; }
        public string SkeletonJson { get; private set; }

        public SpineProcessorResult(SpineImporterResult importerResult)
        {
            Atlas = new ContentAtlas(importerResult.Atlas)
                .Load();
            SkeletonJson = importerResult.Json;
        }
    }
}
