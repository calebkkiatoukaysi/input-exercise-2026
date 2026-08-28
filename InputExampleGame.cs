using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InputExercise;

public class InputExampleGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Ball[] _balls;

    protected KeyboardState currentKeyboardState;

    protected KeyboardState priorKeyboardState;

    protected GamePadState currentGamePadState;

    protected GamePadState priorGamePadState;

    protected MouseState currentMouseState;

    protected MouseState priorMouseState;

    public MathHelper.Random Random {get; init;} = new();


    public InputExampleGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _balls = new Ball[] {
            new Ball(this, Color.Red) { Position = new Vector2(250, 200) },
            new Ball(this, Color.Green) { Position = new Vector2(350, 200) },
            new Ball(this, Color.Blue) { Position = new Vector2(450, 200) }
        };

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        foreach (Ball b in _balls) b.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        currentKeyboardState = Keyboard.GetState();
        currentGamePadState = GamePad.GetState(PlayerIndex.One);
        currentMouseState = Mouse.GetState();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || currentKeyboardState.IsKeyDown(Keys.Escape))
            Exit();

        // Keyboard Input
        if(currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.W))
            _balls[0].Position += new Vector2(0, -100 * (float) gameTime.ElapsedGameTime.TotalSeconds);

        if(currentKeyboardState.IsKeyDown(Keys.Down) || currentKeyboardState.IsKeyDown(Keys.S))
            _balls[0].Position += new Vector2(0, 100 * (float) gameTime.ElapsedGameTime.TotalSeconds);
    
        if(currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A))
            _balls[0].Position += new Vector2(-100 * (float) gameTime.ElapsedGameTime.TotalSeconds, 0);
    
        if(currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D))
            _balls[0].Position += new Vector2(100 * (float) gameTime.ElapsedGameTime.TotalSeconds, 0);

        if(currentKeyboardState.IsKeyDown(Keys.Space) && priorKeyboardState.IsKeyUp(Keys.Space))
            _balls[0].Warp();
        
        // gamepad input
        _balls[1].Position += 100 * (float)gameTime.ElapsedGameTime.Seconds * currentGamePadState.ThumbSticks.Right;

        if(currentGamePadState.Buttons.A == ButtonState.Pressed && priorGamePadState.Buttons.A == ButtonState.Released)
            _balls[1].Warp();

        //mouse input
        _balls[2].Position = new Vector2(currentMouseState.Position.X, currentMouseState.Position.Y);

        if(currentMouseState.LeftButton == ButtonState.Pressed && priorMouseState.LeftButton == ButtonState.Released)
        {
            _balls[0].Warp();
            _balls[1].Warp();
        }

        priorKeyboardState = currentKeyboardState;
        priorGamePadState = currentGamePadState;
        priorMouseState = currentMouseState;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        foreach (Ball b in _balls) b.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
