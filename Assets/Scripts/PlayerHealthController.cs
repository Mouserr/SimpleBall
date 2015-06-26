using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerHealthController : UnitHealthController
    {
        private static PlayerHealthController instance;
        public static PlayerHealthController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<PlayerHealthController>();
                }

                return instance;
            }
        }

        protected override string EnemyTag
        {
            get { return "Enemy"; }
        }

        protected override void OnCollideWithEnemy(GameObject enemy, bool horizontally)
        {
            if (horizontally)
            {
                OnHit();
            }
        }
    }
}