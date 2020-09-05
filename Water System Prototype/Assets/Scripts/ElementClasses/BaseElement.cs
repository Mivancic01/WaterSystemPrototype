using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Elements
{
    public class BaseElement : MonoBehaviour
    {
        public GameObject propertiesWindowPrefab, mapIconPrefab;
        private GameObject propertiesWindowObj, mapIconObj;
        public int typeID;
        public Vector3 position;

        public BaseElement(int id, Vector3 pos)
        {
            typeID = id;
            position = pos;
        }

        public void Initialize()
        {
            mapIconObj = Instantiate(mapIconPrefab, position, Quaternion.identity);
            propertiesWindowObj = Instantiate(propertiesWindowPrefab, GameObject.FindWithTag("Canvas").transform);
            ChangeWindowVisibility(false);
        }

        public void ChangeVisibility(bool isVisible)
        {
            mapIconObj.SetActive(isVisible);
        }

        public void ChangeWindowVisibility(bool isVisible)
        {
            propertiesWindowObj.SetActive(isVisible);
        }

        public void DestroyElement()
        {
            GameObject.Destroy(mapIconObj);
            GameObject.Destroy(propertiesWindowObj);
        }
    }
}


