using UnityEngine;

namespace ShootEmUpZenject
{
    public interface IMovable
    {
        void Move(Vector2 direction, float speed);
    }
}