using UnityEngine;

public class OptionButton : MonoBehaviour
{
    public GameObject optionPanel; // ��Ÿ���� �� �г�

    public void OnOptionButtonClick()
    {
        optionPanel.SetActive(true); // �г��� ��
    }
}
