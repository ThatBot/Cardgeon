using UnityEngine;
using TMPro;
using Cardgeon.Base;
using Cardgeon.Combat;

namespace Cardgeon.Card
{
	public class Card : MonoBehaviour
	{
        [Header("Cosmetic Variables")]
        public CardScriptableObject cardInfo = null;
        [SerializeField] private float cardSelectOffset = 0f;
        [SerializeField] private SpriteRenderer cardRenderer = null;
        [SerializeField] private Material foilMaterial = null;
        [SerializeField] private TMP_Text cardNameText = null;
        [SerializeField] private TMP_Text cardDamageText = null;
        [SerializeField] private TMP_Text cardManaText = null;
        [SerializeField] private TMP_Text cardDescriptionText = null;
        [HideInInspector] public int slot;
        private Vector3 startPos = Vector3.zero;
        private float handLimit = -0.5f;

        private void Start()
        {
            startPos = transform.position;

            cardRenderer.sprite = cardInfo.cardSprite;
            cardNameText.text = cardInfo.cardName;
            cardDamageText.text = (cardInfo.isSpell) ? "--" : cardInfo.damageValue.ToString();
            cardManaText.text = cardInfo.manaCost.ToString();
            cardDescriptionText.text = cardInfo.description;

            if (cardInfo.isFoil)
            {
                cardRenderer.material = foilMaterial;
            }
        }

        private void OnMouseEnter()
        {
            if (!CardController.Instance.isInspecting) 
            { 
                transform.Translate(transform.up * cardSelectOffset);
            }
        }

        private void OnMouseOver()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Inspect();
            }
        }

        private void OnMouseExit()
        {
            transform.position = startPos;
        }

        private void OnMouseDrag()
        {
            if (!CardController.Instance.isInspecting) 
            { 
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                transform.position = mousePos;
            }
        }

        private void OnMouseUp()
        {
            if(transform.position.y <= handLimit)
            {
                DroppedOnHand();
            }
            else if(transform.position.y > handLimit)
            {
                PlayCard();
            }
            else
            {
                Debug.Log("Where the hell did that card go");
            }
        }

        private void Inspect()
        {
            Sprite sprite = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            CardController.Instance.Inspect(cardInfo);
        }

        public void DroppedOnHand()
        {
            Debug.Log(string.Format("Handed card: {0}", cardInfo.cardName));
            transform.position = startPos;
        }

        public void PlayCard()
        {
            if (!cardInfo.isSpell)
            {
                BattleManager.Instance.HurtEnemy(cardInfo.damageValue);
            }           
            Debug.Log(string.Format("Played card: {0}", cardInfo.cardName));
            CardController.Instance.RemoveCardFromHand(slot);
            CallCardEvent();
        }

        public void CallCardEvent()
        {
            int rand = Random.Range(0, 100);

            if (rand <= cardInfo.eventRandomChance)
            {
                BattleManager.Instance.battleEvents.ExecuteEvent(cardInfo.cardEvent);
            }            
        }
    }
}
