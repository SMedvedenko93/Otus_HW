using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour, IGameStartListener, IGamePauseListener, IGameResumeListener, IGameFixedUpdateListener
    {
        [SerializeField] private int initialCount = 50;
        
        [SerializeField] private Transform container;
        [SerializeField] private Bullet prefab;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private LevelBounds levelBounds;

        private readonly Queue<Bullet> bulletPool = new();
        private readonly HashSet<Bullet> activeBullets = new();
        private readonly List<Bullet> cache = new();

        void IGameStartListener.StartGame()
        {
            for (var i = 0; i < this.initialCount; i++)
            {
                var bullet = Instantiate(this.prefab, this.container);
                this.bulletPool.Enqueue(bullet);
            }
        }
        
        void IGameFixedUpdateListener.CustomFixedUpdate(float fixedDeltaTime)
        {
            this.cache.Clear();
            this.cache.AddRange(this.activeBullets);

            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var bullet = this.cache[i];
                if (!this.levelBounds.InBounds(bullet.transform.position))
                {
                    this.RemoveBullet(bullet);
                }
            }
        }

        public void CreateBulletByArgs(Args args)
        {
            if (this.bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(this.worldTransform);
            }
            else
            {
                bullet = Instantiate(this.prefab, this.worldTransform);
            }

            bullet.Initialize(args);


            if (this.activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += this.OnBulletCollision;
            }
        }
        
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            this.RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (this.activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= this.OnBulletCollision;
                bullet.transform.SetParent(this.container);
                this.bulletPool.Enqueue(bullet);
            }
        }

        void IGamePauseListener.PauseGame()
        {
            foreach (var activeBullet in activeBullets)
            {
                activeBullet.GetComponent<Bullet>().PauseGame();
            }
        }

        void IGameResumeListener.ResumeGame()
        {
            foreach (var activeBullet in activeBullets)
            {
                activeBullet.GetComponent<Bullet>().ResumeGame();
            }
        }

        public struct Args
        {
            public Vector2 position;
            public Vector2 velocity;
            public Color color;
            public int physicsLayer;
            public int damage;
            public bool isPlayer;
        }
    }
}