using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardPlayer : MonoBehaviour
{
	[SerializeField] private RawImage cardImage = null;
    private CardObject cardObject = null;
    private Card card = null;
    
    public static CardPlayer instance;

    private void Awake()
    {
        instance = this;
    }

    public void SetCard(CardObject cardObject, Card cardComponent)
    {
        if(card != null)
            DismissCard();
        cardImage.texture = cardObject.sprite.texture;
        this.cardObject = cardObject;
        card = cardComponent;
        card.SwitchEnabled();
        cardImage.gameObject.SetActive(true);
    }

    public void DismissCard()
    {
        card.SwitchEnabled();
        CleanPlayer();
    }

    public void PlayCard()
    {
        Debug.Log("Played card");
        Destroy(card.gameObject);
        CleanPlayer();
    }

    private void CleanPlayer()
    {
        cardImage.gameObject.SetActive(false);
        card = null;
        cardObject = null;
    }
}
