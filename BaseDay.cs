using System;
using System.IO;

namespace AOC2021 {

    class BaseDay {

        public virtual bool Debug { get; protected set; }

        public virtual long SolutionPart1 { get; protected set; }
        public virtual long SolutionPart2 { get; protected set; }

        private double startTicks;
        private string currLabel;

        public void StartDay() {
            if (Debug) { StartTime(GetDay()); }

            Solve();
        }

        protected virtual void Solve() { }

        public void EndDay() {
            if (Debug) { StopTime(); }

            int index = new Random().Next(0, Art.Solution.Length);
            string waves = Art.Solution.Substring(index) + Art.Solution.Substring(0, index);
            string log = string.Format("{0}  Part 1: {1}  Part 2: {2}", waves, SolutionPart1 != 0 ? SolutionPart1.ToString() : "X", SolutionPart2 != 0 ? SolutionPart2.ToString() : "X");
            while (log.Length > 90) {
                waves = waves.Remove(waves.Length - 1); 
                log = string.Format("{0}  Part 1: {1}  Part 2: {2}", waves, SolutionPart1 != 0 ? SolutionPart1.ToString() : "X", SolutionPart2 != 0 ? SolutionPart2.ToString() : "X");
            }
            Log(log);
        }

        protected void Log(string s) {
            Console.WriteLine(string.Format("{0}{1} {2} {3}", "  ", GetDay(), string.Format("{0}{1}", SolutionPart1 != 0 ? "*" : " ", SolutionPart2 != 0 ? "*" : " "), s));
        }

        protected void StartTime(string label) {
            Console.WriteLine(string.Format("Start "+label));
            startTicks = DateTime.Now.Ticks;
            currLabel = label;
        }

        protected void StopTime() {
            Console.WriteLine(string.Format("Stop {0} {1} s", currLabel, (DateTime.Now.Ticks - startTicks) / TimeSpan.TicksPerSecond));
        }

        protected string[] GetInput() {
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string text = Path.Combine(path, GetDay(), Debug ? "example.txt" : "input.txt");
            return File.ReadAllLines(text);
        }

        private string GetDay() {
            return GetType().Name;
        }
    }

}
