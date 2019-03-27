using UnityEngine;

public class RayGun : GunParent
{
    public LineRenderer gunLine;
    public LineRenderer uiLine;

    private UI_Button hover = null;

    public float effectsDisplayTime = 0.2f; // how long lineRenderer will stay in a scene
    int shootableMask; //enemy or should be shootable
    public AudioSource aud;

    private static GameObject hitParticle = null;
    private static GameObject flashParticle = null;
    private static GameObject ChargedLaser = null;
    private static GameObject ChargingLaserParticle = null;


    private void Start()
    {
        //gunLine = GetComponent<LineRenderer>();
        if (hitParticle == null)
            hitParticle = Resources.Load("Particle_LaserHit", typeof(GameObject)) as GameObject;

        if (flashParticle == null)
            flashParticle = Resources.Load("Particle_LaserFlash", typeof(GameObject)) as GameObject;

        if (ChargedLaser == null)
            ChargedLaser = Resources.Load("GO_ChargedLaser", typeof(GameObject)) as GameObject;

        if (ChargingLaserParticle == null)
            ChargingLaserParticle = Resources.Load("Particle_LaserCharging", typeof(GameObject)) as GameObject;
    }

    public override void Update()
    {
        base.Update();
        if (timer >= reFireTime * effectsDisplayTime)
        {
            DisableEffects();
        }

        if (isCharging)
        {
            chargingLaser += Time.deltaTime;
            damage += (damage + (chargingLaser * 2.0f));
            GameObject goCharingLaserParticle = (GameObject)Instantiate(ChargingLaserParticle, transform.position, transform.rotation);
            Destroy(goCharingLaserParticle, 0.2f);
        }
        else
        {
            chargingLaser = 0.0f;
            damage = 1.0f;
        }

        if (Time.deltaTime == 0)
        {
            uiLine.enabled = true;

            uiLine.SetPosition(0, transform.position);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, range, 1 << 11))
            {
                uiLine.SetPosition(1, hit.point);
                hover = hit.transform.GetComponent<UI_Button>();
                hover.RunFunction();
                //InstantiateParticle(hit);
            }
            else
            {
                if(hover)
                    hover.ExitHover();
                hover = null;
                uiLine.SetPosition(1, transform.position + transform.forward * range);
            }
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
    }

    protected override void Shoot()
    {
        base.Shoot();

        if (isCharging)
        {
            if (chargingLaser < minChargingLaser)
            {
                NormalShoot();
            }

            if (chargingLaser > minChargingLaser)
            {
                ChargedLaserShoot();
            }
        }
        else
        {
            UIShoot();
        }

    }

    private void ChargedLaserShoot()
    {
        GameObject goChargedLaser = (GameObject)Instantiate(ChargedLaser, transform.position, transform.rotation);
        goChargedLaser.GetComponent<Bullet>().damage = damage;
    }
    private void UIShoot()
    {
        //FlashLight
        GameObject goFlash = (GameObject)Instantiate(flashParticle, transform.position, transform.rotation);
        Destroy(goFlash, 0.22f);


        aud.Play();
        gunLine.enabled = true;
        Transform shotTransform = transform;
        gunLine.SetPosition(0, shotTransform.position);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range, 1<<5))
        {
            gunLine.SetPosition(1, hit.point);
            InstantiateParticle(hit);

            hit.transform.GetComponent<UI_Button>().RunFunction();
        }
        else
        {
            gunLine.SetPosition(1, shotTransform.position + shotTransform.forward * range);
        }
    }
    private void NormalShoot()
    {
        //FlashLight
        GameObject goFlash = (GameObject)Instantiate(flashParticle, transform.position, transform.rotation);
        Destroy(goFlash, 0.22f);


        aud.Play();
        gunLine.enabled = true;
        Transform shotTransform = transform;
        gunLine.SetPosition(0, shotTransform.position);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            gunLine.SetPosition(1, hit.point);
            InstantiateParticle(hit);

            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<Enemy>().TakeDamage(damage);
            }
            else if (hit.transform.tag == "Boss")
            {
                hit.transform.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
        else
        {
            gunLine.SetPosition(1, shotTransform.position + shotTransform.forward * range);
        }
    }

    private void InstantiateParticle(RaycastHit hit)
    {
        GameObject tempParticle = (GameObject)Instantiate(hitParticle, hit.point, hit.transform.rotation);
        tempParticle.transform.LookAt(transform, Vector3.up);
        tempParticle.SetActive(true);
        Destroy(tempParticle, 1.4f);
    }

}