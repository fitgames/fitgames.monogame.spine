using FitGames.MonoGame.Spine.Models;
using Microsoft.Xna.Framework.Content.Pipeline;
using System.IO;
using System.IO.Compression;

namespace FitGames.MonoGame.Spine.Pipeline
{
    [ContentImporter(".spgr", ".zip", DisplayName = "Spine Animation", DefaultProcessor = "Spine Game Ready Processor")]
    public sealed class SpineContentImporter : ContentImporter<SpineImporterResult>
    {
        public override SpineImporterResult Import(string filename, ContentImporterContext context)
        {
            string atlas = null,
                   json = null,
                   texturePath = null;

            using (var zipFile = new FileStream(filename, FileMode.Open))
            {
                using (var zipArchive = new ZipArchive(zipFile, ZipArchiveMode.Read))
                {
                    foreach (var entry in zipArchive.Entries)
                    {
                        var extension = Path.GetExtension(entry.Name.ToLowerInvariant());
                        using (var stream = entry.Open())
                        {
                            switch (extension)
                            {
                                case ".atlas":
                                    using (var reader = new StreamReader(stream))
                                        atlas = reader.ReadToEnd();
                                    break;
                                case ".json":
                                    using (var reader = new StreamReader(stream))
                                        json = reader.ReadToEnd();
                                    break;
                                case ".jpg":
                                case ".bmp":
                                case ".png":
                                    using (var mem = new MemoryStream())
                                    {
                                        stream.CopyTo(mem);
                                        texturePath = Path.Combine(Path.GetTempPath(), entry.Name);

                                        if (File.Exists(texturePath))
                                            File.Delete(texturePath);

                                        File.WriteAllBytes(texturePath, mem.ToArray());
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            return new SpineImporterResult(atlas, json, texturePath);
        }
    }
}
