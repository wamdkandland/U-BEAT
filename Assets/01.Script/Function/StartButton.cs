using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // ��ư���� ȣ���� �Լ�
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Main"); // "Main"�̶�� ������ �̵�
    }
}
