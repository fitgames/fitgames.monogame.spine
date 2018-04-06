using Spine;
using Spine.Monogame.Pipeline;
using System;

namespace FitGames.MonoGame.Spine.Models
{
    public class SpineAsset : IDisposable
    {
        private bool _disposed = false;
        private ContentSkeletonJson _skeletonJson = null;
        private AnimationStateData _animationStateData = null;

        public SpineAsset(Atlas atlas, ContentSkeletonJson contentSkeletonJson)
        {
            Atlas = atlas;
            SkeletonJson = contentSkeletonJson;
        }

        public Atlas Atlas { get; private set; }
        public ContentSkeletonJson SkeletonJson
        {
            get { return _skeletonJson; }
            private set
            {
                _skeletonJson = value;

                if (SkeletonData == null)
                {
                    SkeletonData = _skeletonJson.ReadSkeletonData();
                    SkeletonData.Name = "skeleton";
                }

                if (Skeleton == null)
                    Skeleton = new Skeleton(SkeletonData);

                if (AnimationState == null)
                {
                    _animationStateData = new AnimationStateData(Skeleton.Data);
                    AnimationState = new AnimationState(_animationStateData);
                }
            }
        }
        public SkeletonData SkeletonData { get; private set; }
        public Skeleton Skeleton { get; private set; }
        public AnimationState AnimationState { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool isDisposing)
        {
            if (_disposed) return;

            if (isDisposing)
            {
                _disposed = true;

                if (Atlas != null)
                {
                    Atlas.Dispose();
                    Atlas = null;
                }
            }
        }
    }
}
