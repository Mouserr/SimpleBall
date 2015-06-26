using UnityEngine;

namespace Assets.Scripts
{
    public class BottomCollisionTrigger : MonoBehaviour
    {
        private MovementController movementController;

        void Awake()
        {
            movementController = gameObject.GetComponentInParent<MovementController>();
            if (movementController == null)
            {
                Debug.LogError("Can't find MovementController!");
            }
        }

        private void OnTriggerEnter(Collider col)
        {
             movementController.ContactBottom(col.gameObject);
        }

        private void OnTriggerExit(Collider col)
        {
             movementController.StopContactBottom(col.gameObject);
        }

    }
}