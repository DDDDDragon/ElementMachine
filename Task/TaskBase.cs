using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;

namespace ElementMachine.Tasks
{
    public class TaskManager
    {
        public static void Load()
        {
            #region 遍历Task
            var tasks = typeof(TaskBase).Assembly.GetTypes()
                .Where(t => typeof(TaskBase).IsAssignableFrom(t))
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => (TaskBase)Activator.CreateInstance(t)).ToList();
            #endregion
            foreach(var i in tasks)
            {
                i.Load();
                i.type = Tasks.Count + 1;
                Type type = typeof(ContentInstance<>).MakeGenericType(i.GetType());
                PropertyInfo p = type.GetProperty("Instance", BindingFlags.Static | BindingFlags.Public);
                p.SetValue(typeof(ContentInstance<>), i);
                Tasks.Add(i);
            }
        }
        public bool TryAddTask(TaskBase task)
        {
            if (Main.LocalPlayer.GetModPlayer<MyPlayer>().NowTasks.Where(t => t.NPC == task.NPC).Count() > 0) return false;
            else Main.LocalPlayer.GetModPlayer<MyPlayer>().NowTasks.Add(task);
            return true;
        }
        public static T GetTask<T>() where T : class => ContentInstance<T>.Instance;
        public static int TaskType<T>() where T : TaskBase => GetTask<T>()?.type ?? 0;
        public static List<TaskBase> Tasks = new List<TaskBase>();
    }
    public abstract class TaskBase
    {
        public delegate void ConvDelegate(int index);
        public event ConvDelegate Events;
        public List<string> Conv = new List<string>();
        public List<string> Trans = new List<string>();
        public string Name;
        public string TransName;
        public bool CanRepeat = false;
        public int NPC;
        public int type;
        public int ConvIndex = 0;
        public int DescriptionIndex = 0;
        public void Load()
        {
            LoadConv();
        }
        public virtual string NextConv()
        {
            string ret = GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? Conv[ConvIndex] : Trans[ConvIndex];
            RunConvEvent();
            if (ConvIndex == DescriptionIndex && IsComplete())
            {
                OnComplete();
            }
            if (DescriptionIndex > ConvIndex || (DescriptionIndex <= ConvIndex && IsComplete())) ConvIndex++;
            return ret;
        }
        public virtual void LoadConv()
        {

        }
        public void AddConv(string conv, string trans = "")
        {
            Conv.Add(conv);
            if (trans != "") Trans.Add(trans);
        }
        public void AddTrans(string conv)
        {
            Trans.Add(conv);
        }
        public virtual bool IsComplete()
        {
            return false;
        }
        public virtual void OnComplete()
        {
            Main.NewText(Name + "已完成！", Color.Gold);
        }
        public void RunConvEvent()
        {
            Events?.Invoke(ConvIndex);
        }
        public void AddEvent(ConvDelegate ConvEvent)
        {
            Events += ConvEvent;
        }
    }
    public static class ContentInstance<T> where T : class
    {
        public static T Instance { get; private set; }
    }
}
