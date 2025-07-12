using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Splines;
using DG.Tweening;

public class HandManager : MonoBehaviour
{
    [SerializeField] private int maxHandSize;
    [SerializeField] private GameObject cardPrefab;

    [SerializeField] private SplineContainer splineContainer;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private DeckManager deckManager;

    private List<GameObject> handCards = new();
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) DrawCard();
    }

    private void DrawCard()
    {
        if (handCards.Count >= maxHandSize) return;
        CardEntry drawnCard = deckManager.DrawCardFromDeck();
        if (drawnCard == null)
        {
            Debug.Log("No more card in the deck");
            return;
        }
        GameObject cardObject = Instantiate(drawnCard.prefab, spawnPoint.position, spawnPoint.rotation);
        CardManager cardManager = cardObject.GetComponent<CardManager>();
        cardManager.fillCardData(drawnCard);
        cardManager.fillHandManager(this);
        handCards.Add(cardObject);
        UpdateCardPos(cardObject.GetComponent<CardManager>());
    }

    private void UpdateCardPos(CardManager card)
    {
        if (handCards.Count == 0) return;
        float cardSpacing = 1f / 15;
        float firstCardPosition = 0.5f - (handCards.Count - 1) * cardSpacing / 2;
        Spline spline = splineContainer.Spline;
        for (int i = 0; i < handCards.Count; i++)
        {
            float position = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(position);
            Vector3 forward = spline.EvaluateTangent(position);
            Vector3 up = spline.EvaluateUpVector(position);
            Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, forward).normalized);
            handCards[i].transform.DOLocalRotateQuaternion(rotation, 0.25f);
            handCards[i].transform.DOMove(splinePosition, 0.25f).OnComplete(() =>
            {
                card.traveling = false;
            });
        }
    }

    private void ArrangeCardPos()
    {
        if (handCards.Count == 0) return;
        float cardSpacing = 1f / 15;
        float firstCardPosition = 0.5f - (handCards.Count - 1) * cardSpacing / 2;
        Spline spline = splineContainer.Spline;
        for (int i = 0; i < handCards.Count; i++)
        {
            float position = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(position);
            Vector3 forward = spline.EvaluateTangent(position);
            Vector3 up = spline.EvaluateUpVector(position);
            Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, forward).normalized);
            handCards[i].transform.DOLocalRotateQuaternion(rotation, 0.25f);
            handCards[i].transform.DOMove(splinePosition, 0.25f);
        }
    }

    public void DeleteFromHand(GameObject card)
    {
        handCards.Remove(card);
        ArrangeCardPos();
    }
}
