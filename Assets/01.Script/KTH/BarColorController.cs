using UnityEngine;

public class BarColorController : MonoBehaviour
{
    public GameObject[] bars;               // ����� ������Ʈ��
    public Material[] colorMaterials;       // ����� ��Ƽ���� ���� ����Ʈ
    public float changeInterval = 0.2f;     // �� ��ȯ �ӵ� (��)
    private float timer;

    private int offset = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= changeInterval)
        {
            timer = 0f;

            // ���� �迭 ��ü�� ���������� ��Ƽ���� ����
            for (int i = 0; i < bars.Length; i++)
            {
                int matIndex = (i + offset) % colorMaterials.Length;
                bars[i].GetComponent<Renderer>().material = colorMaterials[matIndex];
            }

            offset = (offset + 1) % colorMaterials.Length;
        }
    }
}
