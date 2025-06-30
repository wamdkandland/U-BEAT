using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    public GameObject graphicsPanel;
    public GameObject audioPanel;
    public GameObject etcPanel;

    public void ShowGraphicsPanel()
    {
        graphicsPanel.SetActive(true);
        audioPanel.SetActive(false);
        etcPanel.SetActive(false);
    }

    public void ShowAudioPanel()
    {
        graphicsPanel.SetActive(false);
        audioPanel.SetActive(true);
        etcPanel.SetActive(false);
    }

    public void ShowEtcPanel()
    {
        graphicsPanel.SetActive(false);
        audioPanel.SetActive(false);
        etcPanel.SetActive(true);
    }
}
