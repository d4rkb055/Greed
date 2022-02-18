using System;
namespace Unit04.Game.Casting
{
    /// <summary>
    /// <para>An item of cultural or historical interest.</para>
    /// <para>
    /// The responsibility of an Artifact is to provide a message about itself.
    /// </para>
    /// </summary>
    public class Gem : FallingObject 
    {
        
        /// <summary>
        /// Constructs a new instance of an Artifact.
        /// </summary>
        public Gem()
        {
            _scoreValue = 1;
            velocity = new Point(0, 5);
            SetText("*");
            SetFontSize(20);
            Random random = new Random();
                int r = random.Next(0, 256);
                int g = random.Next(0, 256);
                int b = random.Next(0, 256);
                Color color = new Color(r, g, b);
                SetColor(color);
        }

       
    }
}