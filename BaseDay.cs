using System;
using System.IO;

namespace AOC2021 {

    class BaseDay {

        public virtual bool Debug { get; protected set; }

        public virtual string SolutionPart1 { get; private set; } = "X";
        public virtual string SolutionPart2 { get; private set; } = "X";

        private double startTicks;
        private string currLabel;

        public void StartDay() {
            if (Debug) { StartTime(GetDay()); }

            Solve();
        }

        protected void SetAnswerPart1(long answer) {
            SetAnswerPart1(answer.ToString());
        }

        protected void SetAnswerPart1(string answer) {
            SolutionPart1 = answer;
        }

        protected void SetAnswerPart2(long answer) {
            SetAnswerPart2(answer.ToString());
        }

        protected void SetAnswerPart2(string answer) {
            SolutionPart2 = answer;
        }

        protected virtual void Solve() { }

        public void EndDay() {
            if (Debug) { StopTime(); }

            int index = new Random().Next(0, Art.Solution.Length);
            string waves = Art.Solution.Substring(index) + Art.Solution.Substring(0, index);
            string log = string.Format("{0}  Part 1: {1}  Part 2: {2}", waves, SolutionPart1, SolutionPart2);
            while (log.Length > 90) {
                waves = waves.Remove(waves.Length - 1); 
                log = string.Format("{0}  Part 1: {1}  Part 2: {2}", waves, SolutionPart1, SolutionPart2);
            }
            Log(log);
        }

        protected void Log(string s) {
            Console.WriteLine(string.Format("{0}{1} {2} {3}", "  ", GetDay(), string.Format("{0}{1}", SolutionPart1 != "X" ? "*" : " ", SolutionPart2 != "X" ? "*" : " "), s));
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
