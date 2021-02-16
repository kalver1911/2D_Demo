
public class ItemProperty
{
    public string itemName;
    public int itemNumber;
    public string itemType;

    public ItemProperty()
    {
        itemName = "null";
        itemNumber = 0;
        itemType = "null";
    }
}

public class PropProperty : ItemProperty
{
    
}

public class WeaponProperty : ItemProperty
{
    public WeaponProperties weaponProperties= new WeaponProperties();
}

public class SlotProperty : ItemProperty
{
    
}