using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorController : MonoBehaviour
{
    [Header("��������� �����")]
    public float waitTime = 3f;               // ����� �������� � ����� ����� ��������� �����
    private bool playerInElevator = false;
    private float timer = 0f;
    public int CurrentBuildScene; //������� ����� �� �����

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInElevator = true;
            timer = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInElevator = false;
            timer = 0f;
        }
    }

    private void Update()
    {
        Debug.Log("������� ������� �� ����� - " + StaticHolder.CurrentLevel);
        if (playerInElevator && StaticHolder.levelCheksComplete)
        {
            timer += Time.deltaTime;

            if (timer >= waitTime)
            {
                LoadNextScene();
            }
        }
    }

    void LoadNextScene()
    {
        Debug.Log("�������� ����� �����");
        StaticHolder.CurrentLevel = CurrentBuildScene;
        StaticHolder.levelCheksComplete = false;
        SceneManager.LoadScene(1);


    }
}
