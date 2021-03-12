using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum BarType
{
    HelthBar,
    ManaBar
}
public class Player_Bar : MonoBehaviour
{
    private Slider _slider ;
    public BarType Type;
    
    
    void Start()
    {
        _slider = GetComponent<Slider>();
        switch (Type)
        {
            case BarType.HelthBar:
                _slider.maxValue = PlayerController.MAX_HEALTH;
                break;
            case BarType.ManaBar:
                _slider.maxValue = PlayerController.MAX_MANA;
                break;
        }
    }
    
    void Update()
    {
        switch (Type)
        {
            case BarType.HelthBar:
                _slider.value = GameObject.Find("Player").GetComponent<PlayerController>().GetHelth();
                break;
            case BarType.ManaBar:
                _slider.value = GameObject.Find("Player").GetComponent<PlayerController>().GetMana();
                break;
        }
    }
}
