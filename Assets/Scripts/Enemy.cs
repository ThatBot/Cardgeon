using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Cardgeon.Combat{
	public class Enemy : MonoBehaviour
	{
		[SerializeField] private EnemyTypeScriptableObject enemyType;
        [SerializeField] private SpriteRenderer enemyRenderer;
        [SerializeField] private Slider healthBarSlider;
        [SerializeField] private TMP_Text healthText;
        private int health;

        private void Start()
        {
            enemyRenderer.sprite = enemyType.enemySprite;
            health = enemyType.enemyHealth;
            healthBarSlider.maxValue = enemyType.enemyHealth;
            healthBarSlider.value = enemyType.enemyHealth;
            healthText.text = enemyType.enemyHealth.ToString();
        }

        public virtual void Hurt(int damage)
        {
            health -= damage;
            UpdateHealthBar();
            if (health <= 0)
            {
                Die();
            }
        }

        private void UpdateHealthBar()
        {
            healthBarSlider.value = health;
            healthText.text = health.ToString();
        }

        public virtual void Die()
        {
            BattleManager.Instance.EnemyDied();
        }
    }
}
