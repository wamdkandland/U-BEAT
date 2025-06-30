using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SongButtonRotator : MonoBehaviour
{
    public List<RectTransform> songButtons; // 1, 2, 3�� ��ư
    private List<Vector3> positions = new List<Vector3>(); // ���� ��ġ��

    public float moveDuration = 0.3f;

    void Start()
    {
        // ������ �� ��ư���� �ʱ� ��ġ ����
        foreach (RectTransform btn in songButtons)
        {
            positions.Add(btn.anchoredPosition);
        }
    }

    // 4�� ��ư�� ������ �� ȣ��
    public void OnRotateButtonClick()
    {
        // ��ġ�� ���������� �б� (��ȯ)
        Vector3 last = positions[positions.Count - 1];
        for (int i = positions.Count - 1; i > 0; i--)
        {
            positions[i] = positions[i - 1];
        }
        positions[0] = last;

        // �� ��ư�� ���ο� ��ġ�� �̵�
        for (int i = 0; i < songButtons.Count; i++)
        {
            StopCoroutine("MoveButton");
            StartCoroutine(MoveButton(songButtons[i], positions[i]));
        }
    }

    IEnumerator MoveButton(RectTransform button, Vector3 targetPos)
    {
        Vector3 start = button.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            button.anchoredPosition = Vector3.Lerp(start, targetPos, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        button.anchoredPosition = targetPos;
    }
}
