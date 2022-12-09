using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
   private void Start() 
   {
      InitVariables();
   }
   
   public override void Die()
   {
      base.Die();
      Destroy(gameObject);
   }

   public override void InitVariables()
   {
      maxHealth = 1;
      SetHealthTo(maxHealth);
      isDead = false;
   }
}
