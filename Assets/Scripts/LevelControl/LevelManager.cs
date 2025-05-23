using UnityEngine;
using UnityEngine.UI; // ��� ������ � UI, ���� ����� �����

public class LevelManager : MonoBehaviour
{
    [Header("��������� ������")]
    public GameObject[] enemies;      // ��� ����� �� ������
    public GameObject optionalItem;   // ������������ ������� ��� ������� (����� ���� null)
    [Header("UI ��������")]
    public Text progressText;         // ����� ��������� (������� � ����������)

    private bool itemPickedUp = false;
    private bool allEnemiesDefeated = false;

    void Start()
    {
        // �������������: ����� �������� UI � ������
        UpdateProgressUI();
    }

    void Update()
    {
        CheckMissionStatus();
    }

    void CheckMissionStatus()
    {
        // �������� ���� ������
        allEnemiesDefeated = AreAllEnemiesDefeated();

        // ���� ��� ������� ���������
        if (allEnemiesDefeated)
        {
            LevelCompleted();
        }
        if (itemPickedUp)
        {
            Debug.Log("������� ��������!");
            StaticHolder.ItemPickedUp = true;
        }

        // ��������� �������� �� UI
        UpdateProgressUI();
    }

    bool AreAllEnemiesDefeated()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null) // ���� ���� ��� ����������
            {
                return false;
            }
        }
        return true;
    }

    public void OnItemPickedUp()
    {
        itemPickedUp = true;
    }

    void UpdateProgressUI()
    {
        if (progressText != null)
        {
            string progress = "";

            if (allEnemiesDefeated)
                progress += "����� ����������! ";
            else
                progress += "���������� ���� ������. ";

            if (optionalItem != null)
            {
                if (itemPickedUp)
                    progress += "������� ��������!";
                else
                    progress += "������� �������.";
            }

            progressText.text = progress;
        }
    }

    void LevelCompleted()
    {
        Debug.Log("������� �������!");
        StaticHolder.levelCheksComplete = true;
        // ����� ������ ���� ����� ���������� ������
        // ��������: GameManager.Instance.CompleteLevel();
    }
}

