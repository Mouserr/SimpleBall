using UnityEngine;

namespace Assets.Scripts
{
    public class BotHealthController : UnitHealthController
    {
        protected override string EnemyTag
        {
            get { return "Player"; }
        }

        protected override void OnCollideWithEnemy(GameObject enemy, bool horizontally)
        {
            if (!horizontally)
            {
                OnHit();
            }
        }
    }
}