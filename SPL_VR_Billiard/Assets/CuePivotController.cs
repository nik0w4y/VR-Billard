using UnityEngine;
using UnityEngine.InputSystem;

public class CuePivotControllerXR : MonoBehaviour
{
    [Header("References")]
    public Transform cue;              // Pool cue
    public Transform pivotPoint;       // Fixed point on table edge

    [Header("Rotation Settings")]
    public float rotationSpeed = 90f;
    public bool lockToPivot = true;

    [Header("XR Input")]
    [Tooltip("Bind this to RightHand / Thumbstick")]
    public InputActionProperty rotateAction;

    private Vector3 offset;

    void OnEnable()
    {
        if (rotateAction != null)
            rotateAction.action.Enable();
    }

    void OnDisable()
    {
        if (rotateAction != null)
            rotateAction.action.Disable();
    }

    void Start()
    {
        if (cue == null || pivotPoint == null)
        {
            Debug.LogError("Cue or PivotPoint not assigned.");
            enabled = false;
            return;
        }

        offset = cue.position - pivotPoint.position;
    }

    void Update()
    {
        if (!lockToPivot) return;

        // Keep cue fixed to pivot
        cue.position = pivotPoint.position + offset;

        // Read joystick input (X axis)
        Vector2 joystick = rotateAction.action.ReadValue<Vector2>();
        float rotationInput = joystick.x;

        // Rotate cue around pivot point
        cue.RotateAround(
            pivotPoint.position,
            Vector3.up,
            rotationInput * rotationSpeed * Time.deltaTime
        );
    }

    /// <summary>
    /// Snap cue to a new pivot point
    /// </summary>
    public void SetPivotPoint(Transform newPivot)
    {
        pivotPoint = newPivot;
        offset = cue.position - pivotPoint.position;
    }

    /// <summary>
    /// Enable or disable pivot locking
    /// </summary>
    public void SetLock(bool state)
    {
        lockToPivot = state;
    }
}
