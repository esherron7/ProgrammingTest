namespace MarioKart{
    public class RaceResults
    {
        // Private fields
        private int interactionID;
        private string playerEmail;
        private int kartID;
        private string raceDate;
        private int raceTime;
        private int timeElapsed;
        private string track;
        private bool kartReturned;
        private static int count;

        // Default constructor
        public RaceResults()
        {
            
        }

        // Parameterized constructor
        public RaceResults(int interactionID, string playerEmail, int kartID, string raceDate, int raceTime, int timeElapsed, string track, bool kartReturned)
        {
            this.interactionID = interactionID;
            this.playerEmail = playerEmail;
            this.kartID = kartID;
            this.raceDate = raceDate;
            this.raceTime = raceTime;
            this.timeElapsed = timeElapsed;
            this.track = track;
            this.kartReturned = kartReturned;
        }

        // Getter and Setter for interactionID
        public int GetInteractionID()
        {
            return interactionID;
        }

        public void SetInteractionID(int interactionID)
        {
            this.interactionID = interactionID;
        }

        // Getter and Setter for playerEmail
        public string GetPlayerEmail()
        {
            return playerEmail;
        }

        public void SetPlayerEmail(string playerEmail)
        {
            this.playerEmail = playerEmail;
        }

        // Getter and Setter for kartID
        public int GetKartID()
        {
            return kartID;
        }

        public void SetKartID(int kartID)
        {
            this.kartID = kartID;
        }

        // Getter and Setter for raceDate
        public string GetRaceDate()
        {
            return raceDate;
        }

        public void SetRaceDate(string raceDate)
        {
            this.raceDate = raceDate;
        }

        // Getter and Setter for raceTime
        public int GetRaceTime()
        {
            return raceTime;
        }

        public void SetRaceTime(int raceTime)
        {
            this.raceTime = raceTime;
        }

        // Getter and Setter for timeElapsed
        public int GetTimeElapsed()
        {
            return timeElapsed;
        }

        public void SetTimeElapsed(int timeElapsed)
        {
            this.timeElapsed = timeElapsed;
        }

        // Getter and Setter for track
        public string GetTrack()
        {
            return track;
        }

        public void SetTrack(string track)
        {
            this.track = track;
        }

        // Getter and Setter for kartReturned
        public bool GetKartReturned()
        {
            return kartReturned;
        }

        public void SetKartReturned(bool kartReturned)
        {
            this.kartReturned = kartReturned;
        }
         public static void SetCount(int count)
        {
            RaceResults.count = count;
        }
        // Static method to get count
        public static int GetCount()
        {
            return count;
        }

        // Static method to increment count
        public static void IncCount()
        {
            count++;
        }

        // Static method to decrement count
        public static void DecCount()
        {
            count--;
        }

        // Override ToString() to return a formatted string representation of a race result
        public override string ToString()
        {
            return $"Interaction ID: {interactionID}, Player Email: {playerEmail}, Kart ID: {kartID}, Race Date: {raceDate}, Race Time: {raceTime}, Time Elapsed: {timeElapsed}, Track: {track}, Kart Returned: {kartReturned}";
        }
    }
}
