using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyPlayer : MonoBehaviour
{
    public Joystick joy;
    public bool move = false;

    Rigidbody rb;
    public Animator anim;
    [SerializeField]
    bool grounded = false;

    [SerializeField]
    AudioSource walkSourceSound;

    bool jump1AnimCan = true;
    bool jump2AnimCan = true;
    bool runAnimCan = true;
    bool stayAnimCan = true;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (grounded) {
            if (move/* && Controller.globalFinish == false*/) {
                if (runAnimCan) {
                    anim.SetTrigger("run");
                    runAnimCan = false;
                }
            } else {
                if (stayAnimCan) {
                    anim.SetTrigger("stay");
                    stayAnimCan = false;
                    runAnimCan = true;
                }
            }
        } else {
            if (rb.velocity.y > 0.5f) {
                if (jump1AnimCan) {
                    anim.speed = 1f;
                    anim.CrossFade("Armature|Jump1", 0.6f, 0, 2f);
                    jump1AnimCan = false;
                    stayAnimCan = true;
                    runAnimCan = true;
                }
            } else if (rb.velocity.y < -0.5f) {
                if (jump2AnimCan) {
                    anim.speed = 1f;
                    anim.CrossFade("Armature|Jump2", 0.6f, 0, 2f);
                    jump2AnimCan = false;

                    stayAnimCan = true;
                    runAnimCan = true;
                }
            }
        }

        if (Controller.globalFinish == false) {
            if ((joy.Vertical < 0.35f && joy.Vertical > -0.35f) && (joy.Horizontal < 0.35f && joy.Horizontal > -0.35f)) {
                if (move) {
                    StopGo();
                } else {
                    rb.velocity = new Vector3(rb.velocity.x / 1.1f, rb.velocity.y, rb.velocity.z / 1.1f);
                }
            } else {
                if (move == false) {
                    Go();
                }

                float dist = Mathf.Abs(Vector2.Distance(joy.Direction, new Vector2(0, 0)));

                transform.eulerAngles = new Vector3(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y + Mathf.Atan2(joy.Direction.x, joy.Direction.y) * Mathf.Rad2Deg, transform.eulerAngles.z);
                rb.velocity = new Vector3((transform.forward.x * dist) * Time.deltaTime * 600, rb.velocity.y, (transform.forward.z * dist) * Time.deltaTime * 600);
                //rb.velocity = Vector3.SmoothDamp(rb.velocity, finalVel, ref vel, 0.01f);
                anim.speed = dist * 1.05f;
            }
        }

        if (move && Controller.globalSound && Controller.globalFinish == false) {
            walkSourceSound.pitch = 0.7f+ (Mathf.Abs(Vector2.Distance(joy.Direction, new Vector2(0, 0))) /2.5f);

            if (!walkSourceSound.isPlaying && grounded) {
                walkSourceSound.Play();
            }
        } else {
            if (walkSourceSound.isPlaying) {
                walkSourceSound.Stop();
            }
        }




        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }
    }

    public void Go() {
        if (runAnimCan && grounded == false) {
            //anim.SetTrigger("run");


            jump1AnimCan = true;
            jump2AnimCan = true;
            runAnimCan = false;
            stayAnimCan = true;
        }
        move = true;
    }
    public void StopGo() {
        anim.speed = 1;
        move = false;
        stayAnimCan = true;
    }

    public void Jump() {

        Vector3 point = new Vector3(transform.position.x, transform.position.y - 1.8f, transform.position.z);
        Collider[] hit = Physics.OverlapBox(point, new Vector3(0.2f, 0.2f, 0.2f), Quaternion.identity);

        bool can = false;

        if (hit.Length > 0) {
            foreach (Collider r in hit) {
                if (r.transform.tag == "Block") {
                    if (r.GetComponent<Collider>().isTrigger == false) {
                        can = true;
                    }
                }
            }
        }

        if (can) {

            rb.velocity = new Vector3(anim.velocity.x, 17f, anim.velocity.z);
            stayAnimCan = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "trampoline") {
            rb.velocity = new Vector3(0, 38, 0);
        }

        if (other.name == "FINISH") {
            if (Controller.globalFinish == false) {

                StartCoroutine(WaitAndShowCompleted());

                Controller.globalFinish = true;
            }
        }
    }
    private void OnTriggerStay(Collider other) {
        if (other.tag == "Block") {
            grounded = true;

            jump1AnimCan = true;
            jump2AnimCan = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Block") {
            grounded = false;
        }
    }

    IEnumerator WaitAndShowCompleted() {
        yield return new WaitForSeconds(0.5f);

        Camera.main.GetComponent<Controller>().ShowCompletedUI();
    }
}
