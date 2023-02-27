using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    //The node that this one connects to
    public NodeScript next;

    //Will return the next node
    public virtual NodeScript GetNext()
    {
        return next;
    }

    //A callback function that gets called when its time to draw gizmos
    private void OnDrawGizmos()
    {
        //Both of these statements are equivalent
        if (next == null)
        //if (!next)
            return;

        //Sets the color of the gizmos 
        Gizmos.color = Color.yellow;

        //Draw a line between the position of the object and the position of the "next" object
        Gizmos.DrawLine(transform.position, next.transform.position);
    }
}
