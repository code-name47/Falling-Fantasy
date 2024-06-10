using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LooneyDog{
    public class LoadingScreen : MonoBehaviour
    {
        public float LoadSeconds { get => _loadSeconds; set => _loadSeconds = value; }
        [SerializeField] private float _loadSeconds;

        private void OnEnable(){
            StartCoroutine(WaitforSceneToLoad());
        }        

        private IEnumerator WaitforSceneToLoad()
        {
            yield return new WaitForSeconds(_loadSeconds);
            SceneManager.LoadScene(2);
            yield return new WaitForSeconds(_loadSeconds);
            GameManager.Game.Screen.LoadFadeScreen(this.gameObject,GameManager.Game.Screen.Game.gameObject);
        }
    }
}