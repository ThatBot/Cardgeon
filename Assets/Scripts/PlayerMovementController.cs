using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardgeon.Dungeon{
	public class PlayerMovementController : MonoBehaviour
	{
		[Header("Properties")]
		[SerializeField] private float speed;
        [SerializeField] private Rigidbody2D rb;

        Vector2 input;


        private void Update()
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + input * speed * Time.deltaTime);
        }
    }
}
