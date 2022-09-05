using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Tween_Library.Scripts;
using Tween_Library.Scripts.Effects;
public class BallPhysics : MonoBehaviour
{
    public ITransformEffect _transformEffect;
    public IColorTweenEffect _colorEffect;
    private YieldInstruction _wait;
    private MeshRenderer ballRenderer;

    [SerializeField] private float scaleSpeed = 1f;
    [SerializeField] private float waitTime;
    [SerializeField] private Color newColor;
    [SerializeField] private GameObject ball;
    
    [field: SerializeField] public Vector3 velocity {get; private set;}
    [SerializeField] private float drag;
    [SerializeField] private LayerMask bounceObjects;
    [SerializeField]private Transform ballVisual;
    private Transform ballPos;
    private Vector3 lastVelocity;
    private float xRotation;

    public GameObject hitParticle;

    private Coroutine previousColorCoroutine;
    private Coroutine previousTransformCoroutine;

    private void Awake()
    {
        ballRenderer = ballVisual.GetComponent<MeshRenderer>();
        ballPos = transform;
        _wait = new WaitForSeconds(waitTime);
        _colorEffect = new ColorChangeEffect(ballRenderer.material, newColor, ball, _wait);
        _transformEffect = new TransformEffect(ballPos, scaleSpeed, _wait);
    }
    
    void FixedUpdate()
    {
        ballPos.position = ballPos.position + velocity * Time.deltaTime;
        velocity = Vector3.Slerp(velocity, Vector3.zero, Time.deltaTime);
        lastVelocity = velocity;
        xRotation += velocity.magnitude * 2;

        float targetAngle = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
        

        Vector3 crossProduct = Vector3.Cross(velocity, Vector3.up).normalized;
        ballVisual.Rotate(crossProduct, -velocity.magnitude * 5, Space.World);
        Debug.DrawRay(ballPos.position,crossProduct * 2, Color.red, 1f);
    } 

    public void AddForce(Vector3 force)
    {
        velocity += force;
    }

    private void OnTriggerEnter(Collider other)
    {   
        Vector3 collisionPoint = other.ClosestPoint(ballPos.position);

        if (other.CompareTag("wall"))
        {
            Tween();
            
            Instantiate(hitParticle, collisionPoint, other.transform.rotation); 

            RaycastHit hit;
            Physics.Raycast(ballPos.position, collisionPoint - ballPos.position, out hit, 5, bounceObjects);
            
            Vector3 bounceDirection = Vector3.Reflect(velocity.normalized, hit.normal);
            bounceDirection.y = 0;
            velocity = bounceDirection * (lastVelocity.magnitude * 0.8f);

            Debug.DrawRay(ballPos.position, bounceDirection, Color.red, 1f);
        }
    }

    private void Tween()
    {
        StopAllCoroutines();
        if (velocity.magnitude > 0.7f)
        {
            if (previousColorCoroutine != null)
            {
                StopCoroutine(previousColorCoroutine);
            }
            if (previousTransformCoroutine != null)
            {
                StopCoroutine(previousTransformCoroutine);
            }
            previousColorCoroutine = StartCoroutine(_colorEffect.Execute());
            previousTransformCoroutine = StartCoroutine(_transformEffect.Execute(Vector3.one * 1.15f));
        }
    }
}
