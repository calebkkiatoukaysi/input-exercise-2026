using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InputExercise
{
    public class Ball
    {
        /// <summary>
        /// The game this ball is a part of
        /// </summary>
        private InputExampleGame _game;

        /// <summary>
        /// A color to help distinguish one ball from another
        /// </summary>
        private Color _color;
        
        /// <summary>
        /// The texture to apply to a ball
        /// </summary>
        private Texture2D _texture;

        /// <summary>
        /// The position of the ball in the game world
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Constructs a new ball instance
        /// </summary>
        /// <param name="game">The game this ball belongs in</param>
        /// <param name="color">A color to distinguish this ball</param>
        public Ball(InputExampleGame game, Color color) 
        {
            this._game = game;
            this._color = color;           
        }

        /// <summary>
        /// Loads the ball's texture
        /// </summary>
        public void LoadContent()
        {
            _texture = _game.Content.Load<Texture2D>("basketball");
        }

        /// <summary>
        /// Draws the ball at its current position and with 
        /// its assigned color
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to render with</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_texture is null) return; // null reference check
                
            spriteBatch.Draw(_texture, Position, _color);
        }
    }
}