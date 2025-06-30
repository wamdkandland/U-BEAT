using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PanelRotator : MonoBehaviour
{
    public List<RectTransform> panels; // 3���� �г�
    private List<Vector3> positions = new List<Vector3>(); // ���� ��ġ��
    public float moveDuration = 0.3f; // �̵� �ð�

    void Start()
    {
        // ���� �� �� �г��� ���� ��ġ ����
        foreach (RectTransform panel in panels)
        {
            positions.Add(panel.anchoredPosition);
        }
    }

    public void OnNextButtonClick()
    {
        // ���� ��ġ ����Ʈ�� �� ĭ�� ���������� �̵�
        Vector3 lastPos = positions[positions.Count - 1];
        for (int i = positions.Count - 1; i > 0; i--)
        {
            positions[i] = positions[i - 1];
        }
        positions[0] = lastPos;

        // �� �г��� ���ο� ��ġ�� �̵� (�ڷ�ƾ ���)
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
