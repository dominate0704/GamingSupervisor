package Dota2Parser.Dota2Parser;

import skadistats.clarity.model.FieldPath;
import skadistats.clarity.model.Entity;

public class Hero
{
    private final int itemCount = 17; // Clarity analyzer has 17 spots for items
    
    public FieldPath x;
    public FieldPath y;
    public FieldPath z;
    public FieldPath health;
    public FieldPath playerID;
    public FieldPath level;
    public FieldPath mana;
    public FieldPath strength;
    public FieldPath intellect;
    public FieldPath maxHealth;
    public FieldPath manaRegen;
    public FieldPath healthRegen;
    public FieldPath movementSpeed;
    public FieldPath damageMin;
    public FieldPath damageMax;
    public FieldPath items[];
    
    public Hero(Entity e)
    {
        x = e.getDtClass().getFieldPathForName("CBodyComponent.m_cellX");
        y = e.getDtClass().getFieldPathForName("CBodyComponent.m_cellY");
        z = e.getDtClass().getFieldPathForName("CBodyComponent.m_cellZ");
        health = e.getDtClass().getFieldPathForName("m_iHealth");
        playerID = e.getDtClass().getFieldPathForName("m_iPlayerID");
        level = e.getDtClass().getFieldPathForName("m_iCurrentLevel");
        mana = e.getDtClass().getFieldPathForName("m_flMana");
        strength = e.getDtClass().getFieldPathForName("m_flStrength");
        intellect = e.getDtClass().getFieldPathForName("m_flIntellect");
        maxHealth = e.getDtClass().getFieldPathForName("m_iMaxHealth");
        manaRegen = e.getDtClass().getFieldPathForName("m_flManaRegen");
        healthRegen = e.getDtClass().getFieldPathForName("m_flHealthRegen");
        movementSpeed = e.getDtClass().getFieldPathForName("m_iMoveSpeed");
        damageMin = e.getDtClass().getFieldPathForName("m_iDamageMin");
        damageMax = e.getDtClass().getFieldPathForName("m_iDamageMax");
        items = new FieldPath[itemCount];
        for (int i = 0; i < 10; i++)
        {
            items[i] = e.getDtClass().getFieldPathForName("m_hItems.000" + i);
        }
        for (int i = 10; i < itemCount; i++)
        {
            items[i] = e.getDtClass().getFieldPathForName("m_hItems.00" + i);
        }
    }
    
    public boolean isPosition(FieldPath path)
    {
        return (path.equals(x) || path.equals(y) || path.equals(z));
    }
    
    public boolean isHealth(FieldPath path)
    {
        return path.equals(health);
    }
    
    public boolean isLevel(FieldPath path)
    {
        return path.equals(level);
    }

    public boolean isMana(FieldPath path)
    {
        return path.equals(mana);
    }
    
    public boolean isStrength(FieldPath path)
    {
        return path.equals(strength);
    }
    
    public boolean isIntellect(FieldPath path)
    {
        return path.equals(intellect);
    }
    
    public boolean isMaxHealth(FieldPath path)
    {
        return path.equals(maxHealth);
    }
    
    public boolean isManaRegen(FieldPath path)
    {
        return path.equals(manaRegen);
    }
    
    public boolean isHealthRegen(FieldPath path)
    {
        return path.equals(healthRegen);
    }
    
    public boolean isMovementSpeed(FieldPath path)
    {
        return path.equals(movementSpeed);
    }
    
    public boolean isDamageMin(FieldPath path)
    {
        return path.equals(damageMin);
    }
    
    public boolean isDamageMax(FieldPath path)
    {
        return path.equals(damageMax);
    }
    
    public boolean isItems(FieldPath path)
    {
        for (int i = 0; i < itemCount; i++)
        {
            if (path.equals(items[i]))
            {
                return true;
            }
        }
        return false;
    }
}
