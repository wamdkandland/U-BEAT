using UnityEngine;

public class PanelToggle : MonoBehaviour
{
    public GameObject targetPanel; // 켜고 끌 패널

    public void TogglePanel()
    {
        if (targetPanel != null)
        {
            // 현재 상태와 반대로 설정
            targetPanel.SetActive(!targetPanel.activeSelf);
        }
    }
}
