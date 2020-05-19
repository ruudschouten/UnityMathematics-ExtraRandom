using ExtraRandom.Weighted;

namespace ExtraRandom.Interface
{
    public interface IWeightedRandom<T>
    {
        /// <summary>
        /// Return a random <see cref="WeightedEntry{T}"/>.  
        /// </summary>
        /// <returns>A Random <see cref="WeightedEntry{T}"/>.</returns>
        WeightedEntry<T> Next();
        
        /// <summary>
        /// Add a new <see cref="WeightedEntry{T}"/> to the entries.
        /// </summary>
        /// <param name="entry">The entry that should be added.</param>
        void Add(WeightedEntry<T> entry);
    }
}