using System.Collections.Generic;
using System.Linq;

namespace InventorySystem.Services
{
    public static class NextIDService
    {
        // Simple helper function to generate next ID number
        public static int GetNextId(this List<int> ids)
        {
            if (ids.Count == 0)
            {
                return 1;
            }
            return ids.Max() + 1;
        }
    }
}
