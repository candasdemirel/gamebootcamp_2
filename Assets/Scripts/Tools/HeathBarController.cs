using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HeathBarController : MonoBehaviour
{
    private Slider _slider;
    [SerializeField]
    private TextMeshProUGUI healthText;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {   
        EventManager.HPChangeEventResult += UpdateSliderValue;
    }

    private void OnDisable()
    {
        EventManager.HPChangeEventResult -= UpdateSliderValue;
    }


    public void UpdateSliderValue(int hp)
    {
        healthText.text = hp.ToString();
        _slider.value = (float)hp / 100;
    }



}
