using UnityEngine;

public class PartsMatch : MonoBehaviour
{
    public CellParts part;

    public bool IsPartsMatch(CellParts _part)
    {
        return _part == part;
    }
}
