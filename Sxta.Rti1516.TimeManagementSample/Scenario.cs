using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Sxta.Rti1516.TimeManagementSample
{
 
	/// <summary>
	/// Level class. Keeps the level information and draws a level on screen.
	/// An 'item' in a level can be a wall, a floor, a box or sokoban. The
	/// width and height of a level are measured in items, not in pixels. The
	/// width and height of a level when drawed on the screen can be calculated
	/// by multiplying the width and height by the size of the item.
	/// </summary>
	public class Scenario
	{
	    private string name = string.Empty; // Name of the level

	    private ItemType[,] levelMap;       // Level layout of items
	    private int width = 0;              // Level width in items
	    private int height = 0;             // Level height in items

        private Actor actor;
        private Home target;
        private Home home;
	    
        // ITEM_SIZE is the size of an item in the level
        // TO DO: Let user change it, and save the size in the savegame.xml ???
	    public const int ITEM_SIZE = 30;
	    
	    // changedItems is updated every time Sokoban moves or pushes a box.
	    // Max. 3 items can be changed each push, 2 for each move. We keep
	    // track of these change so we don't have to redraw the whole level
	    // after each move/push.
	    private Item item1, item2, item3;
	    
	    // For drawing the level on screen
        private Bitmap img;
        private Graphics g;
		
		#region Properties

        public string Name
        {
            get { return name; }
        }
        
        public int Width
        {
            get { return width; }
        }
        
        public int Height
        {
            get { return height; }
        }
        
        #endregion
        
	    /// <summary>
	    /// Constructor.
	    /// </summary>
	    /// <param name="aName">Level name</param>
	    /// <param name="aLevelMap">Level map</param>
	    /// <param name="aWidth">Level width</param>
	    /// <param name="aHeight">Level height</param>
	    /// <param name="aNrOfGoals">Number of goals</param>
	    /// <param name="aLevelNr">Level number</param>
	    /// <param name="aLevelSetName">Set name that level belongs to</param>
		public Scenario(string aName, ItemType[,] aLevelMap, int aWidth, int aHeight)
		{
		    name = aName;
			width = aWidth;
			height = aHeight;
			levelMap = aLevelMap;
		}

        public void SetActor(Actor actor)
        {
            lock (this)
            {
                this.actor = actor;

                item1 = new Item(ItemType.SokobanOnGoal, actor.PosX, actor.PosY);
                DrawChanges(actor.Direction);
            }
        }

        public void SetTarget(Home target)
        {
            lock (this)
            {
                this.target = target;

                item1 = new Item(ItemType.Goal, target.PosX, target.PosY);
                DrawChanges(Actor.MoveDirection.Up); // TODO: chapuza
            }
        }

        protected bool IsThereTarget(int x, int y)
        {
            lock (this)
            {
                if (target != null)
                {
                    return (target.PosX == x && target.PosY == y);
                }
                else
                {
                    return false;
                }
            }
        }

        protected bool IsThereHome(int x, int y)
        {
            lock (this)
            {
                if (actor.Home != null)
                {
                    return (actor.Home.PosX == x && actor.Home.PosY == y);
                }
                else
                {
                    return false;
                }
            }
        }

        protected bool isThereActor(int x, int y)
        {
            lock (this)
            {
                if (actor != null)
                {
                    return (actor.PosX == x && actor.PosY == y);
                }
                else
                {
                    return false;
                }
            }
        }

        public void MoveActorToHome(int x, int y)
        {
            int sokoPosX = actor.PosX;
            int sokoPosY = actor.PosY;

            if (IsThereHome(x, y))
            {
                item1 = new Item(ItemType.SokobanOnGoal, x, y);
            }
            else if (levelMap[x, y] == ItemType.Floor)
            {
                item1 = new Item(ItemType.Sokoban, x, y);
            }

            if (IsThereTarget(sokoPosX, sokoPosY))
            {
                item2 = new Item(ItemType.Goal, sokoPosX, sokoPosY);
            }
            else if (levelMap[sokoPosX, sokoPosY] == ItemType.Floor)
            {
                item2 = new Item(ItemType.Floor, sokoPosX, sokoPosY);
            }

            item3 = new Item(ItemType.Goal, target.PosX, target.PosY);

            actor.PosX = x;
            actor.PosY = y;
        }

        /*
        public void ResetActorPosition(String name, int x, int y)
        {
            if (actors.ContainsKey(name))
            {               
                Actor actor = actors[name];

                int sokoPosX = actor.PosX;
                int sokoPosY = actor.PosY;

                if (IsThereTarget(x, y))
                {
                    item1 = new Item(ItemType.SokobanOnGoal, x, y);
                }
                else if (levelMap[x, y] == ItemType.Floor)
                {
                    item1 = new Item(ItemType.Sokoban, x, y);
                }

                if (IsThereTarget(sokoPosX, sokoPosY))
                {
                    //levelMap[sokoPosX, sokoPosY] = ItemType.Goal;
                    item2 = new Item(ItemType.Goal, sokoPosX, sokoPosY);
                }
                else if (levelMap[sokoPosX, sokoPosY] == ItemType.Floor)
                {
                    item2 = new Item(ItemType.Floor, sokoPosX, sokoPosY);
                }

                actor.PosX = x;
                actor.PosY = y;

                //DrawChanges(actor.Direction);
            }
        }
		*/

        /// <summary>
        /// This method draws the level on screen. Around the level there are
        /// extra rows and columns to make it look better. The first 3 for-
        /// statements draw this border. Then we load the level map and step
        /// through it line by line, and character by character. Depending on
        /// the ItemType in the level map, we draw the corresponding image.
        /// </summary>
        /// <returns>The 'level' image that will be drawn to screen</returns>
		public Image CreateScenario()
		{
            int levelWidth = (width + 2) * Scenario.ITEM_SIZE;
            int levelHeight = (height + 2) * Scenario.ITEM_SIZE;
            
            img = new Bitmap(levelWidth, levelHeight);
            g = Graphics.FromImage(img);
		    
            g.Clear(Color.FromArgb(27, 33, 61));
		 
            // Draw the border around the level
            for (int i = 0; i < width + 1; i++)
            {
                g.DrawImage(ImgSpace, ITEM_SIZE * i, 0,
                    ITEM_SIZE, ITEM_SIZE);
                g.DrawImage(ImgSpace, ITEM_SIZE * i,
                    (height - 1) * ITEM_SIZE, ITEM_SIZE, ITEM_SIZE);
            }
            for (int i = 0; i < height + 1; i++)
            {
                g.DrawImage(ImgSpace, 0, ITEM_SIZE * i,
                    ITEM_SIZE, ITEM_SIZE);
                g.DrawImage(ImgSpace, (width - 1) * ITEM_SIZE,
                    ITEM_SIZE * i, ITEM_SIZE, ITEM_SIZE);
            }

            // Draw the level
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Image image = GetLevelImage(levelMap[i, j], Actor.MoveDirection.Up); //esto esta mal , sokoDirection);

                    g.DrawImage(image, i * ITEM_SIZE,
                         j * ITEM_SIZE, ITEM_SIZE, ITEM_SIZE);
                    
                    /*
                    // Set Sokoban's position
                    if (levelMap[i, j] == ItemType.Sokoban ||
                        levelMap[i, j] == ItemType.SokobanOnGoal)
                    {
                        sokoPosX = i;
                        sokoPosY = j;
                    }
                    */
                }
            }

            return img;
		}

		/// <summary>
		/// When Sokoban moves or pushes we only draws these changes instead of
		/// redrawing the whole level again. Great performance improvement.
		/// </summary>
		/// <returns>The 'level' image that will be drawn to screen</returns>
		public void DrawChanges(Actor.MoveDirection direction)
		{
            if (item1 != null)
            {
                Image image1 = GetLevelImage(item1.ItemType, direction);
                g.DrawImage(image1, item1.XPos * ITEM_SIZE,
                    item1.YPos * ITEM_SIZE, ITEM_SIZE, ITEM_SIZE);
            }

            if (item2 != null)
            {
                Image image2 = GetLevelImage(item2.ItemType, direction);
                g.DrawImage(image2, item2.XPos * ITEM_SIZE,
                    item2.YPos * ITEM_SIZE, ITEM_SIZE, ITEM_SIZE);
            }

            if (item3 != null)
            {
                Image image3 = GetLevelImage(item3.ItemType, direction);
                g.DrawImage(image3, item3.XPos * ITEM_SIZE,
                    item3.YPos * ITEM_SIZE, ITEM_SIZE, ITEM_SIZE);
            }
		}

        public Image GetScenario()
        {
            return img;
        }
		
		#region Moving Actor

        /// <summary>
        /// Check in what direction we want to move and call the corresponding
        /// method.
        /// </summary>
        /// <param name="direction">Direction to move in</param>
        public void MoveActor(Actor.MoveDirection direction)
        {
            switch (direction)
            {
                case Actor.MoveDirection.Up:
                    MoveUp(actor);
                    break;
                case Actor.MoveDirection.Down:
                    MoveDown(actor);
                    break;
                case Actor.MoveDirection.Right:
                    MoveRight(actor);
                    break;
                case Actor.MoveDirection.Left:
                    MoveLeft(actor);
                    break;
            }
        }
        
        /// <summary>
        /// Move up
        /// </summary>
		private void MoveUp(Actor actor)
		{
            int sokoPosX = actor.PosX;
            int sokoPosY = actor.PosY;

            // If soko can move to the next position
		    if (levelMap[sokoPosX, sokoPosY - 1] == ItemType.Floor ||
		        IsThereTarget(sokoPosX, sokoPosY - 1))
		    {
                if (IsThereTarget(sokoPosX, sokoPosY - 1))
                {
                    item2 = new Item(ItemType.SokobanOnGoal, sokoPosX, sokoPosY - 1);
                }
                else if (levelMap[sokoPosX, sokoPosY - 1] == ItemType.Floor)
                {
                    item2 = new Item(ItemType.Sokoban, sokoPosX, sokoPosY - 1);
                }

                UpdateCurrentSokobanPosition(sokoPosX, sokoPosY);
                UpdateCurrentPackagePosition(sokoPosX, sokoPosY - 2, Actor.MoveDirection.Up);
                actor.Move(Actor.MoveDirection.Up);
		    }
		}
		
		
		/// <summary>
		/// Move down
		/// </summary>
		private void MoveDown(Actor actor)
		{
            int sokoPosX = actor.PosX;
            int sokoPosY = actor.PosY;

            if (levelMap[sokoPosX, sokoPosY + 1] == ItemType.Floor ||
                IsThereTarget(sokoPosX, sokoPosY + 1))
            {
                if (IsThereTarget(sokoPosX, sokoPosY + 1))
                {
                    item2 = new Item(ItemType.SokobanOnGoal, sokoPosX, sokoPosY + 1);
                }
                else if (levelMap[sokoPosX, sokoPosY + 1] == ItemType.Floor)
                {
                    item2 = new Item(ItemType.Sokoban, sokoPosX, sokoPosY + 1);
                }

                UpdateCurrentSokobanPosition(sokoPosX, sokoPosY);
                UpdateCurrentPackagePosition(sokoPosX, sokoPosY + 2, Actor.MoveDirection.Down);
                actor.Move(Actor.MoveDirection.Down);
            }
        }
        
     
        /// <summary>
        /// Move right
        /// </summary>
        private void MoveRight(Actor actor)
        {
            int sokoPosX = actor.PosX;
            int sokoPosY = actor.PosY;

            if (levelMap[sokoPosX + 1, sokoPosY] == ItemType.Floor ||
                IsThereTarget(sokoPosX + 1, sokoPosY))
            {
                if (IsThereTarget(sokoPosX + 1, sokoPosY))
                {
                    item2 = new Item(ItemType.SokobanOnGoal, sokoPosX + 1, sokoPosY);
                }
                else if (levelMap[sokoPosX + 1, sokoPosY] == ItemType.Floor)
                {
                    item2 = new Item(ItemType.Sokoban, sokoPosX + 1, sokoPosY);
                }

                UpdateCurrentSokobanPosition(sokoPosX, sokoPosY);
                UpdateCurrentPackagePosition(sokoPosX + 2, sokoPosY, Actor.MoveDirection.Right);
                actor.Move(Actor.MoveDirection.Right);
            }
        }
        
        /// <summary>
        /// Move left
        /// </summary>
        private void MoveLeft(Actor actor)
        {
            int sokoPosX = actor.PosX;
            int sokoPosY = actor.PosY;
            
            if (levelMap[sokoPosX - 1, sokoPosY] == ItemType.Floor ||
                IsThereTarget(sokoPosX - 1, sokoPosY))
            {
                if (IsThereTarget(sokoPosX - 1, sokoPosY))
                {
                    item2 = new Item(ItemType.SokobanOnGoal, sokoPosX - 1, sokoPosY);
                }
                else if (levelMap[sokoPosX - 1, sokoPosY] == ItemType.Floor)
                {
                    item2 = new Item(ItemType.Sokoban, sokoPosX - 1, sokoPosY);
                }

                UpdateCurrentSokobanPosition(sokoPosX, sokoPosY);
                UpdateCurrentPackagePosition(sokoPosX - 2, sokoPosY, Actor.MoveDirection.Left);
                actor.Move(Actor.MoveDirection.Left);
            }
        }

        #endregion

        /// <summary>
        /// Updates Sokoban's position. This code is used in all the MoveXX
        /// methods, so I put it in a separate method.
        /// </summary>
        private void UpdateCurrentSokobanPosition(int sokoPosX, int sokoPosY)
        {
            if (isThereActor(sokoPosX, sokoPosY))
            {
                if (IsThereTarget(sokoPosX, sokoPosY))
                {
                    item1 = new Item(ItemType.Goal, sokoPosX, sokoPosY);
                }
                else if (IsThereHome(sokoPosX, sokoPosY))
                {
                    item1 = new Item(ItemType.Goal, sokoPosX, sokoPosY);
                }
                else
                {
                    item1 = new Item(ItemType.Floor, sokoPosX, sokoPosY);
                }
            }
        }

        private void UpdateCurrentPackagePosition(int packagePosX, int packagePosY, Actor.MoveDirection direction)
        {
            if (IsThereTarget(packagePosX, packagePosY))
            {
                item3 = new Item(ItemType.PackageOnGoal, packagePosX, packagePosY);
            }
            else
            {
                switch (direction)
                {
                    case Actor.MoveDirection.Right:
                        if (packagePosX > target.PosX)
                        {
                            if (packagePosY < target.PosY)
                            {
                                item3 = new Item(ItemType.Package, packagePosX - 1, packagePosY + 1);
                            }
                            else
                            {
                                item3 = new Item(ItemType.Package, packagePosX - 1, packagePosY - 1);
                            }
                        }
                        else
                        {
                            item3 = new Item(ItemType.Package, packagePosX, packagePosY);
                        }
                        break;

                    case Actor.MoveDirection.Left:
                        if (packagePosX < target.PosX)
                        {
                            if (packagePosY < target.PosY)
                            {
                                item3 = new Item(ItemType.Package, packagePosX + 1, packagePosY + 1);
                            }
                            else
                            {
                                item3 = new Item(ItemType.Package, packagePosX + 1, packagePosY - 1);
                            }
                        }
                        else
                        {
                            item3 = new Item(ItemType.Package, packagePosX, packagePosY);
                        }
                        break;

                    case Actor.MoveDirection.Up:
                    case Actor.MoveDirection.Down:
                        item3 = new Item(ItemType.Package, packagePosX, packagePosY);
                        break;
                }

            }
        }
		
		#region GetLevelImage
		
		/// <summary>
		/// Depending on the 'item character' in the XML for the level set we
		/// need to display an image on the screen. This is what happens here.
		/// We also take into account the direction Sokoban is moving in,
		/// because we want him to face to the left when he is moving left.
		/// </summary>
		/// <param name="itemType">Level item</param>
		/// <param name="direction">Sokoban direction</param>
		/// <returns>The image to be displayed on screen</returns>
		public Image GetLevelImage(ItemType itemType, Actor.MoveDirection direction)
		{
		    Image image;

            if (itemType == ItemType.Wall)
                image = ImgWall;
            else if (itemType == ItemType.Floor)
                image = ImgFloor;
            else if (itemType == ItemType.Package)
                image = ImgPackage;
            else if (itemType == ItemType.Goal)
                image = ImgGoal;
            else if (itemType == ItemType.Sokoban)
            {
                if (direction == Actor.MoveDirection.Up)
                    image = ImgSokoUp;
                else if (direction == Actor.MoveDirection.Down)
                    image = ImgSokoDown;
                else if (direction == Actor.MoveDirection.Right)
                    image = ImgSokoRight;
                else
                    image = ImgSokoLeft;
            }
            else if (itemType == ItemType.PackageOnGoal)
                image = ImgPackageGoal;
            else if (itemType == ItemType.SokobanOnGoal)
            {
                if (direction == Actor.MoveDirection.Up)
                    image = ImgSokoUpGoal;
                else if (direction == Actor.MoveDirection.Down)
                    image = ImgSokoDownGoal;
                else if (direction == Actor.MoveDirection.Right)
                    image = ImgSokoRightGoal;
                else
                    image = ImgSokoLeftGoal;
            }
            else
                image = ImgSpace;
            
            return image;
		}
		
        
        // These are the proprties for the images of all possible items within
        // the game. These are hard coded. If we want to ad support for skins,
        // than we should put these values inside a skin XML file.
        
        public Image ImgWall
        {
            get { return
                Image.FromFile("graphics/wall.bmp"); }
        }
		
        public Image ImgFloor
        {
            get { return
                Image.FromFile("graphics/floor.bmp"); }
        }
        
        public Image ImgPackage
        {
            get { return
                Image.FromFile("graphics/package.bmp"); }
        }
        
        public Image ImgPackageGoal
        {
            get { return
                Image.FromFile("graphics/package_goal.bmp"); }
        }
        
        public Image ImgGoal
        {
            get { return
                Image.FromFile("graphics/goal.bmp"); }
        }
        
        public Image ImgSokoUp
        {
            get { return
                Image.FromFile("graphics/soko_up.bmp"); }
        }
        
        public Image ImgSokoDown
        {
            get { return
                Image.FromFile("graphics/soko_down.bmp"); }
        }
        
        public Image ImgSokoRight
        {
            get { return
                Image.FromFile("graphics/soko_right.bmp"); }
        }
        
        public Image ImgSokoLeft
        {
            get { return
                Image.FromFile("graphics/soko_left.bmp"); }
        }
        
        public Image ImgSokoUpGoal
        {
            get { return
                Image.FromFile("graphics/soko_goal_up.bmp"); }
        }
        
        public Image ImgSokoDownGoal
        {
            get { return
                Image.FromFile("graphics/soko_goal_down.bmp"); }
        }
        
        public Image ImgSokoRightGoal
        {
            get { return
                Image.FromFile("graphics/soko_goal_right.bmp"); }
        }
        
        public Image ImgSokoLeftGoal
        {
            get { return
                Image.FromFile("graphics/soko_goal_left.bmp"); }
        }
        
        public Image ImgSpace
        {
            get { return
                Image.FromFile("graphics/space.bmp"); }
        }
        
        #endregion
	}
}
