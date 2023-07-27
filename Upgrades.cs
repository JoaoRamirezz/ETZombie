using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Upgrades
{
    ZombieMain zombie;
    public string name;
    int MoreLifePrice;
    int MoreDamagePrice;
    int MoreMovePrice;



    public void MoreLife(ZombieMain zombie)
    {
        zombie.maxlife += 10;
        MoreLifePrice += 10;
    }
    public string MoreLifeName()
        => "Life";



    public void MoreDamage(ZombieMain zombie)
    {
        zombie.attackDamage += 1;
        MoreDamagePrice += 10;
    }
    public string MoreDamageName()
        => "Damage";



    public void MoreSpeed(ZombieMain zombie)
    {
        zombie.movespeed += 1;
        MoreMovePrice += 10;
    }
    public string MoreSpeedName()
        => "Move Speed";
}