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
    {
        return "Life";
    }
    public int MoreLifePrice(int upgrade)
    {
        return upgrade += 10;
    }



    public void MoreDamage(ZombieMain zombie)
        => zombie.attackDamage += 1;
    public string MoreDamageName()
    {
        return "Damage";
    }
    public int MoreDamagePrice(int upgrade)
    {
        return upgrade += 10;
    }



    public void MoreSpeed(ZombieMain zombie)
        => zombie.movespeed += 1;
    public string MoreSpeedName()
    {
        return "Move Speed";
    }
    public int MoreSpeedPrice(int upgrade)
    {
        return upgrade += 10;
    }
}