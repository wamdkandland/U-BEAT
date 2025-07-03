using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject songSelectCanvas;  // �� ���� ĵ����
    public GameObject scoreCheckCanvas;  // ���� Ȯ�� ĵ����

    // �� ���� ��ư ���� ��
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

    // �� â ��� ����
    public void CloseAll()
    {
        songSelectCanvas.SetActive(false);
        scoreCheckCanvas.SetActive(false);
    }
}
