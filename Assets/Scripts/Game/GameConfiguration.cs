using System;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Game settings/Game Configuration")]
    public class GameConfiguration : ScriptableObject
    {
        [SerializeField]
        private BonusConfiguration _bonusConfiguration;

        [SerializeField]
        private float _bonusAppearanceTime = 2f;
        [SerializeField]
        private float _characterSpeed = 60f;
        [SerializeField]
        private float _maxBonusesCount = 2f;

        public BonusConfiguration BonusConfiguration => _bonusConfiguration;
        public float BonusAppearanceTime => _bonusAppearanceTime;
        public float CharacterSpeed => _characterSpeed;
        public float MaxBonusesCount => _maxBonusesCount;
    }

    [Serializable]
    public class BonusConfiguration
    {
        public int MinPoints;
        public int MaxPoints;
        public float MinLifetime;
        public float MaxLifetime;
        public Gradient Gradient;
    }
}