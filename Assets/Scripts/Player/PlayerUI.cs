using UnityEngine;
using UnityEngine.UI;

// Por el momento, esta clase es un MonoBehaviour, pero en el futuro será una
// clase que se instanciará y asignará al principio de un nivel.

/// <summary>
/// Expone la UI del jugador. Esta clase se encarga de actualizar los elementos de la UI del jugador, como la barra de stamina, y exponer métodos para que otras clases puedan interactuar con la UI.
/// </summary>
public class PlayerUI : MonoBehaviour
{
    public Slider stamSlider;

    public Slider speedOMeter;
    public Text speedOMeterText;

    public void StartStaminaSlider(float maxStamina)
    {
        stamSlider.maxValue = maxStamina;
        stamSlider.value = maxStamina;
    }

    public void UpdateStamina(float stamina)
    {
        stamSlider.value = stamina;
    }

    public void StartSpeedOMeter(float maxSpeed)
    {
        speedOMeter.maxValue = maxSpeed;
        speedOMeter.value = 0;
        speedOMeterText.text = $"Speed: 0";
    }

    public void UpdateSpeedOMeter(float speed)
    {
        speedOMeter.value = speed;
        speedOMeterText.text = $"Speed: {speed.ToString("0.00")}";
    }
}
