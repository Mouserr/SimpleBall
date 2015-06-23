using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// "Hey, where are you going again? Ok, ok, I'll move a bit..."
    /// </summary>
    public class LazySpectator : MonoBehaviour
    {
        public Transform WatchingTarget;

        public float MaxDelta;

        public Vector3 normalDirection = Vector3.back;

        private Vector3 startPosition;
        private Plane movementPlane;
        private float maxDeltaSqr;

        private void Awake()
        {
            movementPlane = new Plane(normalDirection, WatchingTarget.position);
            maxDeltaSqr = MaxDelta*MaxDelta;
        }

        private void Update()
        {
            float distance;
            Ray centerRay = camera.ScreenPointToRay(
                new Vector3(camera.pixelWidth/2f, camera.pixelHeight/2f, 0));
            if (!movementPlane.Raycast(centerRay, out distance))
            {
                Debug.LogError("Something wrong with camera! Can't raycast!");
                return;
            }

            Vector3 delta = WatchingTarget.position - centerRay.GetPoint(distance);
            if (delta.sqrMagnitude > maxDeltaSqr)
            {
                transform.Translate(delta * (delta.magnitude - MaxDelta));
            }
        }
    }
}