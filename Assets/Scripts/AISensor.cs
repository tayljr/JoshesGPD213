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
    
    public Transform badie;
    
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
        aWorldState.Set(Fighter.seeBadie, seeBadie);
        aWorldState.Set(Fighter.seeSword,seeSword);
        aWorldState.Set(Fighter.seeGun,seeGun);
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
}
