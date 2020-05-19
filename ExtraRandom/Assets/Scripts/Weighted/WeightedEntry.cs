using System;
using UnityEngine;

namespace ExtraRandom.Weighted
{
    [Serializable]
    public class WeightedEntry<T>
    {
        [SerializeField] private int weight;
        [SerializeField] private T entry;
        
        private float _percentage;

        /// <summary>
        /// The likeliness that this entry gets chosen.
        /// </summary>
        public int Weight
        {
            get => weight;
            set => weight = value;
        }
        
        /// <summary>
        /// The entry.
        /// </summary>
        public T Entry
        {
            get => entry;
            set => entry = value;
        }

        /// <summary>
        /// The percentage based on the <see cref="Weight"/> and the <see cref="WeightedRandom{T}.CollectiveWeight"/>
        /// </summary>
        public float Percentage
        {
            get => _percentage;
            set => _percentage = value;
        }

        /// <summary>
        /// A new instance.
        /// </summary>
        /// <param name="weight">The likeliness that this entry should be chosen.</param>
        /// <param name="entry">The entry.</param>
        public WeightedEntry(int weight, T entry)
        {
            this.weight = weight;
            this.entry = entry;
        }

        public override string ToString()
        {
            return $"{entry} - [{weight}] with {Percentage}% chance";
        }
    }
}