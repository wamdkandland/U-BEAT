using UnityEngine;

public class AudioPanelController : MonoBehaviour
{
    public Transform[] panels;              // 3���� �г�
    public BarShaker barShaker;             // �� ���� ��ũ��Ʈ
    public float targetZ = 100f;            // �߾� ���� Z��
    public float tolerance = 0.5f;          // ���� ��� ����

    void Update()
    {
        foreach (var panel in panels)
        {
            float z = panel.position.z;

            // Z ��ġ�� �߾��̸� �ش� AudioSource ����
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
