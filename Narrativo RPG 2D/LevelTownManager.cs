using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTownManager : MonoBehaviour
{
    public static LevelTownManager instance;
    public Dialogue dialogueBoy;

    public string[] newExitDialogue;

    void Start()
    {
        instance = this;
    }

    public void OpenChest()
    {
        dialogueBoy.dialogue = newExitDialogue;
        dialogueBoy.dialogue2 = newExitDialogue;

        //Coger la posición del NPC y hacer que se mueva
        //Alternativa: dialogueBoy.transform.position = dialogueBoy.transform.position + dialogueBoy.transform.right;
        dialogueBoy.transform.position += dialogueBoy.transform.up;
    }
}
