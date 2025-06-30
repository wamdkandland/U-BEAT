using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PanelRotator : MonoBehaviour
{
    public List<RectTransform> panels; // 3개의 패널
    private List<Vector3> positions = new List<Vector3>(); // 원래 위치들
    public float moveDuration = 0.3f; // 이동 시간

    void Start()
    {
        // 시작 시 각 패널의 현재 위치 저장
        foreach (RectTransform panel in panels)
        {
            positions.Add(panel.anchoredPosition);
        }
    }

    public void OnNextButtonClick()
    {
        // 현재 위치 리스트를 한 칸씩 오른쪽으로 이동
        Vector3 lastPos = positions[positions.Count - 1];
        for (int i = positions.Count - 1; i > 0; i--)
        {
            positions[i] = positions[i - 1];
        }
        positions[0] = lastPos;

        // 각 패널을 새로운 위치로 이동 (코루틴 사용)
        for (int i = 0; i < panels.Count; i++)
        {
            StopCoroutine("MovePanel");
            StartCoroutine(MovePanel(panels[i], positions[i]));
        }
    }

    IEnumerator MovePanel(RectTransform panel, Vector3 targetPos)
    {
        Vector3 start = panel.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            panel.anchoredPosition = Vector3.Lerp(start, targetPos, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        panel.anchoredPosition = targetPos;
    }
}
