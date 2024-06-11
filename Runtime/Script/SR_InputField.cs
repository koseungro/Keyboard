using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SR_InputField : MonoBehaviour
{
    private InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<InputField>();
        inputField.lineType = InputField.LineType.MultiLineNewline;
    }

    public void Update()
    {
        if(inputField.isFocused==true)
        {
            SR_KeyBoard.Instance.curInputfield = gameObject.GetComponent<SR_InputField>();
        }
    }

    public void ActivateInputField()
    {
        inputField.ActivateInputField();
        StartCoroutine(EndText());
    }
    IEnumerator EndText()
    {
        yield return 0;
        inputField.MoveTextEnd(false);
    }


    public void SetText(string text)
    {
        inputField.text = text;
    }
}
