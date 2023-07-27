using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Upgrades
{
    ZombieMain zombie;
    public string name;



    public void MoreLife(ZombieMain zombie)
        => zombie.maxlife += 10;
    public string MoreLifeName()
        => "Life Up ❤";




    public void MoreDamage(ZombieMain zombie)
        => zombie.attackDamage += 1;
    public string MoreDamageName()
        => "Damage Up 👊";
    
    


    public void MoreSpeed(ZombieMain zombie)
        => zombie.movespeed += 1;
    public string MoreSpeedName()
        => "Move Speed Up 🏃‍♂️";




    public void MoreChance(ZombieMain zombie)
        => zombie.chance += 1;
    public string MoreChanceName()
        => "Chance Up 🍀";



    public void MoreCure(ZombieMain zombie)
        => zombie.cure += 10;
    public string MoreCureName()
        => "Cure Up 💊";



    public void MoreZombiesLife(ZombieMain zombie)
        => zombie.zombiesLife += 10;
    public string MoreZombiesLifeName()
        => "Zombies Life 💚";





    public int UpPrice(int upgrade)
        => upgrade += 10;
    
}