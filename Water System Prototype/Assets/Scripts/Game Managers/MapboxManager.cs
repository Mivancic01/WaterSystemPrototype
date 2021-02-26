using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;

public class MapboxManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMapType(int type)
    {
        AbstractMap mapbox_handler = GetComponent<AbstractMap>();

        if(type == 0)
            mapbox_handler.ImageLayer.SetLayerSource(ImagerySourceType.MapboxStreets);
        else if (type == 1)
            mapbox_handler.ImageLayer.SetLayerSource(ImagerySourceType.MapboxLight);
        else if (type == 2)
            mapbox_handler.ImageLayer.SetLayerSource(ImagerySourceType.MapboxSatelliteStreet);
    }
}
