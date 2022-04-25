using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAnimationController : MonoBehaviour
{
    Animator animator;
    int isSwimmingHash;
    int isSwimmingFastHash;
    bool set;
    int count;
    public float velocity;
    public float velocityCheck;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isSwimmingHash = Animator.StringToHash("isSwimming");
        isSwimmingFastHash = Animator.StringToHash("isSwimmingFast");
        set = false;
     }
    

    // Update is called once per frame
    void Update()
    {
        if(velocity < velocityCheck){
            animator.SetBool(isSwimmingHash, true);
        }
        else{
            animator.SetBool(isSwimmingFastHash, true);
        }
    }
}
