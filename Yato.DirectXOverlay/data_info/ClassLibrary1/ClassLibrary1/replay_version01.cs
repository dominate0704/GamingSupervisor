﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace replayParse
{


    public class replay_version01
    { 
        public static int[,,] replayinfo = new int[200000, 10, 4];
        private int[,] prev_stat = new int[10, 4]; // first index is heroID for this match, the second index is some info: 0: health, 1: cell_x, 2: cell_y, 3 cell_z;
        public static Dictionary< string, int> heros = new Dictionary<string, int>();
        private int[] sideOfHero = new int[10];  // the index is the heroID: the sort of ID show the sequence of picking , the number in string shows the side of heros. 0: for one side, 1 : for another side.
        public int offsetTic = 0;
        public replay_version01() {
            
            string[] lines = System.IO.File.ReadAllLines(@"X:\data_info\replay.txt");
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
                            for (int j = 0; j < 3; j++)
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
                if (words[1].Contains('P'))
                {
                    mode = 1;
                }
                string[] substrings = Regex.Split(words[2], "Hero_");
                if (!heros.Keys.Contains(substrings[1]))
                {
                    heros.Add(substrings[1], value);
                    heroID = heros[substrings[1]];
                    if (Int32.Parse(words[3]) > 100)
                    {
                        sideOfHero[heroID] = 1;
                    }
                    value++;
                }
                else
                {
                    heroID = heros[substrings[1]];
                }
                if (mode == 1)
                {
                    if (words.Length < 6)
                    {
                        throw new System.ArgumentOutOfRangeException("lost position information");
                    }
                       
                    replayinfo[time - offsetTic, heroID, 1] = Int32.Parse(words[3]);
                    replayinfo[time - offsetTic, heroID, 2] = Int32.Parse(words[4]);
                    replayinfo[time - offsetTic, heroID, 3] = Int32.Parse(words[5]);
                    prev_stat[heroID, 1] = Int32.Parse(words[3]);
                    prev_stat[heroID, 2] = Int32.Parse(words[4]);
                    prev_stat[heroID, 3] = Int32.Parse(words[5]);
                }
                else
                {
                    replayinfo[time - offsetTic, heroID, 0] = Int32.Parse(words[3]);
                    prev_stat[heroID, 0] = Int32.Parse(words[3]);
                }
            }
        }

        public replay_version01(string fileAddress)
        {

            string[] lines = System.IO.File.ReadAllLines(fileAddress);
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
                            for (int j = 0; j < 3; j++)
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
                if (words[1].Contains('P'))
                {
                    mode = 1;
                }
                string[] substrings = Regex.Split(words[2], "Hero_");
                if (!heros.Keys.Contains(substrings[1]))
                {
                    heros.Add(substrings[1], value);
                    heroID = heros[substrings[1]];
                    if (Int32.Parse(words[3]) > 100)
                    {
                        sideOfHero[heroID] = 1;
                    }
                    value++;
                }
                else
                {
                    heroID = heros[substrings[1]];
                }
                if (mode == 1)
                {
                    replayinfo[time - offsetTic, heroID, 1] = Int32.Parse(words[3]);
                    replayinfo[time - offsetTic, heroID, 2] = Int32.Parse(words[4]);
                    replayinfo[time - offsetTic, heroID, 3] = Int32.Parse(words[5]);
                    prev_stat[heroID, 1] = Int32.Parse(words[3]);
                    prev_stat[heroID, 2] = Int32.Parse(words[4]);
                    prev_stat[heroID, 3] = Int32.Parse(words[5]);
                }
                else
                {
                    replayinfo[time - offsetTic, heroID, 0] = Int32.Parse(words[3]);
                    prev_stat[heroID, 0] = Int32.Parse(words[3]);
                }
            }
        }

        public Dictionary<string, int> getHeros()
        {
            return heros;
        }

        public int[,,] getReplayInfo()
        {
            return replayinfo;
        }
        public int  getOffSet()
        {
            return offsetTic;
        }
         public int[] getHeroSide()
        {
            return sideOfHero;
        }
    }
}