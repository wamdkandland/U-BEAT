using UnityEngine;

public class BarColorController : MonoBehaviour
{
    public GameObject[] bars;               // 막대기 오브젝트들
    public Material[] colorMaterials;       // 사용할 머티리얼 색상 리스트
    public float changeInterval = 0.2f;     // 색 전환 속도 (초)
    private float timer;

    private int offset = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= changeInterval)
        {
            timer = 0f;

            // 막대 배열 전체를 순차적으로 머티리얼 적용
            for (int i = 0; i < bars.Length; i++)
            {
                int matIndex = (i + offset) % colorMaterials.Length;
                bars[i].GetComponent<Renderer>().material = colorMaterials[matIndex];
            }

            offset = (offset + 1) % colorMaterials.Length;
        }
    }
}
