using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HummerController : MonoBehaviour {

	public GameObject particle;
	public AudioSource hitSE;

	public Transform parentTransform;

	//AudioSource audio;

	void Start () {
        hitSE = GetComponent<AudioSource> ();	
	}

	IEnumerator Hit(Vector3 target)
	{
		// Hummer Down		
		transform.position = new Vector3(target.x, 0, target.z);

		//Hummer Rotate
		Quaternion startRotation = Quaternion.Euler(-45, 0f, 90f);
		Quaternion endRotation = Quaternion.Euler(-90f, 0f, 90f);

		Quaternion lerpedRotation = Quaternion.Lerp(startRotation, endRotation, 0.5f);

		transform.rotation = lerpedRotation;

		// Impact
		Instantiate (this.particle, transform.position, Quaternion.identity);

		Camera.main.GetComponent<CameraController>().Shake();

        hitSE.PlayOneShot(hitSE.clip);

		yield return new WaitForSeconds (0.1f);

        // Hummer Up
        transform.rotation = Quaternion.Euler(-45, 0f, 90f);
        transform.position = parentTransform.position;
		yield return null;
    }

	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100)) 
			{
				GameObject mole = hit.collider.gameObject;

				if (mole.TryGetComponent<MolePop>(out MolePop molePop))
				{
					bool isHit = molePop.Hit();

					// if hit the mole, show hummer and effect
					if (isHit)
					{
						StartCoroutine(Hit(mole.transform.position));

						ScoreManager.score += 10;
					}
				}
				else return;
            }
		}
	}
}
