using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LooneyDog
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Game;

        public LevelManager Level;
        public DataManager Data;
        public ScreenManager Screen;
        public SkinManager Skin;
        public SoundManager Sound;
        
        // public AnimationManager Animation;


        private void Awake()
        {
            Debug.Log("GameManager is awake.");
            if (Game == null)
            {
                DontDestroyOnLoad(gameObject);
                Game = this;
                Initialize();
            }
            else if (Game != this)
            {
                Destroy(gameObject);
            }

        }

        private void Initialize()
        {
            Application.targetFrameRate = 60;
            Debug.Log("Intializing GameManager.");

            if (Data == null) { Data = gameObject.GetComponent<DataManager>(); }
            Debug.Log("Data Manager loaded.");

           /* if (Skin == null) { Skin = gameObject.GetComponent<SkinManager>(); }
            Debug.Log("Skin Manager loaded.");*/

            if (Level == null) { Level = gameObject.GetComponent<LevelManager>(); }
            Debug.Log("Level Manager loaded.");

            if (Screen == null) { Screen = gameObject.GetComponent<ScreenManager>(); }
            Debug.Log("Screen Manager loaded.");

            /*if (Sound == null) { Sound = gameObject.GetComponent<SoundManager>(); }
            Debug.Log("Sound Manager loaded");

            if (Animation == null) { Animation = gameObject.GetComponent<AnimationManager>(); }
            Debug.Log("Animation Manager loaded");*/

        }
    }
}