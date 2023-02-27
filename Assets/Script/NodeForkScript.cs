using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeForkScript : NodeScript
{
    //An array of the possible nodes that this one will connect to
    public NodeScript[] targets;

    //Returns a random node from targets
    public override NodeScript GetNext()
    {
        //If the targets array is empty or undefined, we return null
        if (targets == null || targets.Length == 0)
        {
            return null;
        }

        //A random number is generated within the bounds of the targets array
        int rng = Random.Range(0, targets.Length);

        //A random element of targets is returned
        return targets[rng];
    }

    //A callback function that gets called when its time to draw gizmos
    private void OnDrawGizmos()
    {
        //Both of these statements are equivalent
        if (targets == null)
            return;

        //Sets the color of the gizmos 
        Gizmos.color = Color.yellow;

        foreach (var node in targets)
        {
            //Draw a line between the position of the object and the position of the "next" object
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }
}
