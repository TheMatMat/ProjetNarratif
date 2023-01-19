using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamSeven
{
    public class MobileControllerTest : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.rotation = GyroToUnity(Input.gyro.attitude);
        }

        private static Quaternion GyroToUnity(Quaternion q)
        {
            return new Quaternion(q.x, q.y, -q.z, -q.w);
        }
    }
}
