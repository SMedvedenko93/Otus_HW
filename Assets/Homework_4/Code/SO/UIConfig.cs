using UnityEngine;

namespace MVx
{
    [CreateAssetMenu(menuName = "Game/Configs/UI Config")]
    public class UIConfig : ScriptableObject
    {
        [Header("Popups")]
        public GameObject[] popups;
    }
}