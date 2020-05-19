using System;
using System.Collections.Generic;
using System.Linq;
using ExtraRandom.Interface;
using UnityEngine;

namespace ExtraRandom.Weighted
{
    /// <summary>
    /// Weighted Random Generator which will return entries based on their weight.
    /// </summary>
    /// <typeparam name="T">What type of entries this should contain.</typeparam>
    [Serializable]
    public class WeightedRandom<T> : IWeightedRandom<T>
    {
        // Made a SerializeField so it can be used in Unity.
        [SerializeField] private List<WeightedEntry<T>> entries;
        [Space] [SerializeField] private bool calculateWeight = true;
        [SerializeField] private bool calculatePercentages = true;
        [SerializeField] private bool sortEntries = true;

        public List<WeightedEntry<T>> Entries => entries;

        /// <summary>
        /// Retrieve <see cref="_collectiveWeight"/>.
        /// If it hasn't been calculated yet, or <see cref="calculateWeight"/> is set to true,
        /// it will calculate the weight.
        /// </summary>
        public int CollectiveWeight
        {
            get
            {
                if (_collectiveWeight == 0 || calculateWeight)
                {
                    _collectiveWeight = CalculateCollectiveWeight();
                    calculateWeight = false;
                }

                return _collectiveWeight;
            }
        }

        private int _collectiveWeight;
        private RegularRandom _random;

        # region Constructors

        /// <summary>
        /// Create a new <see cref="WeightedRandom{T}"/> instance with a random seed
        /// for the <see cref="RegularRandom"/> instance, which is used for rolls.
        /// </summary>
        public WeightedRandom()
        {
            _random = new RegularRandom();
        }

        /// <summary>
        /// Create a new <see cref="WeightedRandom{T}"/> instance with predetermined entries,
        /// and a random seed for the <see cref="RegularRandom"/> instance, which is used for rolls.
        /// <param name="entries">The entries that can be rolled for.</param>
        /// </summary>
        public WeightedRandom(List<WeightedEntry<T>> entries) : this()
        {
            this.entries = entries;
            _collectiveWeight = CalculateCollectiveWeight();
        }

        /// <summary>
        /// Create a new <see cref="WeightedRandom{T}"/> instance with the given <paramref name="seed"/>
        /// for the <see cref="RegularRandom"/> instance, which is used for rolls.
        /// <param name="seed">
        /// The seed that should be used to generate random numbers for <see cref="RegularRandom"/>
        /// </param>
        /// </summary>
        public WeightedRandom(uint seed)
        {
            _random = new RegularRandom(seed);
            entries = new List<WeightedEntry<T>>();
        }

        /// <summary>
        /// Create a new <see cref="WeightedRandom{T}"/> instance with predetermined entries 
        /// with the given <paramref name="seed"/> for the <see cref="RegularRandom"/> instance, which is used for rolls.
        /// </summary>
        /// <param name="seed">
        /// The seed that should be used to generate random numbers for <see cref="RegularRandom"/>
        /// </param>
        /// <param name="entries">The entries that can be rolled for.</param>
        public WeightedRandom(uint seed, List<WeightedEntry<T>> entries) : this(seed)
        {
            this.entries = entries;
            _collectiveWeight = CalculateCollectiveWeight();
        }

        # endregion

        /// <summary>
        /// Calculate the collective weight of all the entries.
        /// </summary>
        /// <returns>The sum of all entries' weight.</returns>
        private int CalculateCollectiveWeight()
        {
            return entries.Sum(entry => entry.Weight);
        }

        /// <summary>
        /// Calculate the percentages of each entry.
        /// </summary>
        private void CalculatePercentages()
        {
            if (!calculatePercentages)
            {
                return;
            }

            foreach (var entry in entries)
            {
                entry.Percentage = ((float) entry.Weight / CollectiveWeight) * 100;
            }

            calculatePercentages = false;
        }

        /// <summary>
        /// Sort the entries based on their percentages.
        /// </summary>
        private void SortEntries()
        {
            if (!sortEntries)
            {
                return;
            }

            entries = entries.OrderByDescending(entry => entry.Percentage).ToList();
            sortEntries = false;
        }

        /// <inheritdoc cref="IWeightedRandom{T}.Add"/>
        public void Add(WeightedEntry<T> entry)
        {
            _collectiveWeight += entry.Weight;
            entries.Add(entry);

            calculateWeight = true;
            calculatePercentages = true;
            sortEntries = true;
        }

        /// <inheritdoc cref="IWeightedRandom{T}.Next"/>
        public WeightedEntry<T> Next()
        {
            // Calculate percentages
            CalculatePercentages();

            // Sort the entries based on their percentages.
            SortEntries();

            var roll = _random.NextInt(100);
            Debug.Log($"Roll was {roll}/{100}");

            var previousPercentage = 0f;

            // Loop over collection and check if the roll was between the previous roll and this roll.
            foreach (var entry in entries)
            {
                var currentPercentage = entry.Percentage + previousPercentage;
                if (InBetween(previousPercentage, currentPercentage, roll))
                {
                    Debug.Log($"Roll was between {previousPercentage} and {currentPercentage}");
                    return entry;
                }

                Debug.Log($"Roll wasn't between {previousPercentage} and {currentPercentage}");

                previousPercentage += entry.Percentage;
            }

            return null;
        }

        /// <summary>
        /// Checks if the <paramref name="value"/> is in between the
        /// <paramref name="start"/> and <paramref name="end"/> values.
        /// </summary>
        /// <param name="start">The lower value to check against <paramref name="value"/>.</param>
        /// <param name="end">The higher value to check against <paramref name="value"/>.</param>
        /// <param name="value">
        /// The value to check for against <paramref name="start"/> and <paramref name="end"/>
        /// </param>
        /// <returns>
        /// True if <paramref name="value"/> is in between <paramref name="start"/> and <paramref name="end"/>.
        /// False otherwise.
        /// </returns>
        private bool InBetween(float start, float end, float value)
        {
            return value >= start && value <= end;
        }
    }
}