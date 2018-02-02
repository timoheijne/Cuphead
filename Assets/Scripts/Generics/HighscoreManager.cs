using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

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
            
            var list = UnityEngine.JsonUtility.FromJson<HighscoreList>(raw_json).highscores
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
                if(save) SaveList(highscores);
                return 1;
            }

            var list = UnityEngine.JsonUtility.FromJson<HighscoreList>(raw_json).highscores.ToList();
            list.Add(highscore);
            var ordered = list.OrderByDescending(x => x.Points).ToList();
            
            if(save) SaveList(ordered);
            return ordered.FindIndex(x=> x == highscore) + 1;
        }

        private static void SaveList(List<Highscore> list)
        {
            HighscoreList hl = new HighscoreList(list);
            var json = JsonUtility.ToJson(hl);
            PlayerPrefs.SetString(playerPrefsName, json);
            PlayerPrefs.Save();
        }
    }

    [Serializable]
    public class Highscore
    {
        public string Name;
        public int Points;

        public Highscore(string name, int points)
        {
            Name = name;
            this.Points = points;
        }
    }

    [Serializable]
    public class HighscoreList
    {
        public Highscore[] highscores;

        public HighscoreList(List<Highscore> l)
        {
            highscores = l.ToArray();
        }
    }
}