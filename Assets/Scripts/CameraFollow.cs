using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Transform leftBounds;
    public Transform rightBounds;
    public Transform bottomBounds;
    public Transform topBounds;

    public float smoothDampTime = 0.15f;
    private Vector3 smoothDampVelocity = Vector3.zero;

    public float camWidth, camHieght, levelMinX, levelMaxX, levelHightMin, levelHightMax;

    // Use this for initialization
    void Start()
    {

        camHieght = Camera.main.orthographicSize * 2;
        camWidth = camHieght * Camera.main.aspect;

        float leftBoundWidth = leftBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;
        float rightBoundWidth = rightBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;
        float bottomBoundWidth = bottomBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2;
        float topBoundWidth = topBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2;

        levelMinX = leftBounds.position.x + leftBoundWidth + (camWidth / 2);
        levelMaxX = rightBounds.position.x - rightBoundWidth - (camWidth / 2);
        levelHightMin = bottomBounds.position.y + bottomBoundWidth + (camHieght / 2);
        levelHightMax = topBounds.position.y + topBoundWidth + (camHieght / 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            float targetX = Mathf.Max(levelMinX, Mathf.Min(levelMaxX, target.position.x));
            float targetY = Mathf.Max(levelHightMin, Mathf.Min(levelHightMax, target.position.y));

            float x = Mathf.SmoothDamp(transform.position.x, targetX, ref smoothDampVelocity.x, smoothDampTime);
            float y = Mathf.SmoothDamp(transform.position.y, targetY, ref smoothDampVelocity.y, smoothDampTime);

            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}
