using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickenManager : MonoBehaviour
{
    [SerializeField]chickinMaker p1,p2,p3,p4,p5,p6,p7,p8; 
    [SerializeField] List<GameObject> chickens;
    void Start()
    {
        p1.InstantiateChicken(chickens[0]);
        p2.InstantiateChicken(chickens[1]);
        p3.InstantiateChicken(chickens[2]);
        p4.InstantiateChicken(chickens[3]);
        p5.InstantiateChicken(chickens[4]);
        p6.InstantiateChicken(chickens[5]);
        p7.InstantiateChicken(chickens[6]);
        p8.InstantiateChicken(chickens[7]);

    }

    public void resetChickens(){
        p1.destroyChick();
        p2.destroyChick();
        p3.destroyChick();
        p4.destroyChick();
        p5.destroyChick();
        p6.destroyChick();
        p7.destroyChick();
        p8.destroyChick();
        p1.InstantiateChicken(chickens[0]);
        p2.InstantiateChicken(chickens[1]);
        p3.InstantiateChicken(chickens[2]);
        p4.InstantiateChicken(chickens[3]);
        p5.InstantiateChicken(chickens[4]);
        p6.InstantiateChicken(chickens[5]);
        p7.InstantiateChicken(chickens[6]);
        p8.InstantiateChicken(chickens[7]);

    }
    

    
}
