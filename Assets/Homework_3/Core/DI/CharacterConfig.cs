using UnityEngine;

namespace ShootEmUpZenject
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Game/Configs/CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        [Header("Base Stats")]
        public int BaseDamage;
        public int BaseHealth;
        public float BaseSpeed;
        public Vector3 SpawnPosition;
    }
}