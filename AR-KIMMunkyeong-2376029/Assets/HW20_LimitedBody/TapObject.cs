using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class TapObject : MonoBehaviour
{
    private Camera mainCamera;
    private Renderer cubeRenderer;

    private void Awake()
    {
        mainCamera = Camera.main;
        cubeRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Pointer.current != null && Pointer.current.press.wasPressedThisFrame)
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            Vector2 screenPosition = Pointer.current.position.ReadValue();
            Ray ray = mainCamera.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    // 랜덤 색상으로 변경
                    cubeRenderer.material.color = Random.ColorHSV();
                    Debug.Log("색깔 변경!");
                }
            }
        }
    }
}
