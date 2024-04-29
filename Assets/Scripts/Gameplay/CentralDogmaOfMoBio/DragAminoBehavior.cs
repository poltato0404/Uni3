using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Dynamic,
    Static
}

public class DragAminoBehavior : MonoBehaviour
{
    public Vector3 offset;
    public string destinationTag = "DropZone";
    public Nucleotide nucleotide;
    public State state = State.Dynamic;
    public Polypeptide polypeptide = Polypeptide.None;

    /**
     * 0 - A
     * 1 - T
     * 2 - G
     * 3 - C
     * 4 - U
     */
    public Material[] materials;

    private Renderer _renderer;
    private Rigidbody rb;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        if (state == State.Dynamic && polypeptide == Polypeptide.None)
        {
            rb.isKinematic = false;

            int randomNum = Random.Range(0, 5);
            switch (randomNum)
            {
                case 0:
                    nucleotide = Nucleotide.A;
                    _renderer.material = materials[0];
                    break;
                case 1:
                    nucleotide = Nucleotide.T;
                    _renderer.material = materials[1];
                    break;
                case 2:
                    nucleotide = Nucleotide.G;
                    _renderer.material = materials[2];
                    break;
                case 3:
                    nucleotide = Nucleotide.C;
                    _renderer.material = materials[3];
                    break;
                case 4:
                    nucleotide = Nucleotide.U;
                    _renderer.material = materials[4];
                    break;
            }
            Invoke("LifeTime", 10f);
        }
    }

    void LifeTime()
    {
        if(state == State.Dynamic)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
        transform.GetComponent<Collider>().enabled = false;

        rb.isKinematic = true;

        //_renderer.material = materials[Random.Range(0, materials.Length)];
    }

    private void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + offset;
    }

    private void OnMouseUp()
    {
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit hitInfo;
        if(Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
        {
            if(hitInfo.transform.tag == destinationTag)
            {
                transform.position = hitInfo.transform.position;
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                // If AminoAcid matches the correct one then Positive Feedback
                // Else Negative feedback
                if (hitInfo.transform.gameObject.GetComponent<CodonMatch>().isMatch(nucleotide, polypeptide, this.gameObject))
                {
                    ObjectiveHandler.instance.score += 10;
                    VAFeedback.Instance.RightAnswer(hitInfo.transform);
                    transform.GetComponent<Collider>().enabled = false;
                    state = State.Static;
                }
                else
                {
                    if (ObjectiveHandler.instance.score <= 0) ObjectiveHandler.instance.score = 0;
                    else ObjectiveHandler.instance.score -= 5;
                    VAFeedback.Instance.WrongAnswer(hitInfo.transform);
                    transform.GetComponent<Collider>().enabled = true;
                    rb.isKinematic = false;
                }
            }
            else
            {
                transform.GetComponent<Collider>().enabled = true;
                rb.isKinematic = false;
            }
        }
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}