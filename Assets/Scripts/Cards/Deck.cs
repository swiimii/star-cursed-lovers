using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    public List<CardDetails> rizzlerDeck, wizardDeck, rebelDeck;

    [SerializeField] private List<CardDetails> drawPile;
    private List<CardDetails> discardPile;

    public GameObject cardPrefab;
    public Sprite[] cardArt, deckArt;


    void Awake()
    {
        // shuffle/copy contents of decklist into drawPile
        drawPile = new List<CardDetails>(GetDeckList());

        // Use funny lambda to shuffle deck
        drawPile.Sort( (CardDetails a, CardDetails b) => { return Random.value > .5f ? 1 : -1; } );

        GetComponent<Image>().sprite = GetDeckSprite();
    }

#nullable enable
    public GameObject? DrawCard()
    {
        if( drawPile.Count > 0 )
        {

            var newCard = Instantiate(cardPrefab);
            newCard.GetComponent<Card>().details = drawPile[0];
            newCard.GetComponent<Image>().sprite = GetCardSprite();
            drawPile.RemoveAt(0);
            
            return newCard;
        }
        else
        {
            return null;
        }
    }

    public Sprite GetCardSprite()
    {
        switch( GameState.singleton.deckMode )
        {
            case GameDefs.Decks.Rizzler:
                return cardArt[0];
            case GameDefs.Decks.Wizard:
                return cardArt[1];
            case GameDefs.Decks.Rebel:
                return cardArt[2];
            default:
                Debug.Log("Unexpected deck mode");
                return cardArt[0];
        }
    }

    public Sprite GetDeckSprite()
    {
        switch (GameState.singleton.deckMode)
        {
            case GameDefs.Decks.Rizzler:
                return deckArt[0];
            case GameDefs.Decks.Wizard:
                return deckArt[1];
            case GameDefs.Decks.Rebel:
                return deckArt[2];
            default:
                Debug.Log("Unexpected deck mode");
                return deckArt[0];
        }
    }

    public List<CardDetails> GetDeckList()
    {
        switch (GameState.singleton.deckMode)
        {
            case GameDefs.Decks.Rizzler:
                return rizzlerDeck;
            case GameDefs.Decks.Wizard:
                return wizardDeck;
            case GameDefs.Decks.Rebel:
                return rebelDeck;
            default:
                Debug.Log("Unexpected deck mode");
                return rizzlerDeck;
        }
    }
}
