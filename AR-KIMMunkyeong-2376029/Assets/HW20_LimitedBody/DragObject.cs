using UnityEngine;
using UnityEngine.InputSystem;

public class DragObject : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;
    private float zDistance;
    private Vector3 offset;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        var pointer = Pointer.current;
        if (pointer == null) return;

        // 누르기 시작
        if (pointer.press.wasPressedThisFrame)
        {
            StartDrag(pointer.position.ReadValue());
        }

        // 떼었을 때
        if (pointer.press.wasReleasedThisFrame)
        {
            isDragging = false;
        }

        // 드래그 중
        if (isDragging)
        {
            ExecuteDrag(pointer.position.ReadValue());
        }
    }

    private void StartDrag(Vector2 screenPos)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == transform)
            {
                isDragging = true;
                zDistance = mainCamera.WorldToScreenPoint(transform.position).z;

                Vector3 clickWorldPos = mainCamera.ScreenToWorldPoint(
                    new Vector3(screenPos.x, screenPos.y, zDistance));

                if (transform.parent != null)
                    offset = transform.localPosition - transform.parent.InverseTransformPoint(clickWorldPos);
                else
                    offset = transform.position - clickWorldPos;
            }
        }
    }

    private void ExecuteDrag(Vector2 screenPos)
    {
        Vector3 mousePoint = new Vector3(screenPos.x, screenPos.y, zDistance);
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(mousePoint);
        transform.position = new Vector3(newPosition.x + offset.x, transform.position.y, newPosition.z + offset.z);
    }
}