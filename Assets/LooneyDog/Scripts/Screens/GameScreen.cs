using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
namespace LooneyDog
{
    public class GameScreen : MonoBehaviour
    {

        public JoypadController JoypadController { get => _joypadController; set => _joypadController = value; }
        public Button PauseButton { get => _pauseButton; set => _pauseButton = value; }
        public Button Attack { get => _attack; set => _attack = value; }

        [SerializeField] private JoypadController _joypadController;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _attack;

        private void Awake()
        {
            //_attack.onClick.AddListener(OnClickAttack); 
            
        }

        private void OnClickAttack() {
            
            //GameManager.Game.Level.PlayerController.AniCtrl.SetTrigger("AttackTrigger");
        }
    }
}

