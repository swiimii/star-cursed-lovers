using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    public Text dialogueText;

    public IEnumerator DisplayMessage( Line message )
    {
        StopCoroutine("DisplayMessageIE");
        dialogueText.text = "";
        dialogueText.color = message.color;
        yield return StartCoroutine(DisplayMessageIE(message.text));
    }

    private IEnumerator DisplayMessageIE( string message )
    {
        int i = 0;
        while (i < message.Length)
        {
            dialogueText.text = dialogueText.text + message[i];
            i++;
            yield return new WaitForFixedUpdate();
        }
    }
}
