using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float factorCameraSize;
    public GameObject panelGameOver;
    public GameObject buttonUI;
    bool gameOver;

    public TextMeshProUGUI textAcorn;
    int numAcorn;

    public void GameOver()
    {
        panelGameOver.SetActive(true);
        gameOver = true;
        Invoke("ActivateButtonUI", 1);
    }

    void ActivateButtonUI()
    {
        buttonUI.SetActive(true);
    }

    void Update()
    {
        if (gameOver)
        {
            Camera.main.orthographicSize -= Time.deltaTime * factorCameraSize;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3, 6);
        }
    }

    public void LoadScene(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }

    //Funcion public que va a ser llamada desde el script del player cuando este haga trigger sobre la coin
    public void AddAcorn()
    {
        numAcorn++; //sumo 1
        Debug.Log(numAcorn);
        //muestro por la interfaz:
        textAcorn.text = "" + numAcorn.ToString();
    }
}
