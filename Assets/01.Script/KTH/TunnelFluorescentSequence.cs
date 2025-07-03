using UnityEngine;
using System.Collections;

public class TunnelLightStepSequence : MonoBehaviour
{
    public Renderer[] lights;

    [ColorUsage(true, true)]
    public Color emissionColor = Color.white * 5f;
    public Color offColor = Color.black;

    public float[] stepDelays = new float[] { 1f, 0.8f, 0.6f, 0.4f, 0.2f };
    public float fastDelay = 0.1f;
    public float fastDuration = 2f;
    public float repeatInterval = 45f;

    private void Start()
    {
        StartCoroutine(RunLoop());
    }

    IEnumerator RunLoop()
    {
        while (true)
        {
            yield return StartCoroutine(StepSequence());
            yield return new WaitForSeconds(repeatInterval);
        }
    }

    IEnumerator StepSequence()
    {
        // 초기화
        SetAllEmission(offColor);

        // 점점 빨라지는 단계
        foreach (float delay in stepDelays)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                SetEmission(lights[i], emissionColor);
                yield return new WaitForSeconds(delay);
                SetEmission(lights[i], offColor);
            }
        }

        // 촤라라락 모드 (빠르게 왕복 반복)
        float elapsed = 0f;
        int index = 0;
        while (elapsed < fastDuration)
        {
            SetEmission(lights[index], emissionColor);
            yield return new WaitForSeconds(fastDelay);
            SetEmission(lights[index], offColor);

            index = (index + 1) % lights.Length;
            elapsed += fastDelay;
        }

        // 전부 끄기
        SetAllEmission(offColor);
    }

    void SetEmission(Renderer r, Color color)
    {
        Material mat = r.material;
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", color);
        DynamicGI.SetEmissive(r, color);
    }

    void SetAllEmission(Color color)
    {
        foreach (var r in lights)
            SetEmission(r, color);
    }
}
