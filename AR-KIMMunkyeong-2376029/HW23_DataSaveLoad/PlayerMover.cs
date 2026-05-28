using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5f;
    public float rotateSpeed = 180f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal"); // A/D
        float v = Input.GetAxis("Vertical");   // W/S

        // 이동
        Vector3 move = new Vector3(h, 0f, v).normalized;
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

        // 이동 방향으로 회전
        if (move.magnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
        }
    }
}