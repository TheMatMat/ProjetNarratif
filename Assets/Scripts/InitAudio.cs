using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamSeven
{

    public class InitAudio : MonoBehaviour
    {
        void Start()
        {
            GameManager.instance?.GoToSnapshot("MenuSnapshot", 1);
        }
    }
}
