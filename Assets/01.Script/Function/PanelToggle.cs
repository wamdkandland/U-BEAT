using UnityEngine;

public class PanelToggle : MonoBehaviour
{
    public GameObject targetPanel; // �Ѱ� �� �г�

    public void TogglePanel()
    {
        if (targetPanel != null)
        {
            // ���� ���¿� �ݴ�� ����
            targetPanel.SetActive(!targetPanel.activeSelf);
        }
    }
}
