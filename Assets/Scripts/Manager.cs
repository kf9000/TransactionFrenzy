using System.Linq;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static float conveyorSpeed = 0.2f;
    public static float spawnSpeed = 2f;
    public static int score = 0;
    public static bool gameStarted = false;
    public static int highscore = 0;
    public ProductSpawner spawner;
    public TMP_Text scoreText;
    public TMP_Text highscoreText;
    public GameObject infoBox;
    public GameObject buttonHolder;
    public GameObject MainMenu;
    private float timer;
    private int lastScore = 0;
    private float defaultConvSpeed;
    private float defaultSpawnSpeed;
    GameObject[] barcodes;
    GameObject[] scannedCodes;

    void Awake()
    {
        defaultConvSpeed = conveyorSpeed;
        defaultSpawnSpeed = spawnSpeed;
    }

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

        IncreaseDifficulty(1.05f);
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

        highscoreText.text = highscore.ToString();
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

        var buttons = buttonHolder.transform.GetComponentsInChildren<Transform>();
        foreach(Transform buttonPart in buttons)
        {
            if(buttonPart.name == "Push")
            {
                buttonPart.transform.localPosition = new Vector3(0,0,0);
            }
        }
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

        conveyorSpeed = defaultConvSpeed;
        spawnSpeed = defaultSpawnSpeed;
    }

    public void IncreaseDifficulty(float amount)
    {
        if(score % 5 == 0)
        {
            if(score != lastScore)
            {
                conveyorSpeed *= amount;
                spawnSpeed /= amount;
                lastScore = score;
            }
        }
    }
}

