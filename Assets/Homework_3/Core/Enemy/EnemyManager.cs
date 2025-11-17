using System.Collections.Generic;
using Zenject;

namespace ShootEmUpZenject
{
    public class EnemyManager : IFixedTickable
    {
        private readonly List<Enemy> _activeEnemies = new List<Enemy>();

        public void FixedTick()
        {
            for (int i = _activeEnemies.Count - 1; i >= 0; i--)
            {
                _activeEnemies[i].FixedTick();
            }
        }

        public void AddEnemy(Enemy enemy)
        {
            if (!_activeEnemies.Contains(enemy))
            {
                _activeEnemies.Add(enemy);
            }
        }

        public void RemoveEnemy(Enemy enemy)
        {
            _activeEnemies.Remove(enemy);
        }
    }
}