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

    public void UpdateStamina(float stamina)
    {
        stamSlider.value = stamina;
    }

    public void StartStaminaSlider(float maxStamina)
    {
        stamSlider.maxValue = maxStamina;
        stamSlider.value = maxStamina;
    }
}
