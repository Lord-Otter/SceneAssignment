using UnityEngine;
using UnityEngine.Splines;

public class CameraModeToggle : MonoBehaviour
{
    private MouseLookController mouseLook;
    private MovementController movement;
    private SplineAnimate splineAnimate;
    private Camera mainCamera;

    private Transform playerTransform;
    private GameObject flashLight;
    private GameObject trackingTarget;
    private SplineAnimate targetAnimate;

    private Vector3 lastPlayerPosition;
    private Quaternion lastPlayerRotation;
    private Quaternion lastCameraLocalRotation;

    private bool freeMovement = true;
    private float modeSwapCooldown;

    void Awake()
    {
        mouseLook = GetComponent<MouseLookController>();
        movement = GetComponent<MovementController>();
        splineAnimate = GetComponent<SplineAnimate>();
        mainCamera = GetComponentInChildren<Camera>();

        trackingTarget = GameObject.Find("TrackingTarget");
        targetAnimate = trackingTarget.GetComponent<SplineAnimate>();

        playerTransform = transform;

        flashLight = transform.Find("Head/Camera/flashlight low poly").gameObject;

        splineAnimate.enabled = false;
        targetAnimate.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ToggleCameraMode();
        }

        if (!freeMovement)
        {
            CameraTargetTracking();
        }
    }

    private void ToggleCameraMode()
    {
        freeMovement = !freeMovement;

        flashLight.SetActive(freeMovement);

        mouseLook.enabled = freeMovement;
        movement.enabled = freeMovement;
        splineAnimate.enabled = !freeMovement;
        targetAnimate.enabled = !freeMovement;

        if(freeMovement)
            mainCamera.transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    private void CameraTargetTracking()
    {
        if (trackingTarget == null) return;

        Vector3 direction = trackingTarget.transform.position - mainCamera.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        mainCamera.transform.rotation = targetRotation;
    }
}
