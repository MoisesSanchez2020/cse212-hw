using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

public static class SetsAndMapsTester {
    public static void Run() {
        // Problem 1: Find Pairs with Sets
        Console.WriteLine("\n=========== Finding Pairs TESTS ===========");
        DisplayPairs(new[] { "am", "at", "ma", "if", "fi" });
        Console.WriteLine("---------");
        DisplayPairs(new[] { "ab", "bc", "cd", "de", "ba" });
        Console.WriteLine("---------");
        DisplayPairs(new[] { "ab", "ba", "ac", "ad", "da", "ca" });
        Console.WriteLine("---------");
        DisplayPairs(new[] { "ab", "ac" }); // No pairs displayed
        Console.WriteLine("---------");
        DisplayPairs(new[] { "ab", "aa", "ba" });
        Console.WriteLine("---------");
        DisplayPairs(new[] { "23", "84", "49", "13", "32", "46", "91", "99", "94", "31", "57", "14" });

        // Problem 2: Degree Summary
        Console.WriteLine("\n=========== Census TESTS ===========");
        Console.WriteLine(string.Join(", ", SummarizeDegrees("census.txt")));

        // Problem 3: Anagrams
        Console.WriteLine("\n=========== Anagram TESTS ===========");
        Console.WriteLine(IsAnagram("CAT", "ACT")); // true
        Console.WriteLine(IsAnagram("DOG", "GOOD")); // false
        Console.WriteLine(IsAnagram("AABBCCDD", "ABCD")); // false
        Console.WriteLine(IsAnagram("ABCCD", "ABBCD")); // false
        Console.WriteLine(IsAnagram("BC", "AD")); // false
        Console.WriteLine(IsAnagram("Ab", "Ba")); // true
        Console.WriteLine(IsAnagram("A Decimal Point", "Im a Dot in Place")); // true
        Console.WriteLine(IsAnagram("tom marvolo riddle", "i am lord voldemort")); // true
        Console.WriteLine(IsAnagram("Eleven plus Two", "Twelve Plus One")); // true
        Console.WriteLine(IsAnagram("Eleven plus One", "Twelve Plus One")); // false

        // Problem 4: Maze
        Console.WriteLine("\n=========== Maze TESTS ===========");
        Dictionary<(int, int), bool[]> map = SetupMazeMap();
        var maze = new Maze(map);
        maze.ShowStatus(); // Should be at (1,1)
        // ...rest of your maze movement tests...
        maze.ShowStatus(); // Should be at the final position after all movements

        // Problem 5: Earthquake
        Console.WriteLine("\n=========== Earthquake TESTS ===========");
        EarthquakeDailySummary();
    }

    private static void DisplayPairs(string[] words) {
        HashSet<string> wordSet = new HashSet<string>(words);
        HashSet<string> displayed = new HashSet<string>();

        foreach (string word in words) {
            string reversed = new string(word.Reverse().ToArray());

            if (wordSet.Contains(reversed) && !displayed.Contains(word) && !word.Equals(reversed)) {
                Console.WriteLine($"{word} & {reversed}");
                displayed.Add(reversed);
            }
        }
    }

    private static Dictionary<string, int> SummarizeDegrees(string filename) {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename)) {
            var fields = line.Split(",");
            string degree = fields[3].Trim();

            if (degrees.ContainsKey(degree)) {
                degrees[degree]++;
            } else {
                degrees.Add(degree, 1);
            }
        }

        return degrees;
    }

    private static bool IsAnagram(string word1, string word2) {
        word1 = new string(word1.ToLower().Where(char.IsLetter).ToArray());
        word2 = new string(word2.ToLower().Where(char.IsLetter).ToArray());

        if (word1.Length != word2.Length) {
            return false;
        }

        var letterCounts = new Dictionary<char, int>();

        foreach (char c in word1) {
            if (letterCounts.ContainsKey(c)) {
                letterCounts[c]++;
            } else {
                letterCounts[c] = 1;
            }
        }

        foreach (char c in word2) {
            if (!letterCounts.ContainsKey(c) || --letterCounts[c] < 0) {
                return false;
            }
        }

        return letterCounts.Values.All(count => count == 0);
    }

    private static Dictionary<(int, int), bool[]> SetupMazeMap() {
        return new Dictionary<(int, int), bool[]> {
            // Populate this dictionary based on your maze structure
            // Example: { (1, 1), new[] { false, true, false, false } },
            // Add other cells as needed
        };
    }

    public class Maze {
        private Dictionary<(int, int), bool[]> map;
        private (int X, int Y) currentPosition;

        public Maze(Dictionary<(int, int), bool[]> mazeMap) {
            this.map = mazeMap;
            this.currentPosition = (1, 1); // Starting position
        }

        public void MoveLeft() {
            if (CanMove(currentPosition, 0)) {
                currentPosition = (currentPosition.X - 1, currentPosition.Y);
                ShowStatus();
            } else {
                Console.WriteLine("Error: Cannot move left");
            }
        }

        public void MoveRight() {
            if (CanMove(currentPosition, 1)) {
                currentPosition = (currentPosition.X + 1, currentPosition.Y);
                ShowStatus();
            } else {
                Console.WriteLine("Error: Cannot move right");
            }
        }

        public void MoveUp() {
            if (CanMove(currentPosition, 2)) {
                currentPosition = (currentPosition.X, currentPosition.Y - 1);
                ShowStatus();
            } else {
                Console.WriteLine("Error: Cannot move up");
            }
        }

        public void MoveDown() {
            if (CanMove(currentPosition, 3)) {
                currentPosition = (currentPosition.X, currentPosition.Y + 1);
                ShowStatus();
            } else {
                Console.WriteLine("Error: Cannot move down");
            }
        }

        private bool CanMove((int X, int Y) position, int direction) {
            if (!map.ContainsKey(position)) {
                return false;
            }
            return map[position][direction];
        }

        public void ShowStatus() {
            Console.WriteLine($"Current Position: ({currentPosition.X}, {currentPosition.Y})");
        }
    }

    private static void EarthquakeDailySummary() {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        var response = client.GetAsync(uri).Result;

        if (!response.IsSuccessStatusCode) {
            Console.WriteLine("Error fetching earthquake data.");
            return;
        }

        var json = response.Content.ReadAsStringAsync().Result;
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        try {
            var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);
            foreach (var feature in featureCollection.Features) {
                Console.WriteLine($"{feature.Properties.Place} - Mag {feature.Properties.Mag}");
            }
        } catch (Exception ex) {
            Console.WriteLine($"Error parsing earthquake data: {ex.Message}");
        }
    }

    public class FeatureCollection {
        public List<Feature> Features { get; set; }
    }

    public class Feature {
        public Properties Properties { get; set; }
    }

    public class Properties {
        public string Place { get; set; }
        public double Mag { get; set; }
    }
}
