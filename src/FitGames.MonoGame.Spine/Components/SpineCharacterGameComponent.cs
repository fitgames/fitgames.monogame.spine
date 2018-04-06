using Microsoft.Xna.Framework;
using Spine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGames.MonoGame.Spine.Components
{
    public class SpineCharacterGameComponent : DrawableGameComponent
    {
        private readonly ISpineAnimationService _animationService;
        private Skeleton _skeleton = null;

        public SpineCharacterGameComponent(Game game) : base(game)
        {
            _animationService = game.Services.GetService<ISpineAnimationService>() ?? null;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _animationService.Render(_skeleton);

            base.Draw(gameTime);
        }
    }
}
