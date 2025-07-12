using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private List<int> deck = new();
    [SerializeField] private CardData database;

    public CardEntry DrawCardFromDeck()
    {
        if (deck.Count <= 0) return null;
        int randomInt = Random.Range(0, deck.Count());
        CardEntry cardEntry = database.GetCardById(deck[randomInt]);
        deck.RemoveAt(randomInt);
        return cardEntry;
    }
}
