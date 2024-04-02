using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is where all the data lives
/// </summary>
namespace GameEssentials.GameManager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        /// <summary>
        /// Dictates which scene to load
        /// </summary>
        public int sceneToLoad;

        /// <summary>
        /// Dictates whether or not we can unlock the level
        /// </summary>
        public bool[] isLevelComplete;

        // Define any variables that we can store and track here
        // We can store them locally in a persistent drive somewhere in the device
        public int playerScore;
        public int playerCoins;
        public int totalGameTimeSession; // Might be unnecessary

        // Awake is called at the very first instance the game loads or this object gets enabled
        private void Awake()
        {
            Instance = this;

            for(int i = 0; i < isLevelComplete.Length; i++)
            {
                isLevelComplete[i] = PlayerPrefs.GetInt("LevelComplete" + i) == 1 ? true : false;
            }

            DontDestroyOnLoad(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnApplicationQuit()
        {
            for(int i = 0; i < isLevelComplete.Length; i++)
            {
                PlayerPrefs.SetInt("LevelComplete" + i, isLevelComplete[i] == true ? 1 : 0);
            }
            PlayerPrefs.Save();
        }

        public void SaveData()
        {
            for (int i = 0; i < isLevelComplete.Length; i++)
            {
                PlayerPrefs.SetInt("LevelComplete" + i, isLevelComplete[i] == true ? 1 : 0);
            }
            PlayerPrefs.Save();
        }
    }
}


public class PlayerData
{
    public int playerScore;
    public int playerCoins;
    public int totalGameTimeSession;
}