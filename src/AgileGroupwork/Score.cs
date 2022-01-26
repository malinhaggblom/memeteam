namespace MemeTeamPro
{
    using System;
    public class Score
    {
        public string name { get; set; }
        public int score { get; set; }
        public string type { get; set; }

        public Score(string name, int score, string type)
        {
            this.name = name;
            this.score = score;
            this.type = type;
        }
        public override string ToString()
        {
            return this.name + ", score" + this.score + " type of score, " + this.type;
        }

    }
}