using UnityEngine;

public class PhaseMatch : MonoBehaviour
{
    public MitosisPhase phase;

    public bool IsPhaseMatch(MitosisPhase _phase)
    {
        if (_phase == phase) return true;
        else return false;
    }
}