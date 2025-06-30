using UnityEngine;

public class PanelToggler : MonoBehaviour
{
    public GameObject targetPanel;

    public void TogglePanel()
    {
        bool isActive = targetPanel.activeSelf;
        targetPanel.SetActive(!isActive);
    }
}
