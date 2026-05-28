using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 100f;

    private static ObjectMover selected; // 현재 선택된 오브젝트
    private Renderer rend;
    private Color originalColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null) originalColor = rend.material.color;
    }

    void OnMouseDown()
    {
        // 이전 선택 해제
        if (selected != null && selected != this)
            selected.Deselect();

        selected = this;
        Select();
    }

    void Update()
    {
        if (selected != this) return;

        // IJKL 키로 이동
        float h = 0f, v = 0f;
        if (Input.GetKey(KeyCode.I)) v = 1f;
        if (Input.GetKey(KeyCode.K)) v = -1f;
        if (Input.GetKey(KeyCode.J)) h = -1f;
        if (Input.GetKey(KeyCode.L)) h = 1f;

        Vector3 move = new Vector3(h, 0f, v) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);
    }

    void Select()
    {
        if (rend != null) rend.material.color = Color.yellow;
        Debug.Log($"[ObjectMover] {gameObject.name} 선택됨 (IJKL로 이동)");
    }

    void Deselect()
    {
        if (rend != null) rend.material.color = originalColor;
    }
}
