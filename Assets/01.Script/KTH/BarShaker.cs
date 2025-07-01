using UnityEngine;

public class BarShaker : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public int sampleSize = 1024;

    [Header("Shake Settings")]
    public float amplitude = 2f;
    public float speed = 1f;

    [Header("Movement Smoothing")]
    public float moveSmoothSpeed = 5f;   // 보간 속도

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
        // 1) FFT 데이터 가져오기
        audioSource.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);

        // 2) RMS 계산
        float sum = 0f;
        for (int i = 0; i < sampleSize; i++)
            sum += samples[i] * samples[i];
        float rms = Mathf.Sqrt(sum / sampleSize);

        // 3) 막대기별 위치 보간
        for (int i = 0; i < bars.Length; i++)
        {
            // 노이즈 × RMS × 진폭
            float noise = Mathf.PerlinNoise(Time.time * speed + seeds[i], 0f);
            float offset = rms * noise * amplitude;

            // 목표 위치
            Vector3 targetPos = basePos[i] + Vector3.up * offset;

            // 보간 적용
            bars[i].localPosition = Vector3.Lerp(
                bars[i].localPosition,
                targetPos,
                Time.deltaTime * moveSmoothSpeed
            );
        }
    }
}