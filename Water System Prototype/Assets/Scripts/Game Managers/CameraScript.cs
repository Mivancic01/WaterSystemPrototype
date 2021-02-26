using UnityEngine;
using Mapbox;

public class CameraScript : MonoBehaviour
{
    public int Speed = 50;

    void Update()
    {
        //Debug.Log("UPDATING CAMERA SCRIPT!");

        float xAxisValue = Input.GetAxis("Horizontal") * Speed * 0.2f;
        float zAxisValue = Input.GetAxis("Vertical") * Speed * 0.2f;

        transform.position = new Vector3(transform.position.x + xAxisValue, transform.position.y, transform.position.z + zAxisValue);
        /*Vector3 tempPos = Camera.main.transform.position;
        tempPos.z = 10;
        Camera.main.transform.position = tempPos;*/

        if (Input.GetMouseButtonDown(0))
            TellLocation();

        //testRaycast();
    }

    void TellLocation()
    {
        Vector3 worldVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldVec.z = Camera.main.transform.localPosition.y;
        Mapbox.Unity.Map.AbstractMap absMap = GameObject.FindWithTag("AbstractMap").GetComponent<Mapbox.Unity.Map.AbstractMap>();
        
        Mapbox.Utils.Vector2d vec = absMap.WorldToGeoPosition(worldVec);
        //Debug.Log("Geo pos is: " + vec.x + ", " + vec.y);
        //Debug.Log("Mouse pos is: " + Input.mousePosition);
        //Debug.Log("Mouse World pos is: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    void testRaycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Name = " + hit.collider.name);
                Debug.Log("Tag = " + hit.collider.tag);
                Debug.Log("Hit Point = " + hit.point);
                Debug.Log("Object position = " + hit.collider.gameObject.transform.position);
                Debug.Log("--------------");
            }
            else
                Debug.Log("There is no hit!");
        }
    }
}