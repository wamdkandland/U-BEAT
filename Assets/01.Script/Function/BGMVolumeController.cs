using UnityEngine;
using UnityEngine.UI;

public class BGMVolumeController : MonoBehaviour
{
    public AudioSource bgmSource; // ������� ����� �ҽ�
    public Slider volumeSlider;   // UI �����̴�

    void Start()
    {
        // �����̴��� �ٲ� ������ OnVolumeChange ȣ��ǵ��� ����
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);

        // �ʱⰪ ���� (���û���)
        volumeSlider.value = bgmSource.volume;
    }

    public void OnVolumeChange(float value)
    {
        bgmSource.volume = value;
    }
}
