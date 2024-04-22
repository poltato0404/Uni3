using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodonMatch : MonoBehaviour
{
    public delegate void DelEventOnRightMatch(int count);
    public static event DelEventOnRightMatch OnRightMatch;

    public Nucleotide nucleotideToMatch;
    public Polypeptide polypeptideToMatch;
    public Material materialToChange;
    public Renderer _renderer;

    public bool isMatch(Nucleotide _nucleotide, Polypeptide _polypeptide, GameObject objToAttach)
    {
        if (_nucleotide == nucleotideToMatch && _nucleotide != Nucleotide.None)
        {
            _renderer.material = materialToChange;
            objToAttach.transform.parent = this.transform;
            OnRightMatch?.Invoke(1);
            return true;
        }

        if(_polypeptide != Polypeptide.None)
        {
            if(_polypeptide == polypeptideToMatch)
            {
                _renderer.material = materialToChange;
                objToAttach.transform.parent = this.transform;
                OnRightMatch?.Invoke(1);
                return true;
            }
        }

        return false;
    }
}