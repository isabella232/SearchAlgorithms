using System;

namespace SearchAlgorithm.DistanceMeasures.StringSurveyors
{
    /// <summary>
    /// Calculate the Levenshtein distance between two strings. This implementation
    /// can be found http://www.dotnetperls.com/levenshtein
    ///
    ///  More information about the algorithm can be found 
    /// http://en.wikipedia.org/wiki/Levenshtein_distance
    /// </summary>
    public class LevenshteinStringSurveyor : Surveyor<string>
    {
        public override int CalculateDistance(string root, string leaf)
        {
            int n = root.Length;
            int m = leaf.Length;
            int[,] d = new int[n + 1,m + 1];

            // Step 1
            if (n == 0)
                return m;

            if (m == 0)
                return n;

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                // Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (leaf[j - 1] == root[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }

            // Step 7
            return d[n, m];
        }
    }
}