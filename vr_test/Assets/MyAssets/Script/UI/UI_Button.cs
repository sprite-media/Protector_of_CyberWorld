using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Button : MonoBehaviour
{
    public enum Button
    {
        MainMenu,
        Resume,
        Exit,
        Start
    }

    public Button ButtonType;

    public void GoTomainMenu()
    {
        Debug.Log("MainMenu");
        SceneManager.LoadScene("MainMenu_woPlayer");
        GameObject.Find("Player").transform.position = new Vector3(0.28f, 0.157f, -0.81f);
    }
    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
    public void Resume()
    {
        Debug.Log("Resume");
        //Time.timeScale = 1.0f;
    }
    public void StartGame()
    {
        Debug.Log("GameStart");
        StartCoroutine("WaitForStart");

    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("tempMap");
        GameObject.Find("Player").transform.position = new Vector3(10.0f, 0.01f, 1.0f);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Hand")
        {
            switch ((int)ButtonType)
            {
                case (int)Button.MainMenu:
                    GoTomainMenu();
                    break;
                case (int)Button.Resume:
                    Exit();
                    break;
                case (int)Button.Exit:
                    Resume();
                    break;
                case (int)Button.Start:
                    StartGame();
                    break;
            }
        }
    }
}
