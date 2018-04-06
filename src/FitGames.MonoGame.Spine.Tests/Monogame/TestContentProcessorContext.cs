using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FitGames.MonoGame.Spine.Tests.Monogame
{
    public sealed class TestContentProcessorContext : ContentProcessorContext
    {
        private readonly string _outputDir;

        public TestContentProcessorContext(string outputDir) : base()
        {
            _outputDir = outputDir;
        }

        public override string BuildConfiguration => throw new NotImplementedException();

        public override string IntermediateDirectory => throw new NotImplementedException();

        public override ContentBuildLogger Logger => throw new NotImplementedException();

        public override string OutputDirectory { get { return _outputDir; } }

        public override string OutputFilename => throw new NotImplementedException();

        public override OpaqueDataDictionary Parameters => throw new NotImplementedException();

        public override TargetPlatform TargetPlatform => throw new NotImplementedException();

        public override GraphicsProfile TargetProfile => throw new NotImplementedException();

        public override void AddDependency(string filename)
        {
            throw new NotImplementedException();
        }

        public override void AddOutputFile(string filename)
        {
            throw new NotImplementedException();
        }

        public override TOutput BuildAndLoadAsset<TInput, TOutput>(ExternalReference<TInput> sourceAsset, string processorName, OpaqueDataDictionary processorParameters, string importerName)
        {
            if (typeof(TInput) == typeof(Texture2DContent) && typeof(TOutput) == typeof(Texture2DContent))
            {
                return (TOutput)(new TextureImporter().Import(sourceAsset.Filename, new TestContentImporterContext()) as object);
            }
            
            throw new NotImplementedException();
        }

        public override ExternalReference<TOutput> BuildAsset<TInput, TOutput>(ExternalReference<TInput> sourceAsset, string processorName, OpaqueDataDictionary processorParameters, string importerName, string assetName)
        {
            throw new NotImplementedException();
        }

        public override TOutput Convert<TInput, TOutput>(TInput input, string processorName, OpaqueDataDictionary processorParameters)
        {
            throw new NotImplementedException();
        }
    }
}
