using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLookController : MonoBehaviour
{
    private Transform head;
    private Transform body;
    private InputAction lookAction;

    [SerializeField]
    private float mouseSpeed = 1.0f;

    [SerializeField, Range(0, 180)]
    private float verticalAngle = 160;
    
    void Start()
    {
        head = GetComponentInChildren<Camera>().transform;
        body = transform;
        lookAction = InputSystem.actions.FindAction("Look");
    }

    void Update()
    {
        Vector2 look = lookAction.ReadValue<Vector2>() * 0.1f;
        float hor = look.x;
        float ver = look.y;

        if (Mathf.Abs(hor) > float.Epsilon) {
            body.Rotate(Vector3.up, hor * mouseSpeed);
        }

        if (Mathf.Abs(ver) > float.Epsilon) {
            Quaternion rot = head.localRotation;
            Quaternion aim = Quaternion.AngleAxis(-0.5f * verticalAngle * Mathf.Sign(ver), Vector3.right);
            Quaternion delta = Quaternion.RotateTowards(rot, aim, Mathf.Sign(ver) * ver * mouseSpeed);
            head.localRotation = delta;
        }
    }
}
