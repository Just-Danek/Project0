using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateLiftController : MonoBehaviour
{
    [Header("��������� �����")]
    public float waitTime = 3f;               // ����� �������� � ����� ����� ��������� �����
    private bool playerInElevator = false;
    private float timer = 0f;
    int l = StaticHolder.CurrentLevel + 1;


    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && StaticHolder.UpdateLevelEnd)
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
        //Debug.Log(l);
        if (playerInElevator)
        {
            timer += Time.deltaTime;

            if (timer >= waitTime)
            {
                
                
                LoadNextLevel();
            }
        }
    }

    void LoadNextLevel()
    {
        Debug.Log("�������� ����� �����");
        SceneManager.LoadScene(l);
        StaticHolder.CurrentLevel = l;
        StaticHolder.levelCheksComplete = false;
        StaticHolder.UpdateLevelEnd = false;
        StaticHolder.ItemPickedUp = false;
        StaticHolder.PropitalHealActive = true;
        StaticHolder.SandevistanActive = true;
    }
}
