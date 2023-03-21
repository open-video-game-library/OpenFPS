using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PositionConstraintAutoOff : MonoBehaviour
{
    private ConstraintSource myConstraintSource;
    private PositionConstraint myPositionConstraint;

    // Start is called before the first frame update
    void Start()
    {
        this.myPositionConstraint = GetComponent<PositionConstraint>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (this.transform.parent == null)
        {
            this.myPositionConstraint.weight = 0f;
        }
        
    }

    public void AddConstraint(Transform parent)
    {
        /*
        //this.myPositionConstraint.RemoveSource(0);
        this.myConstraintSource.sourceTransform = parent;
        this.myPositionConstraint.AddSource(this.myConstraintSource);   // Constraintの参照元を追加
        this.myPositionConstraint.translationOffset = Vector3.zero;     // オフセットを0に
        this.myPositionConstraint.weight = 1f;
        */
    }
}
