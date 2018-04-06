using System;
using System.Collections.Generic;
using System.IO;

namespace Spine.Monogame.Pipeline
{
    public sealed class ContentAtlas
    {
        private string _atlas;
        private List<AtlasPage> _pages = new List<AtlasPage>();
        private List<AtlasRegion> _regions = new List<AtlasRegion>();

        public IEnumerable<AtlasPage> AtlasPages
        {
            get { return _pages; }
        }

        public IEnumerable<AtlasRegion> AtlasRegions
        {
            get { return _regions; }
        }

        public ContentAtlas(string atlas)
        {
            _atlas = atlas;
        }

        public ContentAtlas Load()
        {
            var tuple = new string[4];
            AtlasPage page = null;

            using (var reader = new StringReader(_atlas))
            {
                while (reader.Peek() > 0)
                {
                    var line = reader.ReadLine();

                    if (line == null) break;

                    if (line.Trim().Length == 0) { page = null; }
                    else if (page == null)
                    {
                        page = new AtlasPage
                        {
                            name = line
                        };

                        if (Atlas.ReadTuple(reader, tuple) == 2)
                        {
                            // size is only optional for an atlas packed with an old TexturePacker.
                            page.width = int.Parse(tuple[0]);
                            page.height = int.Parse(tuple[1]);
                            Atlas.ReadTuple(reader, tuple);
                        }
                        page.format = (Format)Enum.Parse(typeof(Format), tuple[0], false);

                        Atlas.ReadTuple(reader, tuple);
                        page.minFilter = (TextureFilter)Enum.Parse(typeof(TextureFilter), tuple[0], false);
                        page.magFilter = (TextureFilter)Enum.Parse(typeof(TextureFilter), tuple[1], false);

                        string direction = Atlas.ReadValue(reader);
                        page.uWrap = TextureWrap.ClampToEdge;
                        page.vWrap = TextureWrap.ClampToEdge;
                        if (direction == "x")
                            page.uWrap = TextureWrap.Repeat;
                        else if (direction == "y")
                            page.vWrap = TextureWrap.Repeat;
                        else if (direction == "xy")
                            page.uWrap = page.vWrap = TextureWrap.Repeat;


                        _pages.Add(page);
                    }
                    else
                    {
                        var region = new AtlasRegion();
                        region.name = line;
                        region.page = page;

                        region.rotate = Boolean.Parse(Atlas.ReadValue(reader));

                        Atlas.ReadTuple(reader, tuple);
                        int x = int.Parse(tuple[0]);
                        int y = int.Parse(tuple[1]);

                        Atlas.ReadTuple(reader, tuple);
                        int width = int.Parse(tuple[0]);
                        int height = int.Parse(tuple[1]);

                        region.u = x / (float)page.width;
                        region.v = y / (float)page.height;
                        if (region.rotate)
                        {
                            region.u2 = (x + height) / (float)page.width;
                            region.v2 = (y + width) / (float)page.height;
                        }
                        else
                        {
                            region.u2 = (x + width) / (float)page.width;
                            region.v2 = (y + height) / (float)page.height;
                        }
                        region.x = x;
                        region.y = y;
                        region.width = Math.Abs(width);
                        region.height = Math.Abs(height);

                        if (Atlas.ReadTuple(reader, tuple) == 4)
                        { // split is optional
                            region.splits = new[] {int.Parse(tuple[0]), int.Parse(tuple[1]),
                                int.Parse(tuple[2]), int.Parse(tuple[3])};

                            if (Atlas.ReadTuple(reader, tuple) == 4)
                            { // pad is optional, but only present with splits
                                region.pads = new[] {int.Parse(tuple[0]), int.Parse(tuple[1]),
                                    int.Parse(tuple[2]), int.Parse(tuple[3])};

                                Atlas.ReadTuple(reader, tuple);
                            }
                        }

                        region.originalWidth = int.Parse(tuple[0]);
                        region.originalHeight = int.Parse(tuple[1]);

                        Atlas.ReadTuple(reader, tuple);
                        region.offsetX = int.Parse(tuple[0]);
                        region.offsetY = int.Parse(tuple[1]);

                        region.index = int.Parse(Atlas.ReadValue(reader));

                        _regions.Add(region);
                    }
                }
            }

            return this;
        }
    }
}
