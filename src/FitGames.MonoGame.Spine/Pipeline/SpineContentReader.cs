using FitGames.MonoGame.Spine.Models;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Spine;
using Spine.Monogame.Pipeline;
using System;
using System.Collections.Generic;

namespace FitGames.MonoGame.Spine.Pipeline
{
    class SpineContentReader : ContentTypeReader<SpineAsset>
    {
        protected override SpineAsset Read(ContentReader input, SpineAsset existingInstance)
        {
            var pages = input.ReadObject<List<AtlasPage>>();
            var regions = input.ReadObject<List<AtlasRegion>>();
            var texture = input.ReadObject<Texture2D>();
            var skeletonJson = input.ReadString();

            // Fix texture reference
            foreach (var page in pages)
            {
                page.rendererObject = texture;
                page.width = texture.Width;
                page.height = texture.Height;

                foreach (var region in regions)
                {
                    if (region.page == null)
                    {
                        region.page = page;
                        continue;
                    }

                    // This happens because regions are loaded before texture in process, and the write/read looses the reference
                    if (region.page.name.Equals(page.name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        region.page = null;
                        region.page = page;
                    }
                }
            }

            var atlas = new Atlas(pages: pages, regions: regions);
            var contentSkeleton = new ContentSkeletonJson(skeletonJson, atlas);

            return new SpineAsset(atlas, contentSkeleton);
        }
    }
}
