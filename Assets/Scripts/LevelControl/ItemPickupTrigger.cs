using UnityEngine;

public class ItemPickupTrigger : MonoBehaviour
{
    [Tooltip("������ �� LevelManager, ����� ��������� �� �������� �������")]
    public LevelManager levelManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ���������� LevelManager
            if (levelManager != null)
            {
                levelManager.OnItemPickedUp();
            }

            // ������ ������� ��������� � ����������
            gameObject.SetActive(false);
        }
    }
}
