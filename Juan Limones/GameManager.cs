using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public Image caughtImage;
    public Image wonImage;
    public float fadeDuration; //La duración del fade de la imagen (la imagen va a aparecer poco a poco)
    public float displayImageDuration; //El total de tiempo que voy a dejar la imagen en pantalla

    public bool isPlayerAtExit;
    public bool isPlayerCaught;

    public GameObject player;
    public AudioClip caughtClip;
    public AudioClip wonClip;

    public TextMeshProUGUI playerNameText;

    AudioSource audioS;
    bool restartLevel; //Me va a decir si voy a resetear el nivel o no
    float timer;

    void Start()
    {
        audioS = GetComponent<AudioSource>();
        LoadNameUser();
    }

    void LoadNameUser()
    {
        playerNameText.text = PlayerPrefs.GetString("NameUser");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            isPlayerAtExit = true;
    }

    void Update()
    {
        if (isPlayerCaught)
            EndLevel(caughtImage, true, caughtClip);
        else if (isPlayerAtExit)
            EndLevel(wonImage, false, wonClip);
    }

    void EndLevel(Image _image, bool restart, AudioClip _clip)
    {
        audioS.clip = _clip;
        if (!audioS.isPlaying)
            audioS.Play();

        timer += Time.deltaTime; //contador de tiempo

        //Aumentamos poco a poco el canal Alpha de la imagen
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, timer / fadeDuration);

        if(timer > fadeDuration + displayImageDuration)
        {
            if (restart)
                Debug.Log("El jugador ha perdido, recargar el nivel actual");
            else
                Debug.Log("Hemos ganao");
        }
    }
}
