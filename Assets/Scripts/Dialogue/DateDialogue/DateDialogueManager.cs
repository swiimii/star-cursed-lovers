using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateDialogueManager : MonoBehaviour
{
    private List<Line> positiveLines, negativeLines, neutralLines;

    public DateDetails dateDetails;
    public DialogueBox dialogueBox;

    public int dialogueMeter = 0;

    private void Start()
    {
        positiveLines = new List<Line>(dateDetails.positiveLines);
        negativeLines = new List<Line>(dateDetails.negativeLines);
        neutralLines = new List<Line>(dateDetails.neutralLines);
    }

    public IEnumerator PlayerSendMessage(Line message, List<GameDefs.Type> dialogueTypes)
    {
        bool isGood = false, isBad = false;

        foreach ( var aspect in dialogueTypes )
        {
            if( dateDetails.negativeTypes.Contains(aspect) )
            {
                // bad affect
                Debug.Log("Bad line...");
                isBad = true;
                dialogueMeter--;
            }
            
            if( dateDetails.positiveTypes.Contains(aspect) )
            {
                // good affect
                Debug.Log("Good Line!");
                isGood = true;
                dialogueMeter++;
            }

            // Reveal Trait If Necessary!
        }

        Line dateResponse;

        if ( isGood == isBad )
        {
            // Neutral response
            dateResponse = PopResponseLine( ref neutralLines );
        }
        else if( isGood )
        {
            // Good Response
            dateResponse = PopResponseLine( ref positiveLines );
        }
        else // isBad
        {
            // Bad Response
            dateResponse = PopResponseLine(ref negativeLines );
        }

        yield return MessageAndResponse(message, dateResponse);
    }

    private IEnumerator MessageAndResponse(Line message, Line response )
    {
        StopAllCoroutines();
        dialogueBox.StopAllCoroutines();
        yield return MessageAndResponseIE( message, response );
    }

    private IEnumerator MessageAndResponseIE(Line message, Line response)
    {
        yield return dialogueBox.DisplayMessage(message);
        yield return new WaitForSeconds(1.5f);
        yield return dialogueBox.DisplayMessage(response);
        yield return new WaitForSeconds(.5f);
    }

    public void Test_MessageAndResponse()
    {
        Line tmpLine = ScriptableObject.CreateInstance<Line>();
        tmpLine.text = "Sup gamer, I hope your dog has been well!";
        tmpLine.color = Color.black;
        var typeList = new List<GameDefs.Type> { GameDefs.Type.Pure, GameDefs.Type.Intelligence };
        PlayerSendMessage(tmpLine, typeList) ;
        // MessageAndResponse(tmpLine, dateDetails.positiveLines[0]);
    }

    public Line PopResponseLine( ref List<Line> responseList )
    {
        int index = Random.Range(0, responseList.Count);
        Line returnLine = responseList[index];
        if( responseList.Count > 1 )
        {
            responseList.RemoveAt(index);
        }
        return returnLine;
    }
}
