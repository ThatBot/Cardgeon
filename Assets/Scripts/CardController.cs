using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cardgeon.Card;

namespace Cardgeon.Base
{
	public class CardController : MonoBehaviour
	{
		[Header("Inspect")]
		[SerializeField] private GameObject inspectUIPanel = null;
        [SerializeField] private Image cardImage = null;
        [SerializeField] private TMP_Text cardNameText = null;
        [SerializeField] private TMP_Text cardDamageText = null;
        [SerializeField] private TMP_Text cardManaText = null;
        [SerializeField] private TMP_Text cardDescriptionText = null;
        public bool isInspecting = false;

        [Header("Hand")]
        [SerializeField] private GameObject[] cardSpots = null;
        [SerializeField] private GameObject cardPrefab = null;

        #region Singleton
        private static CardController _instance;
        public static CardController Instance { get { return _instance; } }


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        #endregion

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                AddCardToHand(); //DEBUG COMMAND
            }
        }

        public void AddCardToHand()
        {
            for (int i = 0; i < cardSpots.Length; i++)
            {
                if(cardSpots[i].transform.childCount != 1)
                {
                    int rand = Random.Range(0, RunController.Instance.deck.storedCards.Count);

                    GameObject cardToAdd = cardPrefab;
                    cardToAdd.GetComponent<Card.Card>().cardInfo = RunController.Instance.deck.storedCards[rand];
                    cardToAdd.GetComponent<Card.Card>().slot = i;
                    Instantiate(cardToAdd, cardSpots[i].transform);
                    break;
                }
            }
        }

        public void AddCardToHand(int amount)
        {
            int addedCards = 0;

            for (int i = 0; i < cardSpots.Length; i++)
            {
                if (cardSpots[i].transform.childCount != 1)
                {
                    int rand = Random.Range(0, RunController.Instance.deck.storedCards.Count);

                    GameObject cardToAdd = cardPrefab;
                    cardToAdd.GetComponent<Card.Card>().cardInfo = RunController.Instance.deck.storedCards[rand];
                    cardToAdd.GetComponent<Card.Card>().slot = i;
                    Instantiate(cardToAdd, cardSpots[i].transform);
                    if(addedCards == amount)
                    {
                        break;
                    }
                }
            }
        }

        public void RemoveCardFromHand(int slot)
        {
            Destroy(cardSpots[slot].transform.GetChild(0).gameObject);
        }

        public void Inspect(CardScriptableObject cardInfo)
        {
            cardImage.sprite = cardInfo.cardSprite;
            cardNameText.text = cardInfo.cardName;
            cardDamageText.text = cardInfo.damageValue.ToString();
            cardManaText.text = cardInfo.manaCost.ToString();
            cardDescriptionText.text = cardInfo.description;

            inspectUIPanel.SetActive(true);
            isInspecting = true;
        }

        public void CloseInspect()
        {
            inspectUIPanel.SetActive(false);
            isInspecting = false;
        }
    }
}
