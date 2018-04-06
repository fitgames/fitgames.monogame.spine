using FitGames.MonoGame.Spine.Models;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using System.IO;

namespace FitGames.MonoGame.Spine.Pipeline
{
    [ContentProcessor(DisplayName = "Spine Game Ready Content Processor")]
    public sealed class SpineContentProcessor : ContentProcessor<SpineImporterResult, SpineProcessorResult>
    {
        public SpineContentProcessor() { }

        public override SpineProcessorResult Process(SpineImporterResult input, ContentProcessorContext context)
        {
            var textureContent = context.BuildAndLoadAsset<Texture2DContent, Texture2DContent>(
                new ExternalReference<Texture2DContent>(input.TempTexturePath), "TextureProcessor");

            File.Delete(input.TempTexturePath);

            return new SpineProcessorResult(input)
            {
                Texture = textureContent
            };
        }
    }
}
