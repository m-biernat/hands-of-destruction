using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour {

    [SerializeField] private Text sliderText;

    public void ChangeSliderText(float value)
    {
        sliderText.text = value.ToString();
    }
}
