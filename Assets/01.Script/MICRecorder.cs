// ================================
// MICRecorder.cs (업데이트 버전)
// -------------------------------
// - 녹음 완료 후 로컬 Whisper STT 호출로 변경
// - WAV 저장은 옵션(디버그용)으로 유지
// ================================

using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class MICRecorder : MonoBehaviour
{
    [Header("Debug UI")]
    public TextMeshProUGUI debugText;

    [Header("STT Target")]
    public WhisperLocalSTT whisperLocalSTT; // 👉 Inspector에서 연결

    private int sampleRate = 16000;
    private int maxRecordTime = 5;

    private AudioClip recordedClip;
    private string micDevice;
    private bool isRecording = false;

    private AudioSource audioSource;
    private string wavPath;

    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        micDevice = Microphone.devices.Length > 0 ? Microphone.devices[0] : null;
#else
        micDevice = Microphone.devices.Length > 0 ? Microphone.devices[0] : null;
#endif
        if (micDevice == null)
        {
            Log("마이크를 찾을 수 없습니다.");
        }
        else
        {
            Log($"사용 마이크: {micDevice}");
        }

        wavPath = Path.Combine(Application.persistentDataPath, "mic_output.wav");
        audioSource = GetComponent<AudioSource>();
    }

    public void StartRecording()
    {
        if (isRecording || micDevice == null) return;

        recordedClip = Microphone.Start(micDevice, false, maxRecordTime, sampleRate);
        isRecording = true;
        Log("🎙️ 녹음 시작");
    }

    public void StopRecording()
    {
        if (!isRecording) return;

        Microphone.End(micDevice);
        isRecording = false;
        Log("🛑 녹음 종료");

        // (선택) WAV 파일로 저장 – 디버그·검수용
        SaveClipAsWav(recordedClip);

        // 🎯 로컬 Whisper STT 호출
        if (whisperLocalSTT != null)
        {
            whisperLocalSTT.TranscribeClip(recordedClip);
        }
        else
        {
            Log("⚠️ WhisperLocalSTT가 연결되지 않았습니다.");
        }
    }

    private void SaveClipAsWav(AudioClip clip)
    {
        if (clip == null) return;
        WaveUtility.Save(clip, wavPath);
        Log("💾 WAV 저장: " + wavPath);
    }

    public void PlaySavedWav()
    {
        if (!File.Exists(wavPath))
        {
            Log("WAV 파일이 없습니다: " + wavPath);
            return;
        }
        StartCoroutine(LoadAndPlayWav(wavPath));
    }

    private IEnumerator LoadAndPlayWav(string path)
    {
        using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + path, AudioType.WAV);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Log("WAV 로드 실패: " + www.error);
        }
        else
        {
            audioSource.clip = DownloadHandlerAudioClip.GetContent(www);
            audioSource.Play();
        }
    }

    private void Log(string msg)
    {
        if (debugText) debugText.text = msg;
        Debug.Log(msg);
    }
}
