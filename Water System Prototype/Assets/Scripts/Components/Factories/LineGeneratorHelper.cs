using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGeneratorHelper : MonoBehaviour
{
    public struct EndPositionVariables
    {
        public EndPositionVariables(GameObject pLine, Vector3 pStartPosition, Vector3 pEndPos, Vector3 pOriginalScale,
            float pOriginalWidth, float pOldZAngle)
        {
            line = pLine;
            startPosition = pStartPosition;
            endPos = pEndPos;
            originalScale = pOriginalScale;
            originalWidth = pOriginalWidth;
            oldZAngle = pOldZAngle;
        }

        public GameObject line;
        public Vector3 startPosition, endPos, originalScale;
        public float originalWidth, oldZAngle;
    }

    public static GameObject SetEndPosition(EndPositionVariables vars)
    {
        var height = vars.endPos.z - vars.startPosition.z;
        var width = vars.endPos.x - vars.startPosition.x;
        var zAngle = Mathf.Atan2(height, width) * Mathf.Rad2Deg;

        vars.line = UpdateScale(vars.line, Mathf.Sqrt(width * width + height * height), vars.originalScale, vars.originalWidth);
        vars.line = UpdateRotation(vars.line, zAngle, vars.oldZAngle);
        //vars.line = UpdatePosition(vars.line);

        return vars.line;
    }
    public static (GameObject, float) SetEndPositionAndGetAngle(EndPositionVariables vars)
    {
        var height = vars.endPos.z - vars.startPosition.z;
        var width = vars.endPos.x - vars.startPosition.x;
        var zAngle = Mathf.Atan2(height, width) * Mathf.Rad2Deg;

        vars.line = UpdateScale(vars.line, Mathf.Sqrt(width * width + height * height), vars.originalScale, vars.originalWidth);
        vars.line = UpdateRotation(vars.line, zAngle, vars.oldZAngle);
        //vars.line = UpdatePosition(vars.line);

        return (vars.line, zAngle);
    }


    private static GameObject UpdateScale(GameObject line, float hipotenusa, Vector3 originalScale, float originalWidth)
    {
        Vector3 newScale = originalScale;
        newScale.x *= hipotenusa / originalWidth;
        line.transform.localScale = newScale;

        return line;
    }

    private static GameObject UpdateRotation(GameObject line, float zAngle, float oldZAngle)
    {
        line.transform.Rotate(0.0f, 0.0f, -oldZAngle, Space.Self);
        line.transform.Rotate(0.0f, 0.0f, zAngle, Space.Self);
        oldZAngle = zAngle;

        return line;
    }

    private static GameObject UpdatePosition(GameObject line)
    {
        var tempPos = line.transform.position;
        tempPos.z = 9f;
        line.transform.position = tempPos;

        return line;
    }

    public static GameObject UpdateWidth(GameObject line, float diameter)
    {
        var scale = line.transform.localScale;

        const float unit_diameter_constant = 150.0f;
        float converter = diameter / unit_diameter_constant;
        scale.y *= converter;

        line.transform.localScale = scale;
        return line;
    }
}
