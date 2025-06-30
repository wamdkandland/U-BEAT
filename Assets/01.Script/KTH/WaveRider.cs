using System.Collections.Generic;
using UnityEngine;

public class WaveRider : MonoBehaviour
{
    [Header("Wave Targets")]
    [Tooltip("흔들고 싶은 오브젝트들의 Transform을 리스트에 담으세요.")]
    public List<Transform> targets = new List<Transform>();

    [Header("Wave Settings")]
    [Tooltip("위아래 흔들리는 속도 (사인파 주파수)")]
    public float waveSpeed = 2f;
    [Tooltip("최대 흔들림 높이 (진폭)")]
    public float waveAmplitude = 0.5f;
    [Tooltip("오브젝트별 위상(Phase) 차이. true면 인덱스별로 자동 Offset 적용")]
    public bool usePhaseOffset = true;

    // 내부에서 원래의 Y 위치를 기억해둘 배열
    private Vector3[] basePositions;

    void Start()
    {
        basePositions = new Vector3[targets.Count];
        for (int i = 0; i < targets.Count; i++)
        {
            basePositions[i] = targets[i].position;
        }
    }

    void Update()
    {
        float t = Time.time * waveSpeed;

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null) continue;

            // 위상(Phase) 추가: 인덱스마다 살짝씩 밀어주면 물결 느낌 UP!
            float phase = usePhaseOffset ? (i * Mathf.PI / targets.Count) : 0f;

            // 사인파에 따라 Y축 오프셋 계산
            float yOffset = Mathf.Sin(t + phase) * waveAmplitude;

            // 원래 위치에 오프셋을 더해줌
            Vector3 pos = basePositions[i];
            pos.y += yOffset;
            targets[i].position = pos;
        }
    }
}