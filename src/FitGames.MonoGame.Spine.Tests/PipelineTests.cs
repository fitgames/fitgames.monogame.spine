using FitGames.MonoGame.Spine.Pipeline;
using FitGames.MonoGame.Spine.Tests.Monogame;
using NUnit.Framework;
using System.IO;

namespace FitGames.MonoGame.Spine.Tests
{
    [TestFixture]
    public class PipelineTests
    {
        [Test]
        public void test_spine_importer_spgr_is_valid()
        {
            var filepath = $"{TestContext.CurrentContext.TestDirectory}\\Assets\\spineboy.spgr";
            var impoter = new SpineContentImporter().Import(filepath, new TestContentImporterContext());

            Assert.IsTrue(impoter.Atlas.Contains(@"spineboy.png"));
            Assert.IsTrue(!string.IsNullOrEmpty(impoter.TempTexturePath));
        }

        [Test]
        public void test_spine_importer_zip_is_valid()
        {
            var filepath = $"{TestContext.CurrentContext.TestDirectory}\\Assets\\spineboy.zip";
            var impoter = new SpineContentImporter().Import(filepath, new TestContentImporterContext());

            Assert.IsTrue(impoter.Atlas.Contains(@"spineboy.png"));
            Assert.IsTrue(!string.IsNullOrEmpty(impoter.TempTexturePath));
        }

        [Test]
        public void test_spine_processor_is_valid()
        {
            var filepath = $"{TestContext.CurrentContext.TestDirectory}\\Assets\\spineboy.spgr";
            var impoter = new SpineContentImporter().Import(filepath, new TestContentImporterContext());
            var processor = new SpineContentProcessor().Process(impoter, new TestContentProcessorContext(TestContext.CurrentContext.TestDirectory));

            Assert.IsFalse(File.Exists(impoter.TempTexturePath));
        }
    }
}
