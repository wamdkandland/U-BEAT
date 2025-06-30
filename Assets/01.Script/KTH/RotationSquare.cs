using UnityEngine;

public class RotationSquare : MonoBehaviour
{
    public float rotationSpeed = 45f; // 초당 45도 회전

    void Update()
    {
        // Z축 기준으로 회전
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

}
