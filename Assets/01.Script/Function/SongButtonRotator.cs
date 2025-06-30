using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SongButtonRotator : MonoBehaviour
{
    public List<RectTransform> songButtons; // 1, 2, 3번 버튼
    private List<Vector3> positions = new List<Vector3>(); // 원래 위치들

    public float moveDuration = 0.3f;

    void Start()
    {
        // 시작할 때 버튼들의 초기 위치 저장
        foreach (RectTransform btn in songButtons)
        {
            positions.Add(btn.anchoredPosition);
        }
    }

    // 4번 버튼이 눌렸을 때 호출
    public void OnRotateButtonClick()
    {
        // 위치를 오른쪽으로 밀기 (순환)
        Vector3 last = positions[positions.Count - 1];
        for (int i = positions.Count - 1; i > 0; i--)
        {
            positions[i] = positions[i - 1];
        }
        positions[0] = last;

        // 각 버튼을 새로운 위치로 이동
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
