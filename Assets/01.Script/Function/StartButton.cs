using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // 버튼에서 호출할 함수
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Main"); // "Main"이라는 씬으로 이동
    }
}
