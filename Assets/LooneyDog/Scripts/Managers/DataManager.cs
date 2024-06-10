using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSG.iOSKeychain;
namespace LooneyDog
{

    public class DataManager : MonoBehaviour
    {
        public PlayerData player { get; private set; } = new PlayerData();
        [SerializeField]
        private string master_Keyword;

        private void Awake()
        {
            Debug.Log("Data Manager is Awake");
        }

        private void OnEnable()
        {
            Debug.Log("Data Manager is Enabled");
            Read(player);
        }

        void Start()
        {
            Debug.Log("Data Manager is Started");
        }

        void OnDisable()
        {
            Write(player);
        }

        void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                Write(player);
            }
        }

        void Read<T>(T obj)
        {
            Debug.Log("Reading value from, master keyword: " + master_Keyword);
            
            string dataAsJson = Keychain.GetValue(master_Keyword);
            JsonUtility.FromJsonOverwrite(dataAsJson, obj);
            Debug.Log("Got json value: " + dataAsJson);
            
        }

        void Write<T>(T obj)
        {

            string dataAsJson = JsonUtility.ToJson(obj);
            Keychain.SetValue(master_Keyword, dataAsJson);
            Debug.Log("Writing json value: " + dataAsJson);
        }
    }
}