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
        //  [중요] audioSource가 아직 없으면 처리 중단
        if (audioSource == null || !audioSource.isPlaying)
            return;

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
