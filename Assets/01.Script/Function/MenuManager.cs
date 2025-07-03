using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject songSelectCanvas;  // 곡 선택 캔버스
    public GameObject scoreCheckCanvas;  // 점수 확인 캔버스

    // 곡 선택 버튼 누를 때
    public void OnClickSongSelect()
    {
        songSelectCanvas.SetActive(true);
        scoreCheckCanvas.SetActive(false);
    }

    public void OnClickScoreCheck()
    {
        songSelectCanvas.SetActive(false);
        scoreCheckCanvas.SetActive(true);
    }

    // 두 창 모두 끄기
    public void CloseAll()
    {
        songSelectCanvas.SetActive(false);
        scoreCheckCanvas.SetActive(false);
    }
}
