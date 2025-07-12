using UnityEngine;
using DG.Tweening;


public class CardManager : MonoBehaviour
{
    Vector3 orignalPos;
    public bool traveling = true;
    public string cardName;
    public int hp;
    public int mobility;
    public int attack;
    public int range;
    bool setOnce = false;

    private HandManager handManager;

    void OnMouseEnter()
    {
        Debug.Log(handManager);
        handManager.DeleteFromHand(gameObject);
        Destroy(gameObject);
        /*if (traveling) return;
        Debug.Log(cardName);
        Debug.Log("Hp" + hp);
        transform.DOMoveY(transform.position.y + 0.2f, 0.25f);
        if (setOnce) return;
        orignalPos = transform.position;
        setOnce = true;*/
    }

    void OnMouseExit()
    {
        if (traveling) return;
        transform.DOMoveY(orignalPos.y, 0.25f);
    }

    public void fillCardData(CardEntry entry)
    {
        cardName = entry.cardName;
        hp = entry.hp;
        mobility = entry.mobility;
        attack = entry.attack;
        range = entry.range;
    }

    public void fillHandManager(HandManager hManager)
    {
        Debug.Log(hManager);
        handManager = hManager;
    }
}
