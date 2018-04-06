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

        public void SetScale(float scale)
        {
            if (SkeletonJson.Scale != scale)
            {
                var prevX = Skeleton.X;
                var prevY = Skeleton.Y;
                var animationName = AnimationState.GetCurrent(0).Animation.Name;

                SkeletonJson.Scale = scale;
                Setup(true);

                AnimationState.SetAnimation(0, animationName, true);
                Skeleton.X += prevX;
                Skeleton.Y += prevY;
            }
        }

        public Atlas Atlas { get; private set; }
        public ContentSkeletonJson SkeletonJson
        {
            get { return _skeletonJson; }
            private set
            {
                _skeletonJson = value;

                Setup();
            }
        }
        public SkeletonData SkeletonData { get; private set; }
        public Skeleton Skeleton { get; private set; }
        public AnimationState AnimationState { get; private set; }

        private void Setup(bool reload = false)
        {
            if (SkeletonData == null || reload)
            {
                SkeletonData = _skeletonJson.ReadSkeletonData();
                SkeletonData.Name = "skeleton";
            }

            if (Skeleton == null || reload)
            {
                Skeleton = new Skeleton(SkeletonData);
            }

            if (AnimationState == null || reload)
            {
                _animationStateData = new AnimationStateData(Skeleton.Data);
                AnimationState = new AnimationState(_animationStateData);
            }
        }

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
