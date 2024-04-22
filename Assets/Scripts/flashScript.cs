using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashScript : MonoBehaviour
{
    // The sphere's radius
    private float sphereRadius = 5f;

    // Maximum distance the SphereCast should travel
    public float maxDistance = 10f;

    // LayerMask to determine which objects to interact with
    public LayerMask interactableLayers;

    // State tracking variable
    private bool wasHitPreviously = false;

    void Update()
    {
        // Assuming there's a Light component attached to this GameObject
        Light light = GetComponent<Light>();

        if (light != null)
        {
            // Get the light's position
            Vector3 lightPosition = light.transform.position;

            // Get the light's forward direction
            Vector3 lightDirection = light.transform.forward;

            // Perform a SphereCast
            RaycastHit hitInfo;
            bool isHit = Physics.SphereCast(lightPosition, sphereRadius, lightDirection, out hitInfo, maxDistance, interactableLayers);

            if (isHit)
            {
                if (!wasHitPreviously)
                {
                    // Transition from no hit to hit
                    OnStartHit(hitInfo);
                }
                wasHitPreviously = true;
            }
            else
            {
                if (wasHitPreviously)
                {
                    // Transition from hit to no hit
                    OnStopHit();
                }
                wasHitPreviously = false;
            }

            // Visualize the direction of the SphereCast
            Debug.DrawRay(lightPosition, lightDirection * maxDistance, isHit ? Color.red : Color.blue);
        }
    }

    private void OnStartHit(RaycastHit hitInfo)
    {
        // If the SphereCast hits an object
        Animator hitAnimator = hitInfo.collider.gameObject.GetComponent<Animator>();
        ai aicomp = hitInfo.collider.gameObject.GetComponent<ai>();

        if (aicomp != null)
        {
            // Set the flag to indicate the object is flashed
            aicomp.flashed = true;
        }

        // You can add any other logic for when a hit starts
    }

    private void OnStopHit()
    {
        // Function to call when the SphereCast stops hitting objects
        GameObject[] allInteractables = GameObject.FindGameObjectsWithTag("guard");

        foreach (GameObject interactable in allInteractables)
        {
            ai aicomp = interactable.GetComponent<ai>();
            if (aicomp != null)
            {
                aicomp.flashed = false;
            }
        }

        // You can add any other logic for when the hit stops
    }
}
