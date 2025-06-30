using System.Collections.Generic;
using UnityEngine;

public class WaveRider : MonoBehaviour
{
    [Header("Wave Targets")]
    [Tooltip("���� ���� ������Ʈ���� Transform�� ����Ʈ�� ��������.")]
    public List<Transform> targets = new List<Transform>();

    [Header("Wave Settings")]
    [Tooltip("���Ʒ� ��鸮�� �ӵ� (������ ���ļ�)")]
    public float waveSpeed = 2f;
    [Tooltip("�ִ� ��鸲 ���� (����)")]
    public float waveAmplitude = 0.5f;
    [Tooltip("������Ʈ�� ����(Phase) ����. true�� �ε������� �ڵ� Offset ����")]
    public bool usePhaseOffset = true;

    // ���ο��� ������ Y ��ġ�� ����ص� �迭
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

            // ����(Phase) �߰�: �ε������� ��¦�� �о��ָ� ���� ���� UP!
            float phase = usePhaseOffset ? (i * Mathf.PI / targets.Count) : 0f;

            // �����Ŀ� ���� Y�� ������ ���
            float yOffset = Mathf.Sin(t + phase) * waveAmplitude;

            // ���� ��ġ�� �������� ������
            Vector3 pos = basePositions[i];
            pos.y += yOffset;
            targets[i].position = pos;
        }
    }
}