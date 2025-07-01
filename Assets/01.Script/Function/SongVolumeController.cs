using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SongVolumeController : MonoBehaviour
{
    public List<AudioSource> songAudioSources; // 조절할 AudioSource들
    public Slider volumeSlider;               // 설정창 슬라이더

    void Start()
    {
        // 슬라이더 값이 바뀌면 볼륨 변경
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);

        // 초기 슬라이더 값 = 첫 번째 곡의 볼륨 (또는 평균값)
        if (songAudioSources.Count > 0)
            volumeSlider.value = songAudioSources[0].volume;
    }

    public void OnVolumeChange(float value)
    {
        // 모든 AudioSource 볼륨 동기화
        foreach (AudioSource source in songAudioSources)
        {
            source.volume = value;
        }
    }
}
