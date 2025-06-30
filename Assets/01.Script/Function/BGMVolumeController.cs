using UnityEngine;
using UnityEngine.UI;

public class BGMVolumeController : MonoBehaviour
{
    public AudioSource bgmSource; // 배경음악 오디오 소스
    public Slider volumeSlider;   // UI 슬라이더

    void Start()
    {
        // 슬라이더가 바뀔 때마다 OnVolumeChange 호출되도록 연결
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);

        // 초기값 설정 (선택사항)
        volumeSlider.value = bgmSource.volume;
    }

    public void OnVolumeChange(float value)
    {
        bgmSource.volume = value;
    }
}
