using UnityEngine;

public class AudioPanelController : MonoBehaviour
{
    public Transform[] panels;              // 3개의 패널
    public BarShaker barShaker;             // 바 흔들기 스크립트
    public float targetZ = 100f;            // 중앙 기준 Z값
    public float tolerance = 0.5f;          // 오차 허용 범위

    void Update()
    {
        foreach (var panel in panels)
        {
            float z = panel.position.z;

            // Z 위치가 중앙이면 해당 AudioSource 전달
            if (Mathf.Abs(z - targetZ) < tolerance)
            {
                AudioSource source = panel.GetComponent<AudioSource>();
                if (barShaker.audioSource != source)
                {
                    barShaker.audioSource = source;
                }
            }
        }
    }
}
