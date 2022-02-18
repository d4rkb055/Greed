namespace Unit04.Game.Casting
{
    /// <summary>
    /// <para>An item of cultural or historical interest.</para>
    /// <para>
    /// The responsibility of an Artifact is to provide a message about itself.
    /// </para>
    /// </summary>
    public class ScoreBoard : FallingObject
    {
        private string message = "";

        /// <summary>
        /// Constructs a new instance of an Artifact.
        /// </summary>
        public ScoreBoard()
        {
        }


        /// <summary>
        /// Sets the artifact's message to the given value.
        /// </summary>
        /// <param name="message">The given message.</param>
        public void UpdateScore()
        {
            int score = GetScoreValue();
        }
    }
}