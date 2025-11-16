using UnityEngine;

namespace ShootEmUpZenject
{
    public class CharacterMovement : IMovable
    {
        public Rigidbody2D _rigidbody2D;

        public CharacterMovement(Rigidbody2D rigidbody2D)
        {
            _rigidbody2D = rigidbody2D;
        }

        public void Move(Vector2 direction, float speed)
        {
            var nextPosition = _rigidbody2D.position + direction * speed;
            _rigidbody2D.MovePosition(nextPosition);
        }

    }
}