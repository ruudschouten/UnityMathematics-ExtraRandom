using ExtraRandom.Types;

namespace ExtraRandom
{
    /// <summary>
    /// Random Number Generator which rolls a certain amount of times,
    /// and then decides on the highest or lowest roll.
    /// This is dependant on the <see cref="Bias"/> specified when creating a new instance.
    /// </summary>
    public class BiasedRandom : BaseRandom
    {
        /// <summary>
        /// What kind if Bias this generator will be using.
        /// </summary>
        private readonly Bias _bias = Bias.Lower;

        /// <summary>
        /// The amount of times a number should be generated before selecting the result.
        /// </summary>
        private readonly int _rollCount;

        /// <summary>
        /// Create a new <see cref="BiasedRandom"/> instance with the given <paramref name="seed"/>.
        /// Will roll 
        /// </summary>
        /// <param name="seed">The seed that should be used to generate random numbers.</param>
        /// <param name="rollCount">
        /// The amount of rolls that will be performed before deciding on a result when generating a random value.
        /// </param>
        public BiasedRandom(uint seed, int rollCount) : base(seed)
        {
            _rollCount = rollCount;
        }

        /// <summary>
        /// Create a new <see cref="BiasedRandom"/> instance with the given <paramref name="seed"/>.
        /// Will roll 
        /// </summary>
        /// <param name="seed">The seed that should be used to generate random numbers.</param>
        /// <param name="rollCount">
        /// The amount of rolls that will be performed before deciding on a result when generating a random value.
        /// </param>
        /// <param name="bias">The kind of <see cref="Bias"/> that this generator should use.</param>
        public BiasedRandom(uint seed, int rollCount, Bias bias) : this(seed, rollCount)
        {
            _bias = bias;
        }

        public override int NextInt(int min, int max)
        {
            return Roll(min, max);
        }

        public override float NextFloat(float min, float max)
        {
            return Roll(min, max);
        }

        /// <summary>
        /// Roll a few random number, the amount of rolls is based on <see cref="_rollCount"/>.
        /// After generating the numbers, the lowest or highest roll will be returned.
        /// This is based on the <see cref="_bias"/>
        /// </summary>
        /// <param name="min">The minimum value that can be rolled for.</param>
        /// <param name="max">The maximum value that can be rolled for.</param>
        /// <returns>The lowest or highest number that was rolled for.</returns>
        private int Roll(int min, int max)
        {
            // Set the default value to the highest possible number.
            var lowest = int.MaxValue;
            // Set the highest value to -1, since all rolls will be higher than 0.
            var highest = -1;
            for (var i = 0; i < _rollCount; i++)
            {
                var r = Random.NextInt(min, max);
                if (r < lowest)
                {
                    lowest = r;
                }

                if (r > highest)
                {
                    highest = r;
                }
            }

            // Return the result based on the bias.
            return _bias == Bias.Lower ? lowest : highest;
        }

        /// <inheritdoc cref="Roll(int,int)"/> 
        private float Roll(float min, float max)
        {
            // Set the default value to the highest possible number.
            var lowest = float.MaxValue;
            // Set the highest value to -1f, since all rolls will be higher than 0.
            var highest = -1f;
            for (var i = 0; i < _rollCount; i++)
            {
                var r = Random.NextFloat(min, max);
                if (r < lowest)
                {
                    lowest = r;
                }

                if (r > highest)
                {
                    highest = r;
                }
            }

            // Return the result based on the bias.
            return _bias == Bias.Lower ? lowest : highest;
        }
    }
}