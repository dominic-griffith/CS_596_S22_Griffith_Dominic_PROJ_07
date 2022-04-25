using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAnimationController : MonoBehaviour
{
    Animator animator;
    int isFastHash;
    public float velocity;
    public float velocityCheck;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isFastHash = Animator.StringToHash("isFast");
     }
    

    // Update is called once per frame
    void Update()
    {
        if(velocity < velocityCheck){
            animator.SetBool(isFastHash, false);
        }
        else{
            animator.SetBool(isFastHash, true);
        }
    }
}
