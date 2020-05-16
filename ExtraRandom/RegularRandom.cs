namespace ExtraRandom
{
    /// <summary>
    /// A basic wrapper class around Unity.Mathematics.Random
    /// </summary>
    public class RegularRandom : BaseRandom
    {
        /// <summary>
        /// Create a new <see cref="RegularRandom"/> instance with a random seed.
        /// </summary>
        public RegularRandom() : base()
        {
        }
        
        /// <summary>
        /// Create a new <see cref="RegularRandom"/> instance with the given <paramref name="seed"/>
        /// </summary>
        /// <param name="seed">The seed that should be used to generate random numbers.</param>
        public RegularRandom(uint seed) : base(seed)
        {
        }
        
        public override int NextInt(int min, int max)
        {
            return Random.NextInt(min, max);
        }

        public override float NextFloat(float min, float max)
        {
            return Random.NextFloat(min, max);
        }
    }
}