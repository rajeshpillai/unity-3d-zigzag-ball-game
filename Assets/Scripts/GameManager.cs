using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gameOver;

    [SerializeField]
    private Light dayLight;

    private Camera mainCamera;

    private bool camColorLerp;

    private Color cameraColor;

    private Color[] tileColor_Day;
    private Color tileColor_Night;
    private int tileColor_Index;

    private Color tileTrueColor;

    private float timer;
    private float timerInterval = 5f;

    private float camLerpTimer;
    private float camLerpInterval = 1f;

    private int direction = 1;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        mainCamera = Camera.main;

        cameraColor = mainCamera.backgroundColor;

        // UNCOMMENT IT LATER

        tileColor_Index = 0;
        tileColor_Day = new Color[3];
        tileColor_Day[0] = new Color(10 / 256f, 139 / 256f, 203 / 256f);
        tileColor_Day[1] = new Color(10 / 256f, 200 / 256f, 20 / 256f);
        tileColor_Day[2] = new Color(220 / 256f, 170 / 256f, 45 / 256f);
        tileColor_Night = new Color(0, 8 / 256f, 11 / 256f);

    }

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        CheckLerpTimer();
    }

    // Update is called once per frame
    void Update()
    {
        CheckLerpTimer();
    }

    void CheckLerpTimer()
    {
        timer += Time.deltaTime;

        if (timer > timerInterval)
        {
            timer -= timerInterval;
            camColorLerp = true;
            camLerpTimer = 0f;
        }

        if (camColorLerp)
        {
            camLerpTimer += Time.deltaTime;
            float percent = camLerpTimer / camLerpInterval;

            if (direction == 1)
            {
                mainCamera.backgroundColor = Color.Lerp(cameraColor, Color.black, percent);
                //tileMat.color = Color.Lerp(tileColor_Day[tileColor_Index], tileColor_Night, percent);
                dayLight.intensity = 1f - percent;
            }
            else
            {
                mainCamera.backgroundColor = Color.Lerp(Color.black, cameraColor, percent);
                //tileMat.color = Color.Lerp(tileColor_Night, tileColor_Day[tileColor_Index], percent);
                dayLight.intensity = percent;
            }

            if (percent > 0.98f)
            {
                camLerpTimer = 1f;
                direction *= -1;
                camColorLerp = false;

                if (direction == -1)
                {
                    tileColor_Index = Random.Range(0, tileColor_Day.Length);
                }

            }

        }


    }

    // Custom function
    public void StartGame()
    {
        UIManager.instance.GameStart();
        ScoreManager.instance.StartScore();
        GameObject.Find("PlatformSpawner").GetComponent<PlatformSpawner>().StartSpawningPlatforms();
    }

    public void GameOver()
    {
        UIManager.instance.GameOver();
        ScoreManager.instance.StopScore();
        gameOver = true;
    }
}
