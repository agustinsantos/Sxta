using System;

namespace Sxta.Rti1516.TimeManagementSample
{
	/// <summary>
    /// Enum every possible item in a level
    /// </summary>
    public enum ItemType
    {
        Wall,
        Floor,
        Package,
        Goal,
        Sokoban,
        PackageOnGoal,
        SokobanOnGoal,
        Space
    }
    
	/// <summary>
	/// Item represents an item in a level, like wall, floor, etc..
	/// </summary>
	public class Item
	{
	    private ItemType itemType;  // Type: wall, floor, etc..
	    private int xPos;           // X position in the level
	    private int yPos;           // Y position in the level
	
	
	    /// <summary>
	    /// Constructor
	    /// </summary>
	    /// <param name="aItemType">Item type</param>
	    /// <param name="aXPos">X position</param>
	    /// <param name="aYPos">Y position</param>
		public Item(ItemType aItemType, int aXPos, int aYPos)
		{
			itemType = aItemType;
			xPos = aXPos;
			yPos = aYPos;
		}
		
		
		// Properties
		
		public ItemType ItemType
		{
		    get { return itemType; }
		}
		
        public int XPos
        {
            get { return xPos; }
        }
        
        public int YPos
        {
            get { return yPos; }
        }
	}
}
