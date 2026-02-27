using std = System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    /// <summary>
    /// Represents a ScriptableObject for storing the weights of different enemy types that can spawn when Pandora's Box is opened, and their respective difficulty levels.
    /// </summary>
    [CreateAssetMenu(fileName = "Pandora Weights", menuName = "ScriptableObjects/Pandora Weights", order = 1)]
    public class PandoraWeights : ScriptableObject
    {
        public enum EnemyType
        {
            None = 0,
            Trivial = 1,
            Easy = 2,
            Medium = 4,
            Hard = 8,
            Support = 16
        }

        public enum WeightLevel
        {
            VeryCommon = 4,
            Common = 3,
            Uncommon = 2,
            Rare = 1,
        }

        [std::Serializable]
        public struct EnemyWeight
        {
            public EnemyType Type;
            public WeightLevel Weight;
            public Enemy enemyPrefab;
        }

        [SerializeField] private EnemyWeight[] _enemyWeights;
        public IEnumerable<EnemyWeight> EnemyWeights => _enemyWeights;

        public IEnumerable<Enemy> GetRandomEnemies(int count, EnemyType blacklist)
        {
            if (_enemyWeights == null || _enemyWeights.Length == 0 || count <= 0)
                return new List<Enemy>();

            // Filter out blacklisted enemy types using bitwise check
            var validEnemies = _enemyWeights
                .Where(e => (e.Type & blacklist) == 0)
                .ToArray();

            if (validEnemies.Length == 0)
                return new List<Enemy>();

            List<Enemy> result = new List<Enemy>();

            // Calculate total weight of filtered enemies
            int totalWeight = validEnemies.Sum(e => (int)e.Weight);

            for (int i = 0; i < count; i++)
            {
                int randomValue = UnityEngine.Random.Range(0, totalWeight);
                int cumulative = 0;

                foreach (var enemyWeight in validEnemies)
                {
                    cumulative += (int)enemyWeight.Weight;

                    if (randomValue < cumulative)
                    {
                        result.Add(enemyWeight.enemyPrefab);
                        break;
                    }
                }
            }

            return result;
        }
    }

    
}