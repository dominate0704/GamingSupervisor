﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Yato.DirectXOverlay;

namespace GamingSupervisor
{
    class Overlay
    {
        private OverlayManager overlayManager = null;
        private OverlayWindow window = null;
        public Direct2DRenderer renderer;
        private IntPtr dotaProcessHandle;

        public Overlay()
        {
            dotaProcessHandle = Process.GetProcessesByName("dota2")[0].MainWindowHandle;
            overlayManager = new OverlayManager(dotaProcessHandle, out window, out renderer);
            renderer.SetupHintSlots();
            Console.WriteLine("Overlay running!");
        }

        public void Clear()
        {
            renderer.clear();
        }

        public void AddRetreatMessage(string message, string img)
        {
            renderer.Retreat(message, img);
        }

        public void AddHeroesSuggestionMessage(string[] heroes, string[] imgs)
        {
            renderer.HeroSelectionHints(heroes, imgs);
        }
        
        public void AddItemSuggestionMessage(string message, string img)
        { 
            renderer.ItemSelectionHints(message, img);
        }
        public void AddHeroInfoMessage(string message, string img)
        {
            renderer.HeroInfoHints(message, img);
        }

        public void ToggleGraphForHeroHP(bool tog = true)
        {
            renderer.ToggleGraph(tog);
        }

        public void AddHPs(double[] newhps)
        {
            renderer.UpdateHeroHPGraph(newhps);
        }

        public void AddHp(double newhp)
        {
            renderer.UpdateHeroHPQueue(newhp);
        }

        public void AddHeroGraphIcons(List<int> graphsIds)
        {
            renderer.UpdateHeroHpGraphIcons(graphsIds);
        }
        
        public void Intructions_setup(string content)
        {
            renderer.Intructions_setup(content);
        }

        public void ShowInstructionMessage()
        {
            renderer.Intructions_Draw(dotaProcessHandle, window);
        }

        public void ToggleHighlight(bool tog = true)
        {
            renderer.ToggleHightlight(tog);
        }

        public void UpdateHighlight(Dictionary<int, List<Tuple<String, String, String>>> ticks, int maxTick)
        {
            renderer.UpdateHighlightTime(ticks, maxTick);
        }

        public void ShowIngameMessage()
        {
            renderer.Ingame_Draw(dotaProcessHandle, window);
        }

        public void ShowDraftMessage()
        {
            renderer.HeroSelection_Draw(dotaProcessHandle, window);
        }

        public void ClearMessage(int MessageNum)
        {
            renderer.DeleteMessage(MessageNum);
        }

        public void XorCheck(int code)
        {
            renderer.HeroSelectionFeedBack(code);
        }
    }
}
