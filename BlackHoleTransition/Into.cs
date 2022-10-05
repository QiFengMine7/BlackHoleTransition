using System.Collections;
using MSCLoader;
using UnityEngine;
using System;

namespace BlackHoleTransition
{
    public class Into : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "Transition Core(Clone)" && GameObject.Find("链接(Clone)").transform.localPosition.x == 0)
            {
                
                gameObject.transform.Find("跃迁核心").gameObject.transform.localScale = new Vector3(1,1,1);
                gameObject.transform.Find("跃迁核心已用").gameObject.transform.localScale = new Vector3(0, 0, 0);
                GameObject.Find("链接(Clone)").transform.localPosition = new Vector3(1, 0, 0);
                Destroy(other.gameObject);
            }
        }

        // Use this for initialization
       
    }
}