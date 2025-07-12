using UnityEngine;

[System.Serializable]
public class CardEntry
{
    public int id;
    public GameObject prefab;
    public int cost;
    public string cardName;
    public int mobility;
    public int hp;
    public int attack;
    public int range;
}

public class CardData : MonoBehaviour
{
    public CardEntry[] entries;

    public CardEntry GetCardById(int id)
    {
        foreach (var entry in entries)
        {
            if (entry.id == id)
                return entry;
        }

        Debug.LogWarning($"GameObject avec l'ID {id} non trouvé !");
        return null;
    }
}
