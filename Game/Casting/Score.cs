namespace Greed.Game.Casting
{
    /// <summary>
    /// <para>An item of cultural or historical interest.</para>
    /// <para>
    /// The responsibility of an Artifact is to provide a message about itself.
    /// </para>
    /// </summary>
    public class Score : Actor
    {
        private int score = 0;

        /// <summary>
        /// Constructs a new instance of an Artifact.
        /// </summary>
        public Score()
        {
            
        }

        public string GetScoreMessage() 
        {
            return $"Score: {score}";
        }

        public void HitObject(int increment)
        {
            this.score += increment;
        }
        public void HitGem()
        {
            this.score += 1;
            SetText($"Score: {score}");
        }

        public void HitRock()
        {
            this.score -= 1;
            SetText($"Score: {score}");
        }
        
    }
}