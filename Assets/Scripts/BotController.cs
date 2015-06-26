using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class BotController : AbstractUnitController
    {
        [SerializeField] private Vector3 direction;
        private const float ActivateDistanse = 15;

        private Vector3 position;

        public bool IsFreezen
        {
            get { return Math.Abs(
                PlayerHealthController.Instance.transform.position.x 
                - transform.position.x) > ActivateDistanse; }
        }

        void FixedUpdate()
        {
            if (IsFreezen) return;
            
            if (position == transform.localPosition)
            {
                direction = -direction;
            }

            position = transform.localPosition;
            movementController.SetMove(direction.normalized);

        }
    }
}