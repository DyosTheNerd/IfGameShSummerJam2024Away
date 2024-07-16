using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PointSystemReader", menuName = "ScriptableObjects/PointSystem/PointSystemReader", order = 0)]
    public class PointSystemReader : ScriptableObject
    {
        public int playerNumber = 1;
        
        public string text = "This is a point system reader";
        public string ReadPointSystem()
        {
            Debug.Log(text);
            return text + 1;
        }
    }
