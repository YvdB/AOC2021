using System;
using System.IO;

namespace AOC2021 {

    class BaseDay {

        public virtual bool Debug { get; protected set; }

        private double startTicks;
        private string currLabel;

        public virtual void Solve() { }

        protected void Log(string s) {
            Console.WriteLine(string.Format("{0}{1} - {2}", Debug ? "[DEBUG]" : "", GetDay(), s));
        }

        protected void StartTime(string label) {
            Console.WriteLine(string.Format("Start "+label));
            startTicks = DateTime.Now.Ticks;
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
