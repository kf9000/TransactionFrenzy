using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    public Hand hand;

    [Header("Input Actions")]
    public InputActionReference gripAction;
    public InputActionReference triggerAction;
    public InputActionReference thumbAction;

    void OnEnable()
    {
        gripAction.action.Enable();
        triggerAction.action.Enable();
        thumbAction.action.Enable();
    }

    void OnDisable()
    {
        gripAction.action.Disable();
        triggerAction.action.Disable();
        thumbAction.action.Disable();
    }

    void Update()
    {
        if (hand == null) return;

        hand.SetGrip(gripAction.action.ReadValue<float>());
        hand.SetTrigger(triggerAction.action.ReadValue<float>());
        hand.SetThumb(thumbAction.action.ReadValue<float>());
    }
}
