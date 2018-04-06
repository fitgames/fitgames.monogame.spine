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

        MouseState prevMouseState = Mouse.GetState();

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
            spineboy.AnimationState.SetAnimation(0, "idle", true);
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

            var mouseState = Mouse.GetState();

            spineboy.SkeletonJson.Scale = 5;

            spineboy.AnimationState.Update(gameTime.ElapsedGameTime.Milliseconds / 1000f);

            prevMouseState = mouseState;

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
            spriteBatch.DrawString(basicFont, "test", Vector2.Zero, Color.WhiteSmoke);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
