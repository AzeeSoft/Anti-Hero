using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideKickController : MonoBehaviour {

    public float MoveSpeed = 2f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Move(Vector3 dir)
    {
        Vector3 newScale = HelperUtilities.CloneVector3(transform.localScale);
        Vector3 newPos = HelperUtilities.CloneVector3(transform.position);

        float delX;
        if (dir.x < 0)
        {
            delX = -MoveSpeed;
            newScale.x = Mathf.Abs(newScale.x);
        }
        else if (dir.x > 0)
        {
            delX = MoveSpeed;
            newScale.x = -Mathf.Abs(newScale.x);
        }
        else
        {
            delX = 0;
        }

        newPos.x += delX;

        transform.position = Vector3.Lerp(transform.position, newPos, Time.fixedDeltaTime);
        transform.localScale = newScale;
    }
}
