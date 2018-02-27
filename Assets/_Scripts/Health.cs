using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour {

    public float health;
    public float maxHealth;
    public bool invincible = false;
    public float armor = 1;
	public bool isAlive = true;

    // Callbacks for when this object dies. Register callbacks using: OnDie += functionname;
    public Action OnDie;
    public Action<float> OnChangeHealth;

	void Start () {
        health = maxHealth;
	}

    /// <summary>
    /// Do damage to entity. Use positive dmg vals to do damage.
    /// </summary>
    /// <param name="dmg"></param>
    /// <param name="attacker"></param>
    public void Damage(float dmg, GameObject attacker=null) {
        dmg = invincible ? 0 : dmg * armor;
        ChangeHealth(-dmg);
    }


    /// <summary>
    /// Heal entity. Use positive hp vals to heal.
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="healer"></param>
    public void Heal(float hp, GameObject healer=null) {
        ChangeHealth(hp);
    }

    void ChangeHealth(float hp) {
        health += hp;
        if (isAlive && health <= 0) {
			isAlive = false;
			Die();
        }
    }

    /// <summary>
    /// Apply poisons/damage/heals over time.
    /// </summary>
    /// <param name="dot">Damage per second to apply. Negative values heal over time</param>
    /// <param name="duration">How long to apply for. Total dmg applied is duration * dot</param>
    /// <param name="tickDuration">OPTIONAL: delay between each tick of dmg</param>
    public void Dot(float dot, float duration, float tickDuration=0) { StartCoroutine(DotRoutine(dot, duration, tickDuration)); }
    IEnumerator DotRoutine(float dot, float duration, float tickDuration=0) { //This is wrapped by Dot(...)
        float endDotTime = Time.time + duration;
        float appliedDmg = 0;
        while (Time.time < endDotTime) {
            float d = dot * (tickDuration == 0 ? Time.deltaTime : tickDuration);
            ChangeHealth(-d);
            appliedDmg += d;
            if (tickDuration == 0)
                yield return null;
            else {
                yield return new WaitForSeconds(tickDuration);
            }
        }
        // ensure total dmg applied is correct and apply any missing dmg due to time being in frames.
        float missingDmg = (dot * duration) - (appliedDmg);
        ChangeHealth(-missingDmg);
    }


    /// <summary>
    /// Set invincible or not.
    /// </summary>
    /// <param name="state">True or false</param>
    /// <param name="duration">OPTIONAL: if the state should be temporary and revert after duration</param>
    public void SetInvincible(bool state, float duration=-1) {
        if (state) {
            SetInvincibleTrue();
            CancelInvoke("SetInvincibleFalse");
            if (duration > 0) Invoke("SetInvincibleFalse", duration);
        }
        else {
            SetInvincibleFalse();
            CancelInvoke("SetInvincibleTrue");
            if (duration > 0) Invoke("SetInvincibleTrue", duration);
        }
    }
    void SetInvincibleTrue() { invincible = true; }    // Called By Invoke()
    void SetInvincibleFalse() { invincible = false; }  // Called By Invoke()
	

    public void Die() {
        //OnDie();
		StartCoroutine(DestroyAfterTime(1f));
	}

	IEnumerator DestroyAfterTime(float time)
	{
		yield return new WaitForSeconds(time);

		Destroy (this.gameObject);
	}

    public void OnDestroy() {
        
    }


}
