using System;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class UnitHealthController : MonoBehaviour
    {
        public int Health = 1;

        protected abstract string EnemyTag { get; }

        protected abstract void OnCollideWithEnemy(GameObject enemy, bool horizontally);

        public void ContactWith(GameObject other)
        {
            if (other.CompareTag(EnemyTag))
            {
                Vector3 delta = other.transform.position - transform.position;
                Debug.Log("delta on collision " + delta);
                OnCollideWithEnemy(other, Math.Abs(delta.x) > Math.Abs(delta.y));
            }
        }

        protected virtual void OnHit()
        {
            Health--;
            Debug.Log("hitted", gameObject);
            if (Health <= 0)
            {
                OnDeath();
            }
        }

        protected virtual void OnDeath()
        {
            gameObject.SetActive(false);
        }
    }
}