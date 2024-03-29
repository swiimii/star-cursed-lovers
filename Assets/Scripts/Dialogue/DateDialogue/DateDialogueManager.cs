using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateDialogueManager : MonoBehaviour
{
    [SerializeField] private List<Line> positiveLines, negativeLines, neutralLines;

    public DateDetails dateDetails;
    public DialogueBox dialogueBox;

    public int dialogueMeter = 0, messageCount = 0;
    public int currentdialogue;
    public LoveBar loveBar;

    const int MAX_MESSAGES = 5, VICTORY_THRESHOLD = 1;

    private void Start()
    {
        positiveLines = new List<Line>(dateDetails.positiveLines);
        negativeLines = new List<Line>(dateDetails.negativeLines);
        neutralLines = new List<Line>(dateDetails.neutralLines);
        currentdialogue = dialogueMeter;
        loveBar.SetHealth(dialogueMeter);
        foreach ( var line in dateDetails.introLines )
        {
            StartCoroutine(DisplayMessageIE(line));
        }
    }

    public IEnumerator PlayerSendMessage(Line message, List<GameDefs.Type> dialogueTypes)
    {
        bool isGood = false, isBad = false;
        messageCount++;
        foreach ( var aspect in dialogueTypes )
        {
            if( dateDetails.negativeTypes.Contains(aspect) )
            {
                // bad affect
                Debug.Log("Bad line...");
                isBad = true;
                dialogueMeter--;
                loveBar.SetHealth(dialogueMeter);
            }
            
            if( dateDetails.positiveTypes.Contains(aspect) )
            {
                // good affect
                Debug.Log("Good Line!");
                isGood = true;
                dialogueMeter++;
                loveBar.SetHealth(dialogueMeter);
            }

            // Reveal Trait If Necessary!
        }

        Line dateResponse;
        int responseValue = 0;
        if ( isGood == isBad )
        {
            // Neutral response
            dateResponse = PopResponseLine( ref neutralLines );
        }
        else if( isGood )
        {
            // Good Response
            responseValue = 1;
            dateResponse = PopResponseLine( ref positiveLines );
        }
        else // isBad
        {
            // Bad Response
            responseValue = -1;
            dateResponse = PopResponseLine(ref negativeLines );
        }

        yield return MessageAndResponse(message, dateResponse, responseValue);

        if(DateIsConcluding())
        {
            yield return WaitForAudioSource();
            yield return DateConclusion();
        }
    }

    public bool DateIsConcluding()
    {
        return messageCount >= MAX_MESSAGES;
    }

    private IEnumerator MessageAndResponse(Line message, Line response, int responseValue )
    {
        StopAllCoroutines();
        dialogueBox.StopAllCoroutines();
        yield return MessageAndResponseIE( message, response, responseValue );
    }

    private IEnumerator MessageAndResponseIE(Line message, Line response, int reactionValue )
    {
        GetComponent<AudioSource>().Stop();
        yield return dialogueBox.DisplayMessage(message);
        yield return new WaitForSeconds(1.5f);
        
        FindObjectOfType<DateSprite>().DoReaction(reactionValue);
        if (response.voice)
        {
            GetComponent<AudioSource>().PlayOneShot(response.voice);
        }
        yield return dialogueBox.DisplayMessage(response);
        yield return new WaitForSeconds(.5f);
    }

    private IEnumerator DisplayMessageIE(Line message )
    {
        if( message.voice )
        {
            GetComponent<AudioSource>().PlayOneShot(message.voice);
        }
        yield return dialogueBox.DisplayMessage(message);
        yield return new WaitForSeconds(1.5f);
    }

    public IEnumerator DateConclusion()
    {
        if( dialogueMeter >= VICTORY_THRESHOLD )
        {
            // good
            foreach( var line in dateDetails.winLines )
            {
                yield return DisplayMessageIE(line);
                yield return WaitForAudioSource();
            }
            GameState.singleton.daysWon.Add(GameState.singleton.daysPassed - 1);
            GameState.singleton.TransitionToLevelSelect();
        }
        else
        {
            // Bad
            foreach (var line in dateDetails.loseLines)
            {
                yield return DisplayMessageIE(line);
                yield return WaitForAudioSource();
            }
            GameState.singleton.TransitionToLevelSelect();
        }
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

    public IEnumerator WaitForAudioSource()
    {
        var src = GetComponent<AudioSource>();
        while( src.isPlaying )
        {
            yield return null;
        }
    }
}
