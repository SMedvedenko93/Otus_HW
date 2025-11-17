using System.Collections.Generic;
using Zenject;

namespace ShootEmUpZenject
{
    public class BulletManager : IFixedTickable
    {
        private readonly List<Bullet> _activeBullets = new List<Bullet>();

        public void FixedTick()
        {
            for (int i = _activeBullets.Count - 1; i >= 0; i--)
            {
                _activeBullets[i].FixedTick();
            }
        }

        public void AddBullet(Bullet bullet)
        {
            if (!_activeBullets.Contains(bullet))
            {
                _activeBullets.Add(bullet);
            }
        }

        public void RemoveBullet(Bullet bullet)
        {
            _activeBullets.Remove(bullet);
        }
    }
}
