using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour 
{
    public float speed;
    public float delayBetweenPoints;
    public bool cyclic;

    private int destiny;
    private bool forward;
    private Transform[] points;
    private Transform beeObj;

    void Start()
    {
        destiny = 1;
        forward = true;
        beeObj = transform.Find("BeeObj");
        Transform path = transform.Find("Path");
        
        points = new Transform[path.childCount];

        for(int i = 0; i < points.Length; i++)
            points[i] = path.GetChild(i);

        
        StartCoroutine("MoveCoroutine");
    }

    private IEnumerator MoveCoroutine()
    {
        destiny = 1;
        Vector2 startPoint = points[destiny - 1].position;
        Vector2 endPoint = points[destiny].position;
        Vector2 direction = (endPoint - startPoint).normalized;

        beeObj.position = startPoint;

        while(true)
        {
            beeObj.Translate(direction * speed * Time.deltaTime);
            // clamp
            beeObj.position = new Vector2(
                Mathf.Clamp(beeObj.position.x, Mathf.Min(startPoint.x, endPoint.x), Mathf.Max(startPoint.x, endPoint.x)),
                Mathf.Clamp(beeObj.position.y, Mathf.Min(startPoint.y, endPoint.y), Mathf.Max(startPoint.y, endPoint.y))
            );
            yield return new WaitForEndOfFrame();

            if((Vector2)beeObj.position == endPoint)
            {
                yield return new WaitForSeconds(delayBetweenPoints);
                destiny = GetNextPointIndex(destiny);
                startPoint = beeObj.position;
                endPoint = points[destiny].position;
                
                direction = (endPoint - startPoint).normalized;
            }
        }
    }

    private int GetNextPointIndex(int currentIndex)
    {
        if(cyclic)
        {
            if(currentIndex == (points.Length -1))
                return 0;
            else
                return ++currentIndex;
        }
        else
        {
            if(forward && currentIndex == (points.Length -1))
                forward = false;
            else if (!forward && currentIndex == 0)
                forward = true;

            return forward ? ++currentIndex : --currentIndex;
        }
    }

    void OnDrawGizmos()
    {
        beeObj = transform.Find("BeeObj");
        Transform path = transform.Find("Path");
        Gizmos.color = Color.cyan;

        for(int i = 0; i < path.childCount; i++)
        {
            if(i > 0)
            {
                Vector2 from = path.GetChild(i-1).position;
                Vector2 to = path.GetChild(i).position;

                Gizmos.DrawLine(from, to);
            }
        }
    }
}
