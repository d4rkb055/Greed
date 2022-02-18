namespace Unit04.Game.Casting
{
    /// <summary>
    /// <para>An item of cultural or historical interest.</para>
    /// <para>
    /// The responsibility of an Artifact is to provide a message about itself.
    /// </para>
    /// </summary>
    public class FallingObject : Actor
    {
        protected int _scoreValue = 0;

        /// <summary>
        /// Constructs a new instance of an Artifact.
        /// </summary>
        public FallingObject()
        {
        }

        /// <summary>
        /// Gets the artifact's message.
        /// </summary>
        /// <returns>The message.</returns>
        public int GetScoreValue()
        {
            return _scoreValue;
        }

        /// <summary>
        /// Sets the artifact's  to the given value.
        /// </summary>
        /// <param name="message">The given message.</param>
        public void SetScoreValue(int scoreValue)
        {
            _scoreValue = scoreValue;
        }
    }
}