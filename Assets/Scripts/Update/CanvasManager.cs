using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public Canvas[] excludedCanvases; // ����� ��� �� 2 Canvas, ������� ������ ���������

    public void DisableAllCanvasesExceptExcluded()
    {
        Canvas[] allCanvases = FindObjectsOfType<Canvas>();

        foreach (Canvas canvas in allCanvases)
        {
            bool isExcluded = false;

            foreach (Canvas excluded in excludedCanvases)
            {
                if (canvas == excluded)
                {
                    isExcluded = true;
                    break;
                }
            }

            if (!isExcluded)
            {
                canvas.gameObject.SetActive(false);
            }
        }
    }
}
