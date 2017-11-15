using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    public GameObject restartButton, mainMenuButton;

    PlayerStorer playerStorer;
    AudioSource audio;
    public UnityEngine.UI.Image screenDarken;
    private float timeScaleEdit = 1;
    private bool slowDown = false;
    private bool showScreen = false;

    void Awake()
    {
        audio = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        playerStorer = GetComponent<PlayerStorer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (slowDown)
        {
            // Global timeScale
            if (timeScaleEdit > 0)
            {
                timeScaleEdit -= 0.007f;
                if (timeScaleEdit <= 0)
                {
                    timeScaleEdit = 0;
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = timeScaleEdit;
                }
            }
            else if (timeScaleEdit <= 0)
            {
                timeScaleEdit = 0;
                Time.timeScale = 0;
            }
            // Pitch slowdown
            if (!(audio.pitch <= 0))
            {
                audio.pitch -= 0.007f;
            }
            if (audio.pitch <= 0)
            {
                showScreen = true;
            }

            // Multiply Vectors by Time.unscaledTime for movement when Time.timeScale == 0

            /*
            GameObject cube = GameObject.Find("Cube").gameObject;
            cube.transform.Rotate(new Vector3(cube.transform.rotation.x + 10f, cube.transform.rotation.y + 10f, cube.transform.rotation.z + 10f) * Time.unscaledTime);
            */
        }
        if (showScreen)
        {
            // Pretty DEBUGGY right here / placeholder
            screenDarken.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(screenDarken.color.a, 255f, 0.01f));
            GameObject.Find("Game Over Text").GetComponent<UnityEngine.UI.Text>().color = new Color(255f, 255f, 255f, 255f);
            restartButton.SetActive(true);
            mainMenuButton.SetActive(true);
        }
    }

    void OnTock()
    {
        CheckIfAlive();
    }

    private void CheckIfAlive()
    {
        int alivePlayerNum = 0;

        for (int i = 0; i < playerStorer.players.Length; i++)
        {
            try
            {
                if (playerStorer.players[i].GetComponent<PlayerHealth>().GetHealth() > 0)
                {
                    alivePlayerNum++;
                }
            }
            catch
            {
                // Player is null
            }
        }

        if (alivePlayerNum <= 0)
        {
            Debug.Log("THAT'S A GAME OVER EVERYBODY");
            timeScaleEdit = Time.timeScale;
            slowDown = true;
            //showScreen = true;
        }
    }

    public void RestartRun()
    {
        Time.timeScale = 1f;
        slowDown = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1); // Loads "Test Scene"
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        slowDown = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0); // Loads "Menu"
    }
}
