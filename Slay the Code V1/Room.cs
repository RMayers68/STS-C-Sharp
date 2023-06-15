using static Global.Functions;
namespace STV
{
    public class Room : IEquatable<Room>, IComparable<Room>
    {
        public string Location { get; set; }
        public char ShortHand { get; set; }
        public int RoomNumber { get; set; }
        public int Floor { get; set; }
        public List<Room> Connections { get; set; }
        private static readonly Random mapRNG = new();


        //constructor
        public Room(int RoomNumber, int Floor, string Location = "Undecided")
        {
            this.Location = Location;
            this.RoomNumber = RoomNumber;
            this.Floor = Floor;
            Connections = new List<Room>();
        }

        public Room(Room room)
        {
            this.Location = room.Location;
            this.Floor = room.Floor;
            this.RoomNumber = room.RoomNumber;
            Connections = room.Connections;
        }

        // location modifiers
        public void ChangeName(string name)
        {
            switch(name)
            {
                default: break;
                case "Monster":
                    Location = name;
                    ShortHand = 'M';
                    break;
                case "Event":
                    Location = name;
                    ShortHand = '?';
                    break;
                case "Elite":
                    Location = name;
                    ShortHand = 'E';
                    break;
                case "Rest Site":
                    Location = name;
                    ShortHand = 'R';
                    break;
                case "Merchant":
                    Location = name;
                    ShortHand = '$';
                    break;
                case "Treasure":
                    Location = name;
                    ShortHand = 'T';
                    break;

            }
        }

        //string method
        public override string ToString()
        {
            return $"({RoomNumber})";
        }
        // return distinct list of connecting rooms
        public void DistinctRooms()
        {
            Connections = Connections.Distinct(new RoomComparer()).ToList();
        }


        // equality
        public override bool Equals(object obj) => this.Equals(obj as Room);


        public bool Equals(Room r)
        {
            if (r is null)
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, r))
            {
                return true;
            }
            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            return (RoomNumber == r.RoomNumber) && (Floor == r.Floor);
        }

        public static int SortByFloorAscending(int floor1, int floor2)
        {

            return floor1.CompareTo(floor2);
        }


        // Default comparer for Part type.
        public int CompareTo(Room r)
        {
            // A null value means that this object is greater.
            if (r == null)
                return 1;

            else
                return this.Floor.CompareTo(r.Floor);
        }

        public override int GetHashCode()
        {
            return Floor + RoomNumber + ShortHand;
        }

        public static List<Room> MapGeneration()
        {
            // Variable Init
            Dictionary<int, List<Room>> MapTemplate = new();
            List<Room> paths = new();

            // Fills Map Template with 7*15 grid of Rooms
            for (int i = 1; i < 16; i++)
                MapTemplate[i] = new List<Room> { new(0, i), new(1, i), new(2, i), new(3, i), new(4, i), new(5, i), new(6, i) };

            // Runs 6 attempts at creating valid paths through Map       
            for (int i = 0; i < 6; i++)
            {

                // Grab starting node at random and move floor by floor to a valid room
                paths.Add(MapTemplate[1][mapRNG.Next(0, 7)]);
                for (int j = 1; j < 15; j++)
                {
                    while (true)
                    {
                        try     // Prevents -1 or 7 index which would be out of range
                        {
                            Room tmp = new(MapTemplate[j + 1][mapRNG.Next(paths.Last().RoomNumber - 1, paths.Last().RoomNumber + 2)]);
                            tmp.Connections.Add(paths.Last());
                            paths.Last().Connections.Add(tmp);
                            paths.Add(tmp);
                            break;
                        }
                        catch (ArgumentOutOfRangeException) { continue; }  // This runs loop until valid room is found
                    }
                }
            }

            // Remove duplicate Rooms selected during the process and merge connections so no path is lost.
            List<Room> distinct = new();
            foreach (Room room in paths)
            {
                if (!distinct.Contains(room))
                    distinct.Add(room);
                else
                {
                    distinct.Find(x => x.Equals(room)).Connections.AddRange(room.Connections);
                }
            }

            // Sort List by floors for assigning locations and visualization
            distinct = distinct.OrderBy(x => x.Floor).ThenBy(x => x.RoomNumber).ToList();
            // Assign locations to each room, starting on bottom floor
            for (int i = 1; i < 16; i++)
            {

                //Initial RNG chances
                int monster = 40, eVent = 65, elite = 83, restSite = 95, merchant = 101, choice = 0;

                // Next 3 conditionals are to set 1 floor of rooms to constant locations
                if (i == 1)
                {
                    foreach (Room r in distinct.Where(x => x.Floor == i))
                        r.ChangeName("Monster");
                    continue;
                }
                else if (i == 9)
                {
                    foreach (Room r in distinct.Where(x => x.Floor == i))
                        r.ChangeName("Treasure");
                    continue;
                }
                else if (i == 15)
                {
                    foreach (Room r in distinct.Where(x => x.Floor == i))
                    {
                        r.ChangeName("Rest Site");
                    }
                    continue;
                }

                // Next 2 are to change RNG chances for certain floors that can not have some events (-1 odds set on those rooms to prevent them being selected)
                else if (i > 1 && i < 6)
                {
                    monster = 63;
                    eVent = 93;
                    elite = -1;
                    restSite = -1;
                }
                else if (i == 14)
                {
                    monster = 50;
                    eVent = 70;
                    elite = 94;
                    restSite = -1;
                }

                // For Floors not 1,9,15 : Roll random number until a valid location is selected
                foreach (Room r in distinct.Where(x => x.Floor == i))
                {
                    while (true)
                    {
                        choice = mapRNG.Next(merchant);
                        if (choice < monster)
                            r.ChangeName("Monster");
                        else if (choice < eVent)
                            r.ChangeName("Event");
                        else if (choice < elite)
                        {
                            if (r.Connections.Find(x => x.Location == "Elite") == null)
                                r.ChangeName("Elite");
                        }
                        else if (choice < restSite)
                        {
                            if (r.Connections.Find(x => x.Location == "Rest Site") == null)
                                r.ChangeName("Rest Site");
                        }
                        else
                        {
                            if (r.Connections.Find(x => x.Location == "Merchant") == null)
                                r.ChangeName("Merchant");
                        }
                        if (r.Location != "Undecided")
                            break;
                    }
                }
            }
            return distinct;
        }

        public static void DrawMap(List<Room> map, string bossEncounter, Room activeRoom)
        {
            ScreenWipe();
            // Drawing the Map   
            Console.WriteLine($"\t{bossEncounter}\t\n");
            for (int floor = 15; floor >= 1; floor--)
            {
                // Rules for drawing lines with rooms
                for (int roomNumber = 0; roomNumber < 7; roomNumber++)
                {
                    if (FindRoom(floor, roomNumber, map) == null)
                        Console.Write("    ");
                    else
                    {
                        if (activeRoom != null && FindRoom(floor, roomNumber, map) is Room drawRoom && drawRoom.Floor == activeRoom.Floor && drawRoom.RoomNumber == activeRoom.RoomNumber)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(drawRoom.ShortHand + "   ");
                            Console.ForegroundColor = ConsoleColor.White;
                        }                         
                        else Console.Write(FindRoom(floor, roomNumber, map).ShortHand + "   ");
                    }
                }
                Console.WriteLine();
                // Rules for drawing paths inbetween Rooms
                for (int roomNumber = 0; roomNumber < 7; roomNumber++)
                {
                    Room currentRoom = FindRoom(floor, roomNumber, map), nextRoom = FindRoom(floor, roomNumber + 1, map);
                    bool currentExists = currentRoom != null, nextExists = nextRoom != null;

                    //Null move position to next to check
                    if (!currentExists)
                    {
                        Console.Write("  ");
                    }
                    else
                    {
                        // Middle Check
                        if (FindRoom(floor - 1, roomNumber, currentRoom.Connections) == null)
                            Console.Write("  ");
                        else
                            Console.Write("| ");
                    }

                    // Diagonal Checks
                    if (roomNumber != 6)
                    {
                        bool leftPathExists = false, rightPathExists = false;
                        if (nextExists)
                            leftPathExists = FindRoom(floor - 1, roomNumber, nextRoom.Connections) != null;
                        if (currentExists)
                            rightPathExists = FindRoom(floor - 1, roomNumber + 1, currentRoom.Connections) != null;
                        switch (rightPathExists, leftPathExists)
                        {
                            default:
                                Console.Write("  ");
                                break;
                            case (true, true):
                                Console.Write("X ");
                                break;
                            case (true, false):
                                Console.Write("\\ ");
                                break;
                            case (false, true):
                                Console.Write("/ ");
                                break;
                        }

                    }
                    else break; //Right check on last room is impossible path, skip
                }
                Console.WriteLine();
            }
        }
    }

    // Custom comparer for the Room class
    public class RoomComparer : IEqualityComparer<Room>
    {
        // Products are equal if their names and product numbers are equal.
        public bool Equals(Room x, Room y)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (x is null || y is null)
                return false;

            //Check whether the products' properties are equal.
            return x.Floor == y.Floor && x.RoomNumber == y.RoomNumber;
        }

        // If Equals() returns true for a pair of objects
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(Room room)
        {
            //Check whether the object is null
            if (room is null) return 0;

            //Get hash code for the FFloor field if it is not null.
            int hashFloor = room.Floor == 0 ? 0 : room.Floor.GetHashCode();

            //Get hash code for the Code field.
            int hashRoomNumber = room.RoomNumber.GetHashCode();

            //Calculate the hash code for the product.
            return hashFloor ^ hashRoomNumber;
        }
    }
}