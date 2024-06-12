using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public enum Fighter
{
    canHit = 0,
    hasSword = 1,
    nearBadie = 2,
    hasGun = 3,
    hasAmmo = 4,
    seeBadie = 5,
    seeSword = 6,
    seeGun = 7,
    seeAmmo = 8,
    canUseWeapon = 9
}

public class AISensor : MonoBehaviour, ISense
{
    public bool canHit = false;
    public bool hasSword = false;
    public bool nearBadie = false;
    public bool hasGun = false;
    public bool hasAmmo = false;
    public bool seeBadie = false;
    public bool seeSword = false;
    public bool seeGun = false;
    public bool seeAmmo = false;
    public bool CanUseWeapon = false;

    public Memory memory;
    public Transform badie;
    public GameObject badieGO;
    public GameObject swordGO;
    public GameObject gunGO;
    public GameObject ammoGO;
    
    void Start()
    {
        if (memory == null)
        {
            memory = gameObject.GetComponentInChildren<Memory>();
        }
    }

    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.Set(Fighter.canHit, canHit);
        CheckHasSword();
        aWorldState.Set(Fighter.hasSword, hasSword);
        CheckNearBadie();
        aWorldState.Set(Fighter.nearBadie, nearBadie);
        CheckHasGun();
        aWorldState.Set(Fighter.hasGun, hasGun);
        CheckHasAmmo();
        aWorldState.Set(Fighter.hasAmmo, hasAmmo);
        CheckSeeBadie();
        aWorldState.Set(Fighter.seeBadie, seeBadie);
        CheckSeeSword();
        aWorldState.Set(Fighter.seeSword,seeSword);
        CheckSeeGun();
        aWorldState.Set(Fighter.seeGun,seeGun);
        CheckSeeAmmo();
        aWorldState.Set(Fighter.seeAmmo,seeAmmo);
        aWorldState.Set(Fighter.canUseWeapon, CanUseWeapon);
    }

    private void CheckNearBadie()
    {
        float distance = Vector3.Distance(badie.position, transform.position);
        nearBadie = distance < 2;
    }

    private void CheckHasSword()
    {
        hasSword = gameObject.GetComponent<Inventory>().hasSword;
    }
    
    private void CheckHasGun()
    {
        hasGun = gameObject.GetComponent<Inventory>().hasGun;
    }
    
    private void CheckHasAmmo()
    {
        hasAmmo = gameObject.GetComponent<Inventory>().hasAmmo;
    }

    private void CheckSeeBadie()
    {
        if(badieGO != null)
        {
            seeBadie = memory.memoryDic.ContainsKey(badieGO);
        }
    }
    private void CheckSeeSword()
    {
        if (swordGO != null)
        {
            seeSword = memory.memoryDic.ContainsKey(swordGO);
        }
    }
    private void CheckSeeGun()
    {
        if (gunGO != null)
        {
            seeGun = memory.memoryDic.ContainsKey(gunGO);
        }
    }
    private void CheckSeeAmmo()
    {
        if (ammoGO != null)
        {
            seeAmmo = memory.memoryDic.ContainsKey(ammoGO);
        }
    }
    
}
