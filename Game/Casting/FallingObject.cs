using System;
namespace Greed.Game.Casting
{
    public class FallingObject : Actor
    {
        private int score;
        
        public FallingObject(int score)
        {            
            this.score = score;
        }
        public int GetScore()
        {
            return score;
        }
        public void SetScore(int score)
        {
            this.score = score;
        }
    }
}