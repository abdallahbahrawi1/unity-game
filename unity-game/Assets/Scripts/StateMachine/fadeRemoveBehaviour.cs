using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class fadeRemoveBehaviour : StateMachineBehaviour
{
    public float fadetime = 0.5f;
    float _timeElapsed = 0;
    SpriteRenderer spriteRenderer;
    Color startColor;
    GameObject objToRemove;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       _timeElapsed = 0f;
       spriteRenderer = animator.GetComponent<SpriteRenderer>();
       objToRemove = animator.gameObject;
       startColor = spriteRenderer.color;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       _timeElapsed += Time.deltaTime;
       float newAlpha = startColor.a * (1 - (_timeElapsed / fadetime));
       spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
       if(_timeElapsed > fadetime){
        Destroy(objToRemove);
       }
    }
}
