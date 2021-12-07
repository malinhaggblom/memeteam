using System;
using System.IO;
using Xunit;
using Groupwork;

namespace AgileGroupWorkTest
{
    public class ProgramTest
    {
        [Fact]
        public void ProgramPrintsHelloWorld()
        {
            using (StringWriter sw = new StringWriter())
            {
                sw.NewLine = "\n";
                TextWriter stdout = Console.Out;
                Console.SetOut(sw);
                Program.Main(null);
                Console.SetOut(stdout);

                Assert.Contains("Hello World", sw.ToString());
            }
        }
    }
}
