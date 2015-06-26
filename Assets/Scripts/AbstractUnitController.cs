using UnityEngine;

namespace Assets.Scripts
{
    public class AbstractUnitController : MonoBehaviour
    {
        protected MovementController movementController;

        protected virtual void Awake()
        {
            movementController = GetComponent<MovementController>();
        } 
    }
}