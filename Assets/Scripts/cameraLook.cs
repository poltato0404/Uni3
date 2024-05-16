using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineFreeLook))]


public class cameraLook : MonoBehaviour
{
    [SerializeField] private float lookSpeed = 1f;
    private CinemachineFreeLook cinemachine;
    private PlayerController playerControls; 
    
    private void Awake() {
        playerControls = new PlayerController();
        cinemachine = GetComponent<CinemachineFreeLook>();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }   


    
    void Update()
    {
        Vector2 delta = playerControls.Player_actionmap.lookAround.ReadValue<Vector2>();
        cinemachine.m_XAxis.Value += delta.x * 100 * lookSpeed * Time.deltaTime;
       
        
    }
}
