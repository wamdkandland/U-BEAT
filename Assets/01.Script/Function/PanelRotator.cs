using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelRotator : MonoBehaviour
{
    public List<RectTransform> panels; // 3개의 패널
    private List<Vector3> basePositions = new List<Vector3>(); // 기준 위치 저장
    public float moveDuration = 0.3f;

    public PanelAudioController panelAudioController; // 음악 컨트롤러 연결

    void Start()
    {
        foreach (RectTransform panel in panels)
        {
            basePositions.Add(panel.anchoredPosition3D); // z값 포함
        }

        ApplyVisuals();

    }

    public void OnNextButtonClick()
    {
        // 패널 순서 회전
        RectTransform last = panels[panels.Count - 1];
        panels.RemoveAt(panels.Count - 1);
        panels.Insert(0, last);

        ApplyVisuals();

        // 이동 끝나고 음악 재생
        StartCoroutine(WaitAndPlayAudio());
    }

    void ApplyVisuals()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            RectTransform panel = panels[i];
            Vector3 targetPos = basePositions[i];
            Vector3 targetScale;

            if (i == 1)
            {
                targetPos.z = 100;
                targetScale = new Vector3(1.3f, 1.3f, 1f);
            }
            else
            {
                targetPos.z = 117;
                targetScale = new Vector3(0.8f, 0.8f, 1f);
            }

            StartCoroutine(MovePanel(panel, targetPos, targetScale));
        }
    }

    IEnumerator MovePanel(RectTransform panel, Vector3 targetPos, Vector3 targetScale)
    {
        Vector3 startPos = panel.anchoredPosition3D;
        Vector3 startScale = panel.localScale;

        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            float t = elapsed / moveDuration;
            panel.anchoredPosition3D = Vector3.Lerp(startPos, targetPos, t);
            panel.localScale = Vector3.Lerp(startScale, targetScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        panel.anchoredPosition3D = targetPos;
        panel.localScale = targetScale;
    }

    IEnumerator WaitAndPlayAudio()
    {
        yield return new WaitForSeconds(moveDuration);
        panelAudioController.CheckAndPlayAudio();
    }
}
