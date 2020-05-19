namespace ExtraRandom.Interface
{
    /// <summary>
    /// Interface which all Random Generator should implement.
    /// </summary>
    public interface IRandom
    {
        /// <summary>
        /// Generate a random <see cref="int"/> value.
        /// </summary>
        /// <returns>The generated number.</returns>
        int NextInt();

        /// <summary>
        /// Generate a random <see cref="int"/> value where the maximum value is specified.
        /// </summary>
        /// <param name="max">The maximum value the generated number can be.</param>
        /// <returns>The generated number.</returns>
        int NextInt(int max);

        /// <summary>
        /// Generate a random <see cref="int"/> value where the minimum and maximum value are specified.
        /// </summary>
        /// <param name="min">The minimum value the generated number can be.</param>
        /// <param name="max">The maximum value the generated number can be.</param>
        /// <returns>The generated number.</returns>
        int NextInt(int min, int max);

        /// <summary>
        /// Generate a random <see cref="float"/> value.
        /// </summary>
        /// <returns>The generated number.</returns>
        float NextFloat();

        /// <summary>
        /// Generate a random <see cref="float"/> value where the maximum value is specified.
        /// </summary>
        /// <param name="max">The maximum value the generated number can be.</param>
        /// <returns>The generated number.</returns>
        float NextFloat(float max);

        /// <summary>
        /// Generate a random <see cref="float"/> value where the minimum and maximum value are specified.
        /// </summary>
        /// <param name="min">The minimum value the generated number can be.</param>
        /// <param name="max">The maximum value the generated number can be.</param>
        /// <returns>The generated number.</returns>
        float NextFloat(float min, float max);
    }
}