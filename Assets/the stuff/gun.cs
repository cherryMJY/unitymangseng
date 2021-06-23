using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gun : MonoBehaviour
{
    public float timeBetweenShots;
    public bool oneShotAtATime;
    public bool active;
    public Transform shootPos;
    private bool ready;
    public GameObject line;
    public Rigidbody rb;
    public Collider colli;
    public Camera cam;
    public Image aimer;
    public Material lineMaterial;
    public bool enemyGun;

    // Start is called before the first frame update
    void Start()
    {
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shoot(Transform target)
    {
        if (ready && active)
        {
            if (!oneShotAtATime)
            {
                StartCoroutine(wait());
            }

            RaycastHit hit;
            Ray vRay = cam.ScreenPointToRay(aimer.transform.position);
            if (Physics.Raycast(vRay, out hit))
            {
                if (hit.transform.gameObject.layer == 9 || hit.transform.gameObject.layer == 8)
                {
                    hit.transform.parent.GetComponent<death>().die();
                }
                Rigidbody hitRb = hit.transform.GetComponent<Rigidbody>();
                if (hitRb)
                {
                    hitRb.AddForce(cam.transform.forward * 50, ForceMode.Impulse);
                }
                StartCoroutine(makeLine(hit));
            }
        }
    }

    public void Activate(Transform hand)
    {
        active = true;
        rb.isKinematic = true;
        colli.enabled = false;
        transform.parent = hand.transform.transform;
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localEulerAngles = new Vector3(0, 180, 0);
    }

    public void DeActivate()
    {
        active = false;
        transform.parent = null;
        rb.isKinematic = false;
        colli.enabled = true;
    }

    public IEnumerator wait()
    {
        ready = false;
        yield return new WaitForSeconds(timeBetweenShots);
        ready = true;
    }
    public IEnumerator makeLine(RaycastHit hitplace)
    {
        var go = new GameObject();
        var lr = go.AddComponent<LineRenderer>();

        lr.material = lineMaterial;
        lr.SetPosition(0, shootPos.position);
        lr.SetPosition(1, hitplace.point);
        lr.SetColors(Color.red, Color.white);
        lr.SetWidth(0.1f, 0.1f);
        yield return new WaitForSeconds(0.07f);
        Destroy(go);
    }
}
