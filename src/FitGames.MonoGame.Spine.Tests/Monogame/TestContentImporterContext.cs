using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.IO;

namespace FitGames.MonoGame.Spine.Tests.Monogame
{
    public class TestContentImporterContext : ContentImporterContext
    {
        public override string IntermediateDirectory
        {
            get { return Directory.GetCurrentDirectory(); }
        }

        public override ContentBuildLogger Logger => throw new System.NotImplementedException();

        public override string OutputDirectory
        {
            get { return Directory.GetCurrentDirectory(); }
        }

        public override void AddDependency(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
