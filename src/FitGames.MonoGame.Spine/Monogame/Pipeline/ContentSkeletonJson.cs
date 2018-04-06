using System;
using System.IO;

namespace Spine.Monogame.Pipeline
{
    public sealed class ContentSkeletonJson : SkeletonJson
    {
        private readonly string _json;

        public ContentSkeletonJson(string json, params Atlas[] atlas) : base(atlas)
        {
            if (string.IsNullOrWhiteSpace(json)) throw new NullReferenceException($"{nameof(json)}");

            _json = json;
        }

        public SkeletonData ReadSkeletonData()
        {
            using (var reader = new StringReader(_json))
            {
                var skeletonData = ReadSkeletonData(reader);
                skeletonData.Name = "skeleton"; //Path.GetFileNameWithoutExtension(path);
                return skeletonData;
            }
        }
    }
}
