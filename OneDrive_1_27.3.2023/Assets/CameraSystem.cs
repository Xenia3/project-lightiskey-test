using Cinemachine;
using UnityEngine;
public class CameraSystem : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private bool useDragPan = true;
    [SerializeField] private bool useCameraMovement = false;
    private bool dragPanMoveActive;
    private Vector2 lastMousePosition;
    private float tragetFieldofView;

    private void Update()
    {
        if (useDragPan) { HandleCameraMovementDragScroll(); }

        if (useCameraMovement)
        {
            HandleCameraMovement();
        }

        HandleCameraRotation();
        HandleCameraZoom();
    }

    private void HandleCameraMovement()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
            inputDir.z = +1f;
        if (Input.GetKey(KeyCode.S))
            inputDir.z = -1f;
        if (Input.GetKey(KeyCode.A))
            inputDir.x = -1f;
        if (Input.GetKey(KeyCode.D))
            inputDir.x = +1f;
        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        float moveSpeed = 15f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleCameraMovementDragScroll()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);
        if (Input.GetMouseButtonDown(1))
        {
            dragPanMoveActive = true;
            lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1))
        { dragPanMoveActive = false; }

        if (dragPanMoveActive)
        {
            Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePosition;

            float dragPanSpeed = 2f;
            inputDir.x = mouseMovementDelta.x * dragPanSpeed;
            inputDir.z = mouseMovementDelta.y * dragPanSpeed;

            lastMousePosition = Input.mousePosition;
        }
        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        float moveSpeed = 15f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleCameraRotation()
    {
        float rotateDir = 0f;
        if (Input.GetKey(KeyCode.Q)) rotateDir = +1f;
        if (Input.GetKey(KeyCode.E)) rotateDir = -1f;

        float rotateSpeed = 100f;
        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);
    }
    private void HandleCameraZoom()
    {
        if (Input.mouseScrollDelta.y > 0) { cinemachineVirtualCamera.m_Lens.FieldOfView = 10; }
        
    }
}