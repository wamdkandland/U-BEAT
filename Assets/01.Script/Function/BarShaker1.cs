using UnityEngine;

public class BarShaker1 : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public int sampleSize = 1024;

    [Header("Shake Settings")]
    public float amplitude = 2f;
    public float speed = 1f;

    [Header("Movement Smoothing")]
    public float moveSmoothSpeed = 5f;

    [Header("Bars")]
    public Transform[] bars;

    float[] samples;
    Vector3[] basePos;
    float[] seeds;

    void Start()
    {
        samples = new float[sampleSize];
        int n = bars.Length;
        basePos = new Vector3[n];
        seeds = new float[n];

        for (int i = 0; i < n; i++)
        {
            basePos[i] = bars[i].localPosition;
            seeds[i] = Random.Range(0f, 100f);
        }
    }

    void Update()
    {
        //  [�߿�] audioSource�� ���� ������ ó�� �ߴ�
        if (audioSource == null || !audioSource.isPlaying)
            return;

        // 1) FFT ������ ��������
        audioSource.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);

        // 2) RMS ���
        float sum = 0f;
        for (int i = 0; i < sampleSize; i++)
            sum += samples[i] * samples[i];
        float rms = Mathf.Sqrt(sum / sampleSize);

        // 3) ����⺰ ��ġ ����
        for (int i = 0; i < bars.Length; i++)
        {
            float noise = Mathf.PerlinNoise(Time.time * speed + seeds[i], 0f);
            float offset = rms * noise * amplitude;

            Vector3 targetPos = basePos[i] + Vector3.up * offset;

            bars[i].localPosition = Vector3.Lerp(
                bars[i].localPosition,
                targetPos,
                Time.deltaTime * moveSmoothSpeed
            );
        }
    }
}
