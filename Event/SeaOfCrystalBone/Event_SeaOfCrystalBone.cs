using ElementMachine.Bases.Support;
using ElementMachine.Event.SeaOfCrystalBone.Boss.CrystalBoneWhale;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ElementMachine.Event.SeaOfCrystalBone
{
    public class Event_SeaOfCrystalBone : EventBase
    {
        public static Event_SeaOfCrystalBone Instance;
        public Event_SeaOfCrystalBone()
        {
            Instance = this;
        }
        public int killcount;
        public override string EventTexture => "ElementMachine/Event/SeaOfCrystalBone/Event_SeaOfCrystalBone";
        public override void ModifyInvasionProgress(ref string text, ref Color color)
        {
            color = Color.DarkGreen;
            switch (Language.ActiveCulture.LegacyId)
            {
                default:
                case 1://"English":
                    {
                        text = "Event_SeaOfCrystalBone";
                        break;
                    }
                case 2:// "German":
                    {
                        text = "Event_SeaOfCrystalBone";
                        break;
                    }
                case 3:// "Italian":
                    {
                        text = "Event_SeaOfCrystalBone";
                        break;
                    }
                case 4:// "French":
                    {
                        text = "Event_SeaOfCrystalBone";
                        break;
                    }
                case 5:// "Spanish":
                    {
                        text = "Event_SeaOfCrystalBone";
                        break;
                    }
                case 6:// "Russian":
                    {
                        text = "Event_SeaOfCrystalBone";
                        break;
                    }
                case 7:// "Chinese":
                    {
                        switch (Wave)
                        {
                            default:
                            case 1:
                                {
                                    text = "晶骨骇浪";
                                    break;
                                }
                        }
                        break;
                    }
                case 8:// "Portuguese":
                    {
                        text = "Event_SeaOfCrystalBone";
                        break;
                    }
                case 9:// "Polish":
                    {
                        text = "Event_SeaOfCrystalBone";
                        break;
                    }
            }
        }
        public override void ReportProgress(out int value, out int maxvalue)
        {
            int bosstype = ModContent.NPCType<CrystalBoneWhale>();
            var boss = NPCSupport.FindNPCFirstOrNull(npc => npc.type == bosstype);
            value = 1;
            maxvalue = 1;
        }
        public override void ModifyNPCSpawn(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            pool.Clear();
            int bosstype = ModContent.NPCType<CrystalBoneWhale>();
            if (NPCSupport.FindNPCFirstOrNull(npc => npc.type == bosstype) is null)
            {
                //pool.Add(ModContent.NPCType<CrystalBoneWhale>(), 1);
                //pool.Add(ModContent.NPCType<PlagueBee>(), 1);
            }
        }
        public override void ModifyNPCSpawnRate(Player player, ref int spawnrate, ref int maxspawn)
        {
            spawnrate = (int)(spawnrate * 0.2f);
            maxspawn = (int)(maxspawn * 2.2f);
        }
        public override void Update()
        {
        }
        public override void OnStart()
        {
            Wave = 1;
            killcount = 0;
        }
        public override void OnEnd()
        {
            Wave = 1;
            killcount = 0;
        }
    }
}