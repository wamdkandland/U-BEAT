using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SongVolumeController : MonoBehaviour
{
    public List<AudioSource> songAudioSources; // ������ AudioSource��
    public Slider volumeSlider;               // ����â �����̴�

    void Start()
    {
        // �����̴� ���� �ٲ�� ���� ����
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);

        // �ʱ� �����̴� �� = ù ��° ���� ���� (�Ǵ� ��հ�)
        if (songAudioSources.Count > 0)
            volumeSlider.value = songAudioSources[0].volume;
    }

    public void OnVolumeChange(float value)
    {
        // ��� AudioSource ���� ����ȭ
        foreach (AudioSource source in songAudioSources)
        {
            source.volume = value;
        }
    }
}
