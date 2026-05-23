using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) transform.Translate(-0.1f, 0, 0);
        if (Input.GetKey(KeyCode.RightArrow)) transform.Translate(0.1f, 0, 0);
        if (Input.GetKey(KeyCode.UpArrow)) transform.Translate(0, 0, 0.1f);
        if (Input.GetKey(KeyCode.DownArrow)) transform.Translate(0, 0, -0.1f);
    }
}