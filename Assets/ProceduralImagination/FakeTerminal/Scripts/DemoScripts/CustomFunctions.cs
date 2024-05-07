﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomFunctions : MonoBehaviour
{
    public List<Light> lights;
    public Light directional;

    public void AllLightsOnOff()
    {
        if (lights != null)
        {
            foreach (Light light in lights)
            {
                if (light.gameObject.activeSelf)
                {
                    light.gameObject.SetActive(false);
                }
                else
                {
                    light.gameObject.SetActive(true);
                }
            }
        }
    }

    public void DirectionalLightOnOff()
    {
        if (directional.gameObject.activeSelf)
        {
            directional.gameObject.SetActive(false);
        }
        else
        {
            directional.gameObject.SetActive(true);
        }
    }

    public void returnToMaze()
    {
        SceneManager.LoadScene("level1");
    }
    public void l1m1()
    {
        SceneManager.LoadScene("06CellMemory");
    }

    public void l1m2()
    {
        SceneManager.LoadScene("07CellStructures");
    }
    public void l1m3()
    {
        SceneManager.LoadScene("04CellDivision");
    }

    public void l2m1()
    {
        SceneManager.LoadScene("PlantGame");
    }
    
    public void l2m2()
    {
        SceneManager.LoadScene("02FeedbackMechanism");
    }
    public void l2m3()
    {
        SceneManager.LoadScene("05BodyOrganFunctions");
    }
     public void l3m1()
    {
        SceneManager.LoadScene("mendel");
    }
    
    public void l3m2()
    {
        SceneManager.LoadScene("03CentralDogma");
    }
    public void l3m3()
    {
        SceneManager.LoadScene("01RecombinantDNA");
    }
    public void nextLvl()
    {
        SceneManager.LoadScene("Shop");
    }
}
