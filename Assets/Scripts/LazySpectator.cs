using System;
using System.Collections;
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

        private Plane movementPlane;
        private Vector3 velocity;
        private IEnumerator movement;

        private void Awake()
        {
            movementPlane = new Plane(normalDirection, WatchingTarget.position);
        }

        private void Update()
        {
            if (movement != null) return;
            float distance;
            Ray centerRay = camera.ScreenPointToRay(
                new Vector3(camera.pixelWidth/2f, camera.pixelHeight/2f, 0));
            if (!movementPlane.Raycast(centerRay, out distance))
            {
                Debug.LogError("Something wrong with camera! Can't raycast!");
                return;
            }

            Vector3 delta = WatchingTarget.position - centerRay.GetPoint(distance);
            float absDelatX = Math.Abs(delta.x);
            if (absDelatX > MaxDelta)
            {
                movement = moveCoroutine(new Vector3(Mathf.Sign(delta.x)*MaxDelta/2, 0, 0), 0.3f);
                StartCoroutine(movement);
            }
        }

        private IEnumerator moveCoroutine(Vector3 shift, float duration)
        {
            Vector3 startPosition = transform.position;
            Vector3 destination = startPosition + shift;
            float time = 0;
            float timeScale = 1/duration;

            while ((transform.position - destination).sqrMagnitude > 0.01f)
            {
                time += Time.fixedDeltaTime * timeScale;
                transform.position = Vector3.Lerp(startPosition, destination, time);
                yield return null;
            }
            movement = null;
        }
    }
}