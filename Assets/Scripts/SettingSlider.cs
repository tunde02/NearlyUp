using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SettingSlider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sliderValueText;
    [SerializeField] private Slider slider;


    private float settingValue;


    private void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnEnable()
    {
        sliderValueText.text = settingValue.ToString();
    }

    private void OnSliderValueChanged(float newValue)
    {
        settingValue = newValue;
        sliderValueText.text = settingValue.ToString();
    }

    public float GetSettingValue()
    {
        return settingValue;
    }

    public void SetSettingValue(float newValue)
    {
        settingValue = newValue;

        slider.value = settingValue;
        sliderValueText.text = settingValue.ToString();
    }
}
