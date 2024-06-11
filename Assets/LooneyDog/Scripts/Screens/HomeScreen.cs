using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
namespace LooneyDog
{
    public class HomeScreen : MonoBehaviour
    {
        public Button Startbutton { get => _startbutton; set => _startbutton = value; }
        public Button ScoreBoard { get => _scoreBoard; set => _scoreBoard = value; }
        public Button SettingButton { get => _settingButton; set => _settingButton = value; }
        public float TransitionSpeedPop { get => _transitionSpeedPop; set => _transitionSpeedPop = value; }

        [SerializeField] private Button _startbutton;
        [SerializeField] private Button _scoreBoard;
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _shopButton;


        [SerializeField] private float _transitionSpeedPop;
        

        private void Awake(){

        _startbutton.onClick.AddListener(OnClickStart);
        _scoreBoard.onClick.AddListener(OnClickScoreBoard);
        _settingButton.onClick.AddListener(OnClickSetting);
        _shopButton.onClick.AddListener(OnClickShopButton);
           

       }


        private void OnClickSetting()
        {
            GameManager.Game.Screen.OpenPopUpScreen(GameManager.Game.Screen.Setting.transform, ScreenLocation.Pop, TransitionSpeedPop);
        }

        private void OnClickScoreBoard()
        {
            GameManager.Game.Screen.OpenPopUpScreen(GameManager.Game.Screen.Result.transform,ScreenLocation.Pop, TransitionSpeedPop);
        }

        private void OnClickStart(){
            //GameManager.Game.Screen.LoadFadeScreen(this.gameObject,GameManager.Game.Screen.Load.gameObject);
            GameManager.Game.Screen.Load.LoadLevel(2, GameDifficulty.Easy, gameObject);
        }

        private void OnClickShopButton()
        {
            GameManager.Game.Screen.OpenPopUpScreen(GameManager.Game.Screen.Shop.transform, ScreenLocation.Pop, TransitionSpeedPop);
        }




    }
}