using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : MonoBehaviour
{
    public float pushSpeed = 5f;
    private bool isMoving = false;

    public void Push(Vector2 direction, System.Action onComplete)
    {
        if (!isMoving)
        {
            StartCoroutine(MoveTile(direction, onComplete));
        }
    }

    private IEnumerator MoveTile(Vector2 direction, System.Action onComplete)
    {
        isMoving = true;

        Vector2 startPos = transform.position;
        Vector2 targetPos = startPos + direction;

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            transform.position = Vector2.Lerp(startPos, targetPos, elapsedTime);
            elapsedTime += Time.deltaTime * pushSpeed;
            yield return null;
        }

        transform.position = targetPos; 
        isMoving = false;

        onComplete?.Invoke();
    }
}
