using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjects : MonoBehaviour
{
    public float waitTime = 1;
    public float moveSpeed = 1;
    public Transform[] moveToPositions;
    int currentPosition;

    PlayerController playerController;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(MoveInDirection());
        playerController = FindObjectOfType<PlayerController>();
    }

    IEnumerator MoveInDirection()
    {
        Vector3 _newPos = moveToPositions[currentPosition].localPosition;
        while (Vector3.Distance(transform.localPosition,_newPos) > 0.1f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _newPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(waitTime);

        if (currentPosition != moveToPositions.Length - 1)
            currentPosition = currentPosition + 1;

        else currentPosition = 0;

        StartCoroutine(MoveInDirection());
    }


    
}
