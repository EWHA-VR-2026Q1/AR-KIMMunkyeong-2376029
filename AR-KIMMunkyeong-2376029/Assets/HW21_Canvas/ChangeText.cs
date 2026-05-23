using UnityEngine;
using TMPro;

public class ChangeText : MonoBehaviour
{
    public TextMeshProUGUI myText;

    public void OnButtonClick()
    {
        myText.text = "Button Click";
    }
}