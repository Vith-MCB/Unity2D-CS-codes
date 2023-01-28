using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    [SerializeField] public Slider slider;
    [SerializeField] private Text oxygenText;

    void Update()
    {
        oxygenText.text = ((int)slider.value).ToString() + "/" + ((int)slider.maxValue).ToString();
    }
    public void SetMaxOxBar(float oxygen)
    {
        slider.maxValue = oxygen;
        slider.value = oxygen;
    }

    public void SetOxBar(float oxygen)
    {
        slider.value = oxygen;
    }
}
