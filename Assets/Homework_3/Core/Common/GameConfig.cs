using UnityEngine;

namespace ShootEmUpZenject
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Game/Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public int BulletPoolCount;
        public GameObject BulletPrefab;
    }
}