using System;
using ExtraRandom.Interface;

namespace ExtraRandom
{
    /// <summary>
    /// Base Random Generation class to create other Random Generations of.
    /// </summary>
    public abstract class BaseRandom : IRandom
    {
        /// <summary>
        /// The Random Generation Generator by Unity.Mathematics.
        /// </summary>
        protected Unity.Mathematics.Random Random;

        /// <summary>
        /// Create a new <see cref="BaseRandom"/> instance with a random seed.
        /// </summary>
        protected BaseRandom()
        {
            Random = new Unity.Mathematics.Random((uint) DateTime.Now.Millisecond);
        }

        /// <summary>
        /// Create a new <see cref="BaseRandom"/> instance with the given <paramref name="seed"/>
        /// </summary>
        /// <param name="seed">The seed that should be used to generate random numbers.</param>
        protected BaseRandom(uint seed)
        {
            Random = new Unity.Mathematics.Random(seed);
        }

        /// <inheritdoc cref="IRandom.NextInt()"/>
        public int NextInt()
        {
            return NextInt(0, int.MaxValue);
        }

        /// <inheritdoc cref="IRandom.NextInt()"/>
        public int NextInt(int max)
        {
            return NextInt(0, max);
        }

        /// <inheritdoc cref="IRandom.NextInt()"/>
        public abstract int NextInt(int min, int max);

        /// <inheritdoc cref="IRandom.NextFloat()"/>
        public float NextFloat()
        {
            return NextFloat(0, float.MaxValue);
        }

        /// <inheritdoc cref="IRandom.NextFloat()"/>
        public float NextFloat(float max)
        {
            return NextFloat(0, max);
        }

        /// <inheritdoc cref="IRandom.NextFloat()"/>
        public abstract float NextFloat(float min, float max);
    }
}