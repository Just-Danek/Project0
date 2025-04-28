using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorController : MonoBehaviour
{
    [Header("��������� �����")]
    public float waitTime = 3f;               // ����� �������� � ����� ����� ��������� �����
    public string nextSceneName;              // ��� ��������� �����
    private bool playerInElevator = false;
    private float timer = 0f;

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
        if (playerInElevator)
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
        SceneManager.LoadScene(1);
    }
}
