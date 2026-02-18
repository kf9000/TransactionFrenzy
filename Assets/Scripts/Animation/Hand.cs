using System;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    //Animation
    public float animationSpeed = 10f;
    Animator animator;
    private float gripTarget;
    private float thumbTarget;
    private float triggerTarget;
    private float gripCurrent;
    private float thumbcurrent;
    private float triggerCurrent;
    private const string gripName = "Grip";
    private const string thumbName = "Thumb";
    private const string triggerName = "Trigger";

    //Physics
    public GameObject followObject;
    public float followSpeed = 30f;
    public float rotateSpeed = 100f;
    public Vector3 positionOffset;
    public Vector3 rotationOffset;
    private Transform followTarget;
    private Rigidbody body;

    void Start()
    {
        animator = GetComponent<Animator>();

        followTarget = followObject.transform;
        body = GetComponent<Rigidbody>();
        body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.mass = 20f;

        body.position = followTarget.position;
        body.rotation = followTarget.rotation;
    }

    void Update()
    {
        AnimateHand();

        PhysicsMove();
        
    }

    private void PhysicsMove()
    {
        var positionWithOffset = followTarget.position + positionOffset;
        var distance = Vector3.Distance(positionWithOffset, transform.position);
        body.linearVelocity = (positionWithOffset - transform.position).normalized * (followSpeed * distance);

        var rotationWithOffset = followTarget.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        if (Mathf.Abs(axis.magnitude) != Mathf.Infinity)
        {
            if (angle > 180.0f) { angle -= 360.0f; }
            body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);
        }
    }

    internal void SetGrip(float readValue)
    {
        gripTarget = readValue;
    }

    internal void SetThumb(float readValue)
    {
        thumbTarget = readValue;
    }

    internal void SetTrigger(float readValue)
    {
        triggerTarget = readValue;
    }

    void AnimateHand()
    {
        if(gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(gripName, gripCurrent);
        }
        if(thumbcurrent != thumbTarget)
        {
            thumbcurrent = Mathf.MoveTowards(thumbcurrent, thumbTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(thumbName, thumbcurrent);
        }
        if(triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(triggerName, triggerCurrent);
        }
    }
}
