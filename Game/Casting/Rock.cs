namespace Unit04.Game.Casting
{
    /// <summary>
    /// <para>An item of cultural or historical interest.</para>
    /// <para>
    /// The responsibility of an Artifact is to provide a message about itself.
    /// </para>
    /// </summary>
    public class Rock : FallingObject 
    {
        

        /// <summary>
        /// Constructs a new instance of an Artifact.
        /// </summary>
        public Rock()
        {
            _scoreValue = -1;
            velocity = new Point(0, 5);
            SetText("o");
        }

        
    }
}