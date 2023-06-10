using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Coins")]
    public TextMeshProUGUI textCoin;
    public int numCoins;

    [Header("Goblins")]
    public TextMeshProUGUI textGoblin;
    public int numGoblin;

    [Header("Game Over")]
    public GameObject panelGameOver;

    private void Awake()
    {
        SaveManager.Init();
    }

    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name != "Level_0") Load();
        textCoin.text = "" + numCoins.ToString();
    }

    #region Add Coin
    public void AddCoin() //Va a ser llamada desde el script del player cuando este haga trigger sobre la coin
    {
        numCoins++; //sumo 1
        Debug.Log(numCoins);
        //muestro por la interfaz:
        textCoin.text = "" + numCoins.ToString();
    }

    public void AddCoin3() 
    {
        numCoins += 3;
        Debug.Log(numCoins);
        //muestro por la interfaz:
        textCoin.text = "" + numCoins.ToString();
    }
    #endregion

    #region Add Goblin
    public void AddGoblin()
    {
        numGoblin++; //sumo 1
        Debug.Log(numGoblin);
        //muestro por la interfaz:
        textGoblin.text = numGoblin.ToString() + "/3";
    }
    #endregion

    #region Game Over
    public void GameOver()
    {
        Invoke("PanelGameOver", 3);
    }

    void PanelGameOver()
    {
        panelGameOver.SetActive(true);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    #endregion

    #region Load & Save
    public void Load()
    {
        string saveString = SaveManager.Load();
        if (saveString != null)
        {
            //Coger los objetos en formato Json y pasarlos a formato SaveObject
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
            numCoins = saveObject.saveNumCoins;
        }
    }

    public void Save()
    {
        SaveObject saveObject = new SaveObject
        {
            saveNumCoins = numCoins,
        };
        //Paso los datos que quiero guardar al formato Json
        string json = JsonUtility.ToJson(saveObject);
        //Escribo en el archivo la información que quiero guardar
        SaveManager.Save(json);
    }

    //Me creo una clase que contiene los tipos de datos que quiero guardar
    private class SaveObject
    {
        public int saveNumCoins;
    }
    #endregion
}