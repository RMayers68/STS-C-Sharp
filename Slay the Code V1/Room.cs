namespace STV
{
    public class Room : IEquatable<Room>, IComparable<Room>
    {
        public string Location { get; set; }
        public char ShortHand { get; set; }
        public int RoomNumber { get; set; }
        public int Floor { get; set; }
        public List<Room> Connections { get; set; }


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
