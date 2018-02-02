using System.Collections.Generic;
using System.Linq;

namespace Stepper.Highscores
{
    public class HighscoreManager
    {
        private static string playerPrefsName = "cuphead_highscores";

        /// <summary>
        /// Loads the highscore from the playerprefs, and returns the list with a maximum of limit.
        /// </summary>
        /// <param name="limit">The maximum number of high scores to populate the list with.
        /// write -1 to retrieve all.</param>
        /// <returns>A list populated with the high scores.</returns>
        public static List<Highscore> GetHighscores(int limit = 10)
        {
            var raw_json = UnityEngine.PlayerPrefs.GetString(playerPrefsName, "empty");

            if (raw_json.Equals("empty")) return null;
            
            var list = UnityEngine.JsonUtility.FromJson<List<Highscore>>(raw_json)
                .OrderByDescending(x=>x.Points).ToList();

            return limit == -1 ? list : list.Take(limit).ToList();
        }

        /// <summary>
        /// Retrieves the rank of the highscore supplied, and saves it if specified.
        /// </summary>
        /// <param name="highscore">The highscore to add.</param>
        /// <param name="save">Save the highscore?</param>
        /// <returns>the rank of the highscore.</returns>
        public static int GetHighscoreRank(Highscore highscore, bool save)
        {
            var raw_json = UnityEngine.PlayerPrefs.GetString(playerPrefsName, "empty");

            if (raw_json.Equals("empty"))
            {
                var highscores = new List<Highscore>();
                highscores.Add(highscore);
                SaveList(highscores);
                return 1;
            }

            var list = UnityEngine.JsonUtility.FromJson<List<Highscore>>(raw_json);
            list.Add(highscore);
            var ordered = list.OrderByDescending(x => x.Points).ToList();
            
            if(save) SaveList(ordered);
            return ordered.FindIndex(x=> x == highscore) + 1;
        }

        private static void SaveList(List<Highscore> list)
        {
            var json = UnityEngine.JsonUtility.ToJson(list);
            UnityEngine.PlayerPrefs.SetString(playerPrefsName, json);
        }
    }

    [System.Serializable]
    public class Highscore
    {
        public string Name { get; private set; }
        public int Points { get; private set; }

        public Highscore(string name, int points)
        {
            Name = name;
            this.Points = points;
        }
    }
}