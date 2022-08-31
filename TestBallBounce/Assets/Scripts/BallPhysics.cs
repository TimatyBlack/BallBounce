using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    [field: SerializeField] public Vector3 velocity {get; private set;}
    [SerializeField] private float drag;
    [SerializeField] private LayerMask bounceObjects;
    private Transform ball;
    private Vector3 lastVelocity;
    private float xRotation;
    // Start is called before the first frame update
    void Start()
    {
        ball = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ball.position = ball.position + velocity * Time.deltaTime;
        velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime);
        lastVelocity = velocity;
        xRotation += velocity.magnitude * 2;

        Quaternion forwardRotation = Quaternion.LookRotation(velocity);
        float targetAngle = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
        

        Vector3 crossProduct = Vector3.Cross(velocity, Vector3.up).normalized;
        ball.Rotate(crossProduct, -velocity.magnitude * 5, Space.World);
        Debug.DrawRay(ball.position,crossProduct * 2, Color.red, 1f);
    } 

    public void AddForce(Vector3 force)
    {
        velocity += force;
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 collisionPoint = other.ClosestPoint(ball.position);
        RaycastHit hit;
        Physics.Raycast(ball.position, collisionPoint - ball.position, out hit, 5, bounceObjects);
        Vector3 bounceDirection = Vector3.Reflect(velocity.normalized, hit.normal);
        bounceDirection.y = 0;
        velocity = bounceDirection * lastVelocity.magnitude * 0.8f;
        Debug.Log("trigger");
        Debug.DrawRay(ball.position, bounceDirection, Color.red, 1f);
    }

    private void OnTriggerStay(Collider other)
    {   
        Vector3 collisionPoint = other.ClosestPoint(ball.position);
        collisionPoint.y = ball.position.y;
        Vector3 collisionDirection = ball.position - collisionPoint;
        AddForce(collisionDirection);
    }
}
