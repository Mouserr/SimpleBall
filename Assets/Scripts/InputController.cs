using UnityEngine;

namespace Assets.Scripts
{
    public class InputController : AbstractUnitController
    {
        private bool jumpInited;

        private int pressID = 0;

        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                pressID++;
                pressID %= int.MaxValue - 10;
                movementController.Jump(pressID);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                movementController.Jump(pressID);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow))
            {
                movementController.Move(Vector3.left);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                movementController.Move(Vector3.right);
            }
        }
    }
}