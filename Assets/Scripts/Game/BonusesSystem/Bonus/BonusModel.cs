using Common;
using UnityEngine;

namespace Game.BonusesSystem.Bonus
{
    public class BonusModel : IModel
    {
        public int Points { get; set; }
        public float Lifetime { get; set; }
        public Vector3 Position { get; set; }
    }
}