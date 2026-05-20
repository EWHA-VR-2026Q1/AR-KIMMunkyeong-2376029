using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnapObject : MonoBehaviour
{
    [SerializeField] private string snapPointTag = "SnapPoint";
    private bool hasHitSnapPoint = false;
    private Vector3 snapTargetPosition;

    void Update()
    {
        if (Pointer.current != null && Pointer.current.press.wasReleasedThisFrame)
        {
            CheckAndSnap();
        }
    }

    private void CheckAndSnap()
    {
        if (hasHitSnapPoint)
        {
            StartCoroutine(LerpToSnap(snapTargetPosition));
            hasHitSnapPoint = false;
        }
    }

    IEnumerator LerpToSnap(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.001f)
        {
            transform.position = Vector3.Lerp(transform.position, target, 0.1f);
            yield return null;
        }
        transform.position = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(snapPointTag))
        {
            hasHitSnapPoint = true;
            snapTargetPosition = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(snapPointTag))
        {
            hasHitSnapPoint = false;
        }
    }
}