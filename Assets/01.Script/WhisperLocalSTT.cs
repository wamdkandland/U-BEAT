// ================================
// WhisperLocalSTT.cs
// -------------------------------
// 1) 로컬 whisper.unity 모델을 사용해 AudioClip을 텍스트로 변환한다.
// 2) MICRecorder(아래)에서 녹음이 끝나면 이 컴포넌트의 TranscribeClip()을 호출한다.
// ================================

using System.Collections;
using System.IO;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Whisper; // Macoron/whisper.unity 네임스페이스

public class WhisperLocalSTT : MonoBehaviour
{
    [Header("Whisper Settings")]
    [Tooltip("씬 어디엔가 존재하는 WhisperManager (InitOnAwake = true 권장)")]
    public WhisperManager manager;

    [Header("UI Reference")]
    public TextMeshProUGUI chatText;
    public TextMeshProUGUI debugText;

    /// <summary>
    /// MICRecorder 등에서 녹음한 AudioClip을 넘겨서 STT를 실행한다.
    /// </summary>
    public void TranscribeClip(AudioClip clip)
    {
        if (clip == null)
        {
            DebugLog("❌ AudioClip이 null 입니다.");
            return;
        }

        // WhisperManager가 준비됐는지 확인
        if (manager == null)
        {
            manager = FindAnyObjectByType<WhisperManager>();
            if (manager == null)
            {
                DebugLog("❌ WhisperManager를 찾을 수 없습니다.");
                return;
            }
        }

        // RunStream()은 내부적으로 비동기로 돌아가므로 코루틴 래핑 불필요 but UI 갱신 목적
        StartCoroutine(TranscribeRoutine(clip));
    }

    private IEnumerator TranscribeRoutine(AudioClip clip)
    {
        DebugLog("🔎 음성 인식 중...");

        bool done = false;
        string resultText = string.Empty;

        // 수정된 코드: WhisperResult의 Result 속성을 사용
        var task = manager.GetTextAsync(clip);
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsCompletedSuccessfully)
        {
            resultText = task.Result.Result; // 'Result' 속성을 사용하여 텍스트를 가져옴
            done = true;
        }
        else
        {
            DebugLog("❌ 음성 인식 중 오류 발생.");
            yield break;
        }

        // RunStream()은 다른 스레드에서 실행되므로 done 플래그 대기
        while (!done)
            yield return null;

        chatText.text = resultText;
        DebugLog("✅ 인식 완료: " + resultText);
    }

    private void DebugLog(string msg)
    {
        if (debugText) debugText.text = msg;
        Debug.Log(msg);
    }
}
