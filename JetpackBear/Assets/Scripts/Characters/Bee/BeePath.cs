using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeePath : BeeObject {

	public float delayBetweenPoints;
	public bool cyclic;

	private int nextPoint;
	private bool forward;
	private Transform[] points;

    protected override void Start () 
	{
		base.Start();

		nextPoint = 1;
		forward = true;

		Transform path = transform.Find("Path");
		points = new Transform[path.childCount];

        for(int i = 0; i < points.Length; i++)
            points[i] = path.GetChild(i);

		StartCoroutine("MoveCoroutine");
	}
	
	private IEnumerator MoveCoroutine()
    {
		// inicia os valores para o movimento
        nextPoint = 1;
        Vector2 startPoint = points[nextPoint - 1].position;
        Vector2 endPoint = points[nextPoint].position;
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

            if(direction.x != 0)
            {
                Vector2 scale = beeObj.localScale;
                scale.x = Mathf.Sign(direction.x);
                beeObj.localScale = scale;
            }

            yield return new WaitForEndOfFrame();

			// Ao chegar no ponto de destino para durante o tempo definido por "delayBetweenPoints"
			// Em seguida, é definido o próximo ponto de destino
            if((Vector2)beeObj.position == endPoint)
            {
                yield return new WaitForSeconds(delayBetweenPoints);
                nextPoint = GetNextPointIndex(nextPoint);
                startPoint = beeObj.position;
                endPoint = points[nextPoint].position;
                
                direction = (endPoint - startPoint).normalized;
            }
        }
    }

	// Retorna o índice do próximo ponto que a abelha deve seguir
	// baseado no tipo de movimento que foi marcado (cíclico ou ida e volta)
    private int GetNextPointIndex(int currentIndex)
    {
        if(cyclic) // O movimento cíclico volta ao ponto inicial quando o chega ao fim
        {
            if(currentIndex == (points.Length -1))
                return 0;
            else
                return ++currentIndex;
        }
        else // O movimento de ida e volta muda a direção do movimento quando chega no final e íncio do trajeto1
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
