﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
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
#if DEBUG
            if (SteamAppsLocation.Get() == "./../../debug")
            {
                dotaProcessHandle = Process.GetProcessesByName("notepad")[0].MainWindowHandle;
                overlayManager = new OverlayManager(dotaProcessHandle, out window, out renderer);
                renderer.SetupHintSlots();
                return;
            }
#endif
            while (Process.GetProcessesByName("dota2").Length == 0)
            {
                Thread.Sleep(500);
            }

            dotaProcessHandle = Process.GetProcessesByName("dota2")[0].MainWindowHandle;
            overlayManager = new OverlayManager(dotaProcessHandle, out window, out renderer);
            renderer.SetupHintSlots();
            Console.WriteLine("Overlay running!");
        }

        public IntPtr GetOverlayHandle()
        {
            return dotaProcessHandle;
        }

        public void Clear()
        {
            renderer.Clear();
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

        public void AddHPs(double[] newhps, double[] newmaxhps)
        {
            renderer.UpdateHeroHPGraph(newhps, newmaxhps);
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

        public void ShowInstructionMessage(double positionX, double positionY, IntPtr visualCustomizeHandle)
        {
            renderer.Intructions_Draw(dotaProcessHandle, window, (float)positionX, (float)positionY, visualCustomizeHandle);
        }

        public void ToggleHighlight(bool tog = true)
        {
            renderer.ToggleHightlight(tog);
        }

        public void UpdateHighlight(Dictionary<int, List<Tuple<String, String, String>>> ticks, float maxTick)
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

        public void ClearMessage(Direct2DRenderer.Hints hint)
        {
            renderer.DeleteMessage(hint);
        }

        public void ClearHeroSuggestion()
        {
            renderer.DeleteMessage(Direct2DRenderer.Hints.hero_selection_1);
            renderer.DeleteMessage(Direct2DRenderer.Hints.hero_selection_2);
            renderer.DeleteMessage(Direct2DRenderer.Hints.hero_selection_3);
            renderer.DeleteMessage(Direct2DRenderer.Hints.hero_selection_4);
            renderer.DeleteMessage(Direct2DRenderer.Hints.hero_selection_5);
        }

        public void ClearItemSuggestion()
        {
            renderer.DeleteMessage(Direct2DRenderer.Hints.items_selection);
        }

        public void ClearHeroInfo()
        {
            renderer.DeleteMessage(Direct2DRenderer.Hints.heroinformation);
        }

        public void ClearRetreat()
        {
            renderer.DeleteMessage(Direct2DRenderer.Hints.retreat);
        }

        public void XorCheck(int code)
        {
            renderer.HeroSelectionFeedBack(code);
        }
    }
}
