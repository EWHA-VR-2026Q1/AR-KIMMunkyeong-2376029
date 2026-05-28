using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    [Header("따라갈 대상")]
    public Transform target;

    [Header("카메라 설정")]
    public float height = 15f;        // 위에서 내려다보는 높이
    public float smoothSpeed = 8f;    // 부드러운 이동 속도
    public Vector3 offset = Vector3.zero; // 추가 오프셋 (선택)

    void LateUpdate()
    {
        if (target == null) return;

        // 탑다운: target 위 height 만큼 올라간 위치
        Vector3 desiredPos = new Vector3(target.position.x + offset.x,
                                         height,
                                         target.position.z + offset.z);

        // 부드럽게 따라가기
        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

        // 항상 아래를 바라보도록 고정
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
}
