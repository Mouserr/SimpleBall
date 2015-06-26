using System;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public float Speed;
    public float JumpSpeed;
    public float MaxJumpTime;

    [SerializeField]
    private Rigidbody model;

    private int lastJumpId;
    private HashSet<GameObject> contactingPlatforms = new HashSet<GameObject>();

    private float jumpTime;

    public bool IsOnPlatform
    {
        get { return contactingPlatforms.Count > 0; }
    }

    public void StartMove(Vector3 direction)
    {
        model.AddForce(direction * Speed);
    }

    public void SetMove(Vector3 direction)
    {
        if (IsOnPlatform)
        {
            model.velocity = direction.normalized * Speed;
        }
    }

    public void Move(Vector3 direction)
    {
        model.velocity += direction.normalized * Speed;
    }

    public void Jump(int jumpId)
    {
        if (!IsOnPlatform && lastJumpId != jumpId) return;

        if (IsOnPlatform)
        {
            jumpTime = 0;
            lastJumpId = jumpId;
            contactingPlatforms.Clear();
        }

        if (jumpTime >= MaxJumpTime) return;
        model.velocity += Vector3.up*JumpSpeed;
        jumpTime += Time.fixedDeltaTime;
    }

    public void ContactBottom(GameObject other)
    {
        if (other.CompareTag("Platform") && !contactingPlatforms.Contains(other))
        {
            Vector3 delta = other.transform.position - transform.position;
           
            if (delta.y < 0 && Math.Abs(delta.x) <= 2*Math.Abs(delta.y))
            {
                contactingPlatforms.Add(other);
            }
        }
    }

    public void StopContactBottom(GameObject other)
    {
        if (other.CompareTag("Platform") && contactingPlatforms.Contains(other))
        {
            contactingPlatforms.Remove(other);
        }
    }

    private void FixedUpdate()
    {
        if (IsOnPlatform)
        {
          /*  if (Math.Abs(model.velocity.x) > 0.01)
            {
                model.AddForce(-model.velocity.x, 0, 0, ForceMode.VelocityChange);
            }
            if (Math.Abs(model.velocity.x) < 0.01 && Math.Abs(Math.Abs(model.velocity.x)) > float.Epsilon)
            {
                model.velocity = new Vector3(0, model.velocity.y, model.velocity.z);
            }*/
        }
        else
        {
            if (transform.position.y < -50)
            {
                model.Sleep();
            }
        }
    }
}
