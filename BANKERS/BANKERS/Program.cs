namespace BANKERS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] allocation = { 
                { 0, 1, 0 }, 
                { 2, 0, 0 }, 
                { 3, 0, 2 }, 
                { 2, 1, 1 }, 
                { 0, 0, 2 } 
            }; 
            
            int[,] maximum = { 
                { 7, 5, 3 }, 
                { 3, 2, 2 }, 
                { 9, 0, 2 }, 
                { 2, 2, 2 }, 
                { 4, 3, 3 } 
            }; 
            
            int[] available = { 3, 3, 2 }; 
            
            BankersAlgorithm ba = new BankersAlgorithm(allocation, maximum, available); 
            
            ba.ShowStepByStepMatrix();
            if (ba.IsSafe())
            {
                Console.WriteLine("The system is in a safe state."); ba.PrintSafeSequence();
            }
            else 
            { 
                Console.WriteLine("The system is in a deadlock state."); 
            }

        }
    }
}
