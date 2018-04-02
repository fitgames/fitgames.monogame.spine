using FitGames.MonoGame.Spine.Models;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace FitGames.MonoGame.Spine.Pipeline
{
    [ContentTypeWriter]
    public class SpineContentWriter : ContentTypeWriter<SpineProcessorResult>
    {
        protected override void Write(ContentWriter output, SpineProcessorResult value)
        {
            output.WriteObject(value.Atlas.AtlasPages);
            output.WriteObject(value.Atlas.AtlasRegions);
            output.WriteObject(value.Texture);
            output.Write(value.SkeletonJson);
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(SpineProcessorResult).AssemblyQualifiedName;
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(SpineContentReader).AssemblyQualifiedName;
        }
    }
}
