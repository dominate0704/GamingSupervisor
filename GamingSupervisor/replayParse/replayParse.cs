﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace replayParse
{

    public class replay_version01
    {
        public static double[,,] replayinfo = new double[200000, 10, 15]; // first index is tic for this match (minus by offsetTic), second index is heroID for this match, the third index is some info: 0: health, 1: cell_x, 2: cell_y, 3 cell_z,
                                                                          // 4:LEVEL, 5:MANA, 6:STRENGTH, 7: AGILITY, 8: INTELLECT, 9:MAXHEALTH, 10:MANAREGEN, 11: HEALTHREGEN, 12:MOVEMENTSPEED,\
                                                                          // 13:DAMAGEMIN, 14:DAMAGEMAX;
        private double[,] prev_stat = new double[10, 15]; // first index is heroID for this match, the second index is some info: 0: health, 1: cell_x, 2: cell_y, 3 cell_z,
                                                   // 4:LEVEL, 5:MANA, 6:STRENGTH, 7: AGILITY, 8: INTELLECT, 9:MAXHEALTH, 10:MANAREGEN, 11: HEALTHREGEN, 12:MOVEMENTSPEED,\
                                                   // 13:DAMAGEMIN, 14:DAMAGEMAX;
        public static Dictionary<string, int> heros = new Dictionary<string, int>();
        public int[] sideOfHero = new int[10];  // the index is the heroID: the sort of ID show the sequence of picking , the number in string shows the side of heros. 0: for one side, 1 : for another side.
        public int offsetTic = 0;    // offset is the first tic in the replay file.

        public replay_version01(string dataFolderLocation)
        {
            string s = dataFolderLocation + "hero.txt";

            string[] lines = System.IO.File.ReadAllLines(s);
            int tic = 0;
            int value = 0;
            foreach (string line in lines)
            {
                string[] words = line.Split(' ');
                int time = Int32.Parse(words[0]);
                if (tic == 0)
                {
                    offsetTic = time;
                    tic = time;
                }
                else
                {
                    while (time > tic)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            for (int j = 0; j < 15; j++)
                            {
                                if (prev_stat[i, j] != 0)
                                {
                                    replayinfo[tic - offsetTic, i, j] = prev_stat[i, j];
                                }
                            }
                        }
                        tic++;
                    }
                    tic = time;
                }
                int mode = 0;
                int heroID = 0;
                //4:LEVEL, 5:MANA, 6:STRENGTH, 7: AGILITY, 8: INTELLECT, 9:MAXHEALTH, 10:MANAREGEN, 11: HEALTHREGEN, 12:MOVEMENTSPEED,\
                                                   // 13:DAMAGEMIN, 14:DAMAGEMAX;
                if (words[1].Contains("POSITION"))
                {
                    mode = 1;
                }
                else if (words[1].Contains("LEVEL"))
                {
                    mode = 4;
                }
                else if (words[1].Contains("MANAREGEN"))
                {
                    mode = 10;
                }
                else if (words[1].Contains("MANA"))
                {
                    mode = 5;
                }
                else if (words[1].Contains("STRENGTH"))
                {
                    mode = 6;
                }
                else if (words[1].Contains("AGILITY"))
                {
                    mode = 7;
                }
                else if (words[1].Contains("INTELLECT"))
                {
                    mode = 8;
                }
                else if (words[1].Contains("MAXHEALTH"))
                {
                    mode = 9;
                }
                else if (words[1].Contains("HEALTHREGEN"))
                {
                    mode = 11;
                }
                else if (words[1].Contains("MOVEMENTSPEED"))
                {
                    mode = 12;
                }
                else if (words[1].Contains("DAMAGEMIN"))
                {
                    mode = 13;
                }
                else if (words[1].Contains("DAMAGEMAX"))
                {
                    mode = 14;
                }
                string[] substrings = Regex.Split(words[2], "Hero_");
                var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);
                string name = r.Replace(substrings[1], "");
                name = string.Join("", name.Split(new string[] { "_" }, StringSplitOptions.None));
                name = name.ToLower();
                if (name.Contains("never"))
                {
                    name = "shadowfiend";
                }
                if (name.Contains("obsidian"))
                {
                    name = "outworlddevourer";
                }
                if (name.Contains("wisp"))
                {
                    name = "io";
                }
                if (name.Contains("magnataur"))
                {
                    name = "magnus";
                }
                if (name.Contains("treant"))
                {
                    name = "treantprotector";
                }
                if (!heros.Keys.Contains(name))
                {
                    heros.Add(name, value);
                    heroID = heros[name];
                    if (double.Parse(words[3]) > 100)
                    {
                        sideOfHero[heroID] = 1;
                    }
                    value++;
                }
                else
                {
                    heroID = heros[name];
                }
                if (mode == 1)
                {
                    if (words.Length < 6)
                    {
                        throw new System.ArgumentOutOfRangeException("lost position information");
                    }

                    replayinfo[time - offsetTic, heroID, 1] = double.Parse(words[3]);
                    replayinfo[time - offsetTic, heroID, 2] = double.Parse(words[4]);
                    replayinfo[time - offsetTic, heroID, 3] = double.Parse(words[5]);
                    prev_stat[heroID, 1] = double.Parse(words[3]);
                    prev_stat[heroID, 2] = double.Parse(words[4]);
                    prev_stat[heroID, 3] = double.Parse(words[5]);
                }
                else
                {
                    replayinfo[time - offsetTic, heroID, mode] = double.Parse(words[3]);
                    prev_stat[heroID, mode] = double.Parse(words[3]);
                }
            }
        }


        /*
         * output: the dictionary of name of the hero in this replay but the name is in lowercase and without the space. the int in the dictionary is the sort of ID show the sequence of picking(such as 1 to 10)
         */
        public Dictionary<string, int> getHerosLowercase()
        {
            return heros;
        }


        /*
         *   output the big double matrix contains the infomation of the replay
         *   first index is tic for this match (minus by offsetTic), second index is heroID for this match, the third index is some info: 0: health, 1: cell_x, 2: cell_y, 3 cell_z,
         *                                                                4:LEVEL, 5:MANA, 6:STRENGTH, 7: AGILITY, 8: INTELLECT, 9:MAXHEALTH, 10:MANAREGEN, 11: HEALTHREGEN, 12:MOVEMENTSPEED,
         *                                                                13:DAMAGEMIN, 14:DAMAGEMAX;
         */
        public double[,,] getReplayInfo()
        {
            return replayinfo;
        }

        /*
         * offset is the first tic in the replay file(named: hero.txt).
         */
        public int getOffSet()
        {
            return offsetTic;
        }

        /*
         * The index is the heroID: the sort of ID show the sequence of picking , the number in string shows the side of heros. 0: for one side, 1 : for another side.
         * Not actually useful.
         */
        public int[] getHeroSide()
        {
            return sideOfHero;
        }
    }
}