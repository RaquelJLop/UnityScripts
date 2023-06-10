using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI namePlayer;
    public TextMeshProUGUI hpText;
    public Slider timeSlider;
    public GameObject panelButtons;

    public TextMeshProUGUI textDamage;
    
    public void SetHUD (Unit unit)
    {
        namePlayer.text = unit.unitName;
        timeSlider.maxValue = unit.unitTime; //Valor máximo del slider
        timeSlider.value = 0; //Valor inicial del slider
    }

    //Función que se va a encargar de mostrar por pantalla la vida del unit cada vez que cambia
    public void SetHP(int currentHP, int maxHP)
    {
        hpText.text = currentHP.ToString() + " / " + maxHP.ToString();
    }

    //////public IEnumerator ShowTextDamage(int damage)
    //////{
    //////    textDamage.gameObject.SetActive(true);
    //////    textDamage.text = damage.ToString();

    //////    float timer = 1;
    //////    while (timer >= 0)
    //////    {
    //////        timer -= Time.deltaTime;
    //////        textDamage.transform.localPosition = new Vector3(textDamage.transform.localPosition.x, textDamage.transform.localPosition.y
    //////            + Time.deltaTime, textDamage.transform.localPosition;
    //////        yield return null;
    //////    }
    //////    textDamage.
    //////}
}
