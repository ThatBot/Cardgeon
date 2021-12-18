using UnityEngine;
using UnityEngine.UI;

public class CardPlayer : MonoBehaviour
{
	[SerializeField] private RawImage cardImage = null;
    private CardObject cardObject;
    private Card card;
    
    public static CardPlayer instance;

    private void Awake()
    {
        instance = this;
    }

    public void SetCard(CardObject cardObject, Card cardComponent)
    {
        cardImage.texture = cardObject.sprite.texture;
        cardImage.gameObject.SetActive(true);
        this.cardObject = cardObject;
        card = cardComponent;
        card.SwitchEnabled();
    }

    public void DismissCard()
    {
        card.SwitchEnabled();
        cardImage.gameObject.SetActive(false);
    }

    public void PlayCard()
    {
        Debug.Log("Played card");
        Destroy(card.gameObject);
        cardImage.gameObject.SetActive(false);
    }
}
