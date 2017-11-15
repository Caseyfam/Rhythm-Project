using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderColor : MonoBehaviour {

    private int sliderVal = 0;
    public string rgb = "";
    public GameObject background;
    public GameObject fill;

    public UnityEngine.UI.Slider sliderR, sliderG, sliderB;

    private Color localColor;

    void Awake()
    {
        UpdateColor();
    }

    public void UpdateColor()
    {
        sliderVal = 20 - (int)GetComponent<UnityEngine.UI.Slider>().value;
        // Don't forget to edit this 20
        
        switch (rgb)
        {
            case "R":
                localColor = new Color(1f, sliderVal / 20f, sliderVal / 20f);
                break;
            case "G":
                localColor = new Color(sliderVal / 20f, 1f, sliderVal / 20f);
                break;
            case "B":
                localColor = new Color(sliderVal / 20f, sliderVal / 20f, 1f);
                break;
            default:
                localColor = new Color(1f, 1f, 1f);
                break;
        }
        GetComponentInParent<SliderColorLogic>().CreatePlayerColor((int)sliderR.value, (int)sliderG.value, (int)sliderB.value, 20f);

        background.GetComponent<UnityEngine.UI.Image>().color = localColor;
        fill.GetComponent<UnityEngine.UI.Image>().color = localColor;
    }
}
