using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Upgrade
{
    public string name;
    public string desc;
    public Sprite sprite;
    public abstract void ApplyUpgrade();
}

public class DashDamageUp : Upgrade
{
    public DashDamageUp(Sprite sprite)
    {
        name = "Dash Damage Up";
        if (UpgradeManager.Instance.selectedUpgrades[0] == 2)
        {
            desc = "Permanently dash";
        }
        else
        {
            desc = "+3 to dash damage";
        }
        this.sprite = sprite;
    }
    
    public override void ApplyUpgrade()
    {
        if (UpgradeManager.Instance.selectedUpgrades[0] == 2)
        {
            GameManager.Instance.player.GetComponent<PlayerController>().DashUpgrade();
        }
        else
        {
            GameManager.Instance.shield.GetComponent<ShieldController>().dashDamage += 3;
        }
        UpgradeManager.Instance.selectedUpgrades[0]++;
    }
}

public class ThornDamageUp : Upgrade
{
    public ThornDamageUp(Sprite sprite)
    {
        name = "Thorn Damage Up";
        if (UpgradeManager.Instance.selectedUpgrades[1] == 2)
        {
            desc = "Get a 2nd shield";
        }
        else
        {
            desc = "+1 to thorn damage";
        }
        
        this.sprite = sprite;
    }
    
    public override void ApplyUpgrade()
    {
        if (UpgradeManager.Instance.selectedUpgrades[1] == 2)
        {
            GameManager.Instance.player.GetComponent<PlayerController>().ShieldUpgrade();
        }
        else
        {
            GameManager.Instance.shield.GetComponent<ShieldController>().thornDamage+= 0.5f;
        }
        UpgradeManager.Instance.selectedUpgrades[1]++;
    }
}

public class ShieldSizeUp : Upgrade
{
    public ShieldSizeUp(Sprite sprite)
    {
        name = "Shield size Up";
        if (UpgradeManager.Instance.selectedUpgrades[2] == 2)
        {
            desc = "Shield now deflect bullets";
        }
        else
        {
            desc = "shield size increase";
        }
        this.sprite = sprite;
    }
    
    public override void ApplyUpgrade()
    {
        if (UpgradeManager.Instance.selectedUpgrades[2] == 2)
        {
            GameManager.Instance.shield.GetComponent<ShieldController>().deflectBullet = true;
        }
        else
        {
            GameManager.Instance.shield.transform.localScale += new Vector3(0f, 0.2f, 0f);
        }
        UpgradeManager.Instance.selectedUpgrades[2]++;
        
    }
}

public class BurnEffect : Upgrade
{
    public BurnEffect(Sprite sprite)
    {
        name = "Burn Effect";
        if (UpgradeManager.Instance.selectedUpgrades[3] == 0)
        {
            desc = "Leave a trail of flames";
        }
        else if (UpgradeManager.Instance.selectedUpgrades[3] == 2)
        {
            desc = "increase spd, spawn more fire";
        }
        else
        {
            desc = "+1 to burn damage";
        }
        
        this.sprite = sprite;
    }
    
    public override void ApplyUpgrade()
    {
        GameManager.Instance.player.GetComponent<PlayerController>().UpgradeBurn(UpgradeManager.Instance.selectedUpgrades[3]);
        UpgradeManager.Instance.selectedUpgrades[3]++;
    }
}


public class FreezeEffect : Upgrade
{
    public FreezeEffect(Sprite sprite)
    {
        name = "Freeze Effect";
        if (UpgradeManager.Instance.selectedUpgrades[4] == 0)
        {
            desc = "Release a cloud of ice that slows";
        }
        else if (UpgradeManager.Instance.selectedUpgrades[4] == 2)
        {
            desc = "reduce cooldown, cloud now deals dmg";
        }
        else
        {
            desc = "increase effect range";
        }
        
        this.sprite = sprite;
    }
    
    public override void ApplyUpgrade()
    {
        GameManager.Instance.player.GetComponent<PlayerController>().UpgradeFreeze(UpgradeManager.Instance.selectedUpgrades[4]);
        UpgradeManager.Instance.selectedUpgrades[4]++;
    }
}

public class HealthUp : Upgrade
{
    public HealthUp(Sprite sprite)
    {
        name = "Health Up";
        if (UpgradeManager.Instance.selectedUpgrades[5] == 2)
        {
            desc = "dash dmg now scales with health";
        }
        else
        {
            desc = "+1 to health";
        }
        this.sprite = sprite;
    }
    
    public override void ApplyUpgrade()
    {
        if (UpgradeManager.Instance.selectedUpgrades[5] == 2)
        {
            GameManager.Instance.shield.GetComponent<ShieldController>().healthScaling = true;
        }
        else
        {
            GameManager.Instance.player.GetComponent<PlayerController>().health++;
        }
        UpgradeManager.Instance.selectedUpgrades[5]++;
    }
}