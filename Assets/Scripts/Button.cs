using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ConfigurableJoint))]
public class Button : MonoBehaviour
{
    public float threshold = 0.1f;
    public float deadZone = 0.05f;

    private float timer = 0f;
    private bool hasEnoughPassed = false;
    public float waitTime = 1f;

    private bool isPressed;
    private Vector3 startPos;
    private ConfigurableJoint joint;
    public UnityEvent onPressed, onReleased;
    void Start()
    {
        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }

    void OnEnable()
    {
        timer = 0f;   
        hasEnoughPassed = false;     
    }
    void Update()
    {
        if (!hasEnoughPassed)
        {
            timer += Time.deltaTime;

            if(timer >= 1f)
            {
                hasEnoughPassed = true;
            }
        }
        else
        {
            if(!isPressed && GetValue() + threshold >= 1)
            {
                Pressed();
            }
            if(isPressed && GetValue() - threshold <= 0)
            {
                Released();
            }          
        }

    }

    private void Pressed()
    {
        isPressed = true;
        onPressed.Invoke();
    }

    private void Released()
    {
        isPressed = false;
        onReleased.Invoke();
    }


    private float GetValue()
    {
        var value = Vector3.Distance(startPos, transform.localPosition) / joint.linearLimit.limit;

        if(Math.Abs(value) < deadZone)
        {
            value = 0;
        }

        return Mathf.Clamp(value, -1f, 1f);
    }
}
