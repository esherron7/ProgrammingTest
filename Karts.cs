namespace MarioKart{
    public class Karts
    {
        // Private fields
        private int kartID;
        private string kartName;
        private string size; 
        private bool availability;
        private static int count;

        // Default constructor
        public Karts()
        {
            
            
        }

        // Parameterized constructor
        public Karts(int kartID, string kartName, string size, bool availability)
        {
            this.kartID = kartID;
            this.kartName = kartName;
            this.size = size;
            this.availability = availability;
        }

        // Getter and Setter for kartID
        public int GetID()
        {
            return kartID;
        }
        public void SetID(int kartID)
        {
            this.kartID = kartID;
        }

        // Getter and Setter for kartName
        public string GetName()
        {
            return kartName;
        }
        public void SetName(string kartName)
        {
            this.kartName = kartName;
        }

        // Getter and Setter for size
        public string GetSize()
        {
            return size;
        }
        public void SetSize(string size)
    {
        if (IsValidSize(size))
        {
            this.size = size;
        }
        else
        {
            throw new ArgumentException("Invalid size. Size must be 'small', 'medium', or 'large'.");
        }
    }

        // Getter and Setter for availability
        public bool GetAvailability()
        {
            return availability;
        }
        public void SetAvailability(bool availability)
        {
            this.availability = availability;
        }

        // Static method to set count
        public static void SetCount(int count)
        {
            Karts.count = count;
        }

        // Static method to get count
        public static int GetCount()
        {
            return count;
        }

        // Static method to increment count
        public static void IncCount()
        {
            Karts.count++;
        }

        // Static method to decrement count
        public static void DecCount()
        {
            Karts.count--;
        }

        // Override ToString() to return a formatted string representation of a kart
        public override string ToString()
        {
            return $"Kart ID: {kartID}, Name: {kartName}, Size: {size}, Available: {availability}";
        }
        public static bool IsValidSize(string size)
    {
        return size == "small" || size == "medium" || size == "large";
    }

    }
}