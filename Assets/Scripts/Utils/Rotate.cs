using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	[SerializeField] private float rotationSpeed = 10f;	

	void Update()
	{
		transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
	}
}
