using TMPro;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static float conveyorSpeed = 0.4f;
    public static float spawnSpeed = 1.4f;
    public static int score = 0;
    public static bool gameStarted = false;
    public static int highscore = 0;
    public ProductSpawner spawner;
    public TMP_Text scoreText;
    public GameObject infoBox;
    public GameObject buttonHolder;
    public GameObject MainMenu;
    private float timer;
    GameObject[] barcodes;
    GameObject[] scannedCodes;

    void Update()
    {
        if (gameStarted)
        {
            //Start spawner
            timer += Time.deltaTime;

            if(timer >= spawnSpeed)
            {
                spawner.SpawnProduct();
                timer = 0f;
            }

            //Handle score counter
            scoreText.text = "Score: " + score;
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        scoreText.gameObject.SetActive(true);
        infoBox.SetActive(false);
        buttonHolder.SetActive(false);
        MainMenu.SetActive(false);
    }

    public void ShowInfo()
    {
        scoreText.gameObject.SetActive(false);
        infoBox.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void ShowMainMenu()
    {
        scoreText.gameObject.SetActive(false);
        infoBox.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif  
    }

    public void ResetGameState()
    {
        if(highscore < score)
        {
            highscore = score;
        }
        score = 0;
        gameStarted = false;
        ShowMainMenu();
        buttonHolder.SetActive(true);

        barcodes = GameObject.FindGameObjectsWithTag("Barcode");
        scannedCodes = GameObject.FindGameObjectsWithTag("Scanned");

        foreach(GameObject barcode in barcodes)
        {
            Destroy(barcode.transform.parent.gameObject);
        }

        foreach(GameObject scannedCode in scannedCodes)
        {
            Destroy(scannedCode.transform.parent.gameObject);
        }
    }
}

