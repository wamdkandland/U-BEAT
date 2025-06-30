using UnityEngine;

public class OptionButton : MonoBehaviour
{
    public GameObject optionPanel; // 나타나게 할 패널

    public void OnOptionButtonClick()
    {
        optionPanel.SetActive(true); // 패널을 켬
    }
}
