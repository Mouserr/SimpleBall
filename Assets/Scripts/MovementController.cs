using System;
using System.Net.NetworkInformation;
using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour
{

    public float Speed;
    public float JumpSpeed;
    public float MaxJumpTime;

    public float CurrentVelocity;

    private int lastJumpId;
    private bool isOnPlatform;
    private float jumpTime;
    
    public void Move(Vector3 direction)
    {
        rigidbody.velocity += direction.normalized * Speed;
    }

    public void Jump(int jumpId)
    {
        if (!isOnPlatform && lastJumpId != jumpId) return;

        if (isOnPlatform)
        {
            jumpTime = 0;
            isOnPlatform = false;
            lastJumpId = jumpId;
        }

        if (jumpTime >= MaxJumpTime) return;
        rigidbody.velocity += Vector3.up*JumpSpeed;
        jumpTime += Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.contacts.Length > 0 && col.contacts[0].point.y < transform.position.y)
        {
            isOnPlatform = true;
        }
    }

    private void FixedUpdate()
    {
        if (isOnPlatform)
        {
            if (Math.Abs(rigidbody.velocity.x) > 0.01)
            {
                rigidbody.AddForce(-rigidbody.velocity.x, 0, 0, ForceMode.VelocityChange);
            }
            if (Math.Abs(rigidbody.velocity.x) < 0.01 && Math.Abs(Math.Abs(rigidbody.velocity.x)) > float.Epsilon)
            {
                rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, rigidbody.velocity.z);
            }
        }
        else
        {
            if (transform.position.y < -50)
            {
                rigidbody.Sleep();
            }
        }
    }
}
