using UnityEngine;

public class RotationSquare : MonoBehaviour
{
    public float rotationSpeed = 45f; // �ʴ� 45�� ȸ��

    void Update()
    {
        // Z�� �������� ȸ��
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

}
