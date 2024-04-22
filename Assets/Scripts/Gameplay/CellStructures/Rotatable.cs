using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rotatable : MonoBehaviour
{
	[SerializeField] private InputAction pressed, axis;

	private Transform cam;
	[SerializeField] private float speed = 1;
	[SerializeField] private bool inverted;
	private Vector2 rotation;
	private bool rotateAllowed = true; // Initialize to true
	private bool isDragging = false; // Track if dragging is happening

	[SerializeField] GameObject drag;
	Isdragging dragStat;

	[SerializeField] GameObject cell;

	private void Awake()
	{
		cell.SetActive(true);
		dragStat = drag.GetComponent<Isdragging>();
		cam = Camera.main.transform;
		pressed.Enable();
		axis.Enable();
		pressed.performed += _ => { StartCoroutine(Rotate()); };
		pressed.canceled += _ => { rotateAllowed = false; };
		axis.performed += context => { rotation = context.ReadValue<Vector2>(); };
	}

	private IEnumerator Rotate()
	{
		rotateAllowed = true;
		while (rotateAllowed && !isDragging) // Check if not dragging
		{
			// apply rotation


			rotation *= speed;
			transform.Rotate(Vector3.up * (inverted ? 1 : -1), rotation.x, Space.World);
			transform.Rotate(cam.right * (inverted ? -1 : 1), rotation.y, Space.World);
			yield return null;
		}
	}

	// Called when dragging starts
	public void OnDragStart()
	{
		isDragging = true;
	}

	// Called when dragging ends
	public void OnDragEnd()
	{
		isDragging = false;
	}
	void Update()
	{
		isDragging = dragStat.isDragging;
	}
}
