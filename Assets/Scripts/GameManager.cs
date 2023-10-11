using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    static private float _gameSpeed = .1f;
    static public float gameSpeed {
        get { return _gameSpeed; }
        private set { _gameSpeed = value; }
    }

    public Transform playerTransform;

    public void End() {

    }
}
