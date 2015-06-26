using UnityEngine;

namespace Assets.Scripts
{
    public class BodyCollisionTrigger : MonoBehaviour
    {
        private UnitHealthController healthController;

        void Awake()
        {
            healthController = gameObject.GetComponentInParent<UnitHealthController>();
            if (healthController == null)
            {
                Debug.LogError("Can't find MovementController!");
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            healthController.ContactWith(col.gameObject);
        }

        private void OnTriggerExit(Collider col)
        {
        }
    }
}