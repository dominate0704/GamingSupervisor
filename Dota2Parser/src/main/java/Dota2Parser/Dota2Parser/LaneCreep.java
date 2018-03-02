package Dota2Parser.Dota2Parser;

import skadistats.clarity.model.FieldPath;
import skadistats.clarity.model.Entity;

public class LaneCreep
{    
    public FieldPath x;
    public FieldPath y;
    public FieldPath z;
    public FieldPath health;
    public FieldPath teamNumber;
    
    public LaneCreep(Entity e)
    {
        x = e.getDtClass().getFieldPathForName("CBodyComponent.m_cellX");
        y = e.getDtClass().getFieldPathForName("CBodyComponent.m_cellY");
        z = e.getDtClass().getFieldPathForName("CBodyComponent.m_cellZ");
        health = e.getDtClass().getFieldPathForName("m_iHealth");
        teamNumber = e.getDtClass().getFieldPathForName("m_iTeamNum");
    }
    
    public boolean isPosition(FieldPath path)
    {
        return (path.equals(x) || path.equals(y) || path.equals(z));
    }
    
    public boolean isHealth(FieldPath path)
    {
        return path.equals(health);
    }
    
    public boolean isTeamNumber(FieldPath path)
    {
        return path.equals(teamNumber);
    }
}
