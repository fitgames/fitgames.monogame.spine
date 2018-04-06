using FitGames.MonoGame.Spine;
using FitGames.MonoGame.Spine.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spine;
using System.Linq;

namespace Example2
{
    public class GameExample : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont basicFont;

        // Spine Demo
        SpineAsset spineboy;
        SkeletonMeshRenderer skeletonRenderer;

        int animationIndex = 0;
        KeyboardState prevKeyboardState = Keyboard.GetState();

        public GameExample()
        {
            IsMouseVisible = true;

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            basicFont = Content.Load<SpriteFont>("basicFont");

            skeletonRenderer = new SkeletonMeshRenderer(graphics.GraphicsDevice);

            spineboy = Content.Load<SpineAsset>("spineboy");
            spineboy.AnimationState.SetAnimation(0, spineboy.SkeletonData.Animations.Items[0].Name, true);
            spineboy.Skeleton.X += 400;
            spineboy.Skeleton.Y += this.GraphicsDevice.Viewport.Height;
            spineboy.Skeleton.UpdateWorldTransform();
        }

        protected override void UnloadContent()
        {
            spineboy?.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                spineboy.SetScale(MathHelper.Clamp(spineboy.SkeletonJson.Scale + 0.01f, 0.01f, 1f));
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                spineboy.SetScale(MathHelper.Clamp(spineboy.SkeletonJson.Scale - 0.01f, 0.01f, 1f));
            }

            if (keyboardState.IsKeyDown(Keys.Up) && prevKeyboardState.IsKeyUp(Keys.Up))
            {
                animationIndex = MathHelper.Clamp(animationIndex - 1, 0, spineboy.SkeletonData.Animations.Count - 1);
                spineboy.AnimationState.SetAnimation(0,
                     spineboy.SkeletonData.Animations.Items[animationIndex].Name, true);
            }

            if (keyboardState.IsKeyDown(Keys.Down) && prevKeyboardState.IsKeyUp(Keys.Down))
            {
                animationIndex = MathHelper.Clamp(animationIndex + 1, 0, spineboy.SkeletonData.Animations.Count - 1);
                spineboy.AnimationState.SetAnimation(0,
                     spineboy.SkeletonData.Animations.Items[animationIndex].Name, true);
            }

            spineboy.AnimationState.Update(gameTime.ElapsedGameTime.Milliseconds / 1000f);
            prevKeyboardState = keyboardState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spineboy.AnimationState.Apply(spineboy.Skeleton);
            spineboy.Skeleton.UpdateWorldTransform();

            skeletonRenderer.Begin();
            skeletonRenderer.Draw(spineboy.Skeleton);
            skeletonRenderer.End();

            spriteBatch.Begin();

            for (var x = 0; x < spineboy.SkeletonData.Animations.Count;x++)
            {
                var color = animationIndex == x ? Color.Yellow : Color.WhiteSmoke;

                spriteBatch.DrawString(basicFont, spineboy.SkeletonData.Animations.Items[x].Name, 
                    new Vector2(20, 26 * x), color);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
