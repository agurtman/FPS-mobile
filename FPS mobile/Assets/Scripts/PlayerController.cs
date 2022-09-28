using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject winWindow;
    [SerializeField] GameObject gameOver;
    [SerializeField] private int numOfLives;
    [SerializeField] private Image[] lives;
    [SerializeField] private Sprite full;
    [SerializeField] GameObject pauseUI;
    private int health;
    bool pause;
    int livesCount;

    Vector3 startPosition;

    public void ChangeHealth(int count)
    {
        numOfLives += count;

        if (health > numOfLives)
        {
            health = numOfLives;
        }

        for (int i = 0; i < lives.Length; i++)
        {
            if (i < health)
            {
                lives[i].sprite = full;
            }
            if (i < numOfLives)
            {
                lives[i].enabled = true;
            }
            else
            {
                lives[i].enabled = false;
            }
        }
    }

    public void Pause()
    {
        if (!pause)
        {
            GetComponent<PlayerLook>().enabled = false;
            pauseUI.SetActive(true);
            pause = true;
            var enemies = FindObjectsOfType<Enemy>();
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].enabled = false;
            }
        }
        else
        {
            GetComponent<PlayerLook>().enabled = true;
            pauseUI.SetActive(false);
            pause = false;
            var enemies = FindObjectsOfType<Enemy>();
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].enabled = true;
            }
        }
    }

    public void WinGame()
    {
        winWindow.SetActive(true);
        GetComponent<PlayerLook>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        GetComponent<PlayerLook>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    void Start()
    {
        startPosition = new Vector3(33.74f, 0.99f, 67.17f);
        livesCount = 0;
    }

    void Update()
    {

        if (numOfLives <= 0)
        {
            Debug.Log("Respawn");
            transform.position = startPosition;
            ChangeHealth(10);
            livesCount += 1;
        }
        if (livesCount >= 3)
        {
            GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Health")
        {
            ChangeHealth(5);
            Destroy(other.gameObject, 1);
        }
    }
}