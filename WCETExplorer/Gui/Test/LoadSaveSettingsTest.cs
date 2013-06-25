using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gui.Classes;
using EvolutionAlgo;


namespace Gui.Test
{
    class LoadSaveSettingsTest
    {

        static void Main()
        {
            LoadSaveSettings lss = new LoadSaveSettings();

            AlgoSettings sa = new AlgoSettings();
            sa.crossoverCount = 10;
            sa.mutationRate = 0.5f;
            sa.populationSize = 111;
            sa.stop = new StopCriterion[] { new maxGeneration(111) };
            sa.strategy = new Elitismus();

            Parameter p = new Parameter(
                    new float[] {1.015f, 2.687f, 3.348f, 4.185f, 5.125f}, 
                    new bool[] {true, true, false}, 
                    new int[] {0, 3, 7, 4}); 

            string outfile = "testconf.xml";
            string dllconfig = @"D:\Documents\bla\bla\es_config.xml";
            string functionname = "execute_ES0";
            lss.save(outfile, dllconfig, functionname, p, sa);

            string infile = outfile;

            AlgoSettings saout; 
            Parameter spout;
            string dllout;
            string functionnameout;
            lss.load(infile, out dllout, out functionnameout, out spout, out saout);

            Console.WriteLine("dllout: " + dllout);
            Console.WriteLine("functionname: " + functionname);
            Console.WriteLine("algoSettings: ");
            Console.WriteLine("\tcrossover: " + saout.crossoverCount);
            Console.WriteLine("\tpopulationsize: " + saout.populationSize);
            Console.WriteLine("\tmutationrate: " + saout.mutationRate);
            Console.WriteLine("\tstrategy: " + saout.strategy.GetType());
            foreach (StopCriterion s in saout.stop)
            {
                Console.Write("\tstop: " + s.GetType());
                if (s is maxGeneration) { Console.WriteLine(((maxGeneration)s).maxGen);  }
                else if (s is Runtime)  { Console.WriteLine(((Runtime) s).runtime); }
                else if (s is Fitness)  { Console.WriteLine(((Fitness)s).fitness); }
                else { Console.WriteLine("## Fail ##"); }
            }

            Console.WriteLine("Parameter: ");
            foreach (float f in spout.analog) 
            {
                Console.WriteLine("\tfloat: " + f);
            }
            foreach (int i in spout.enums) 
            {
                Console.WriteLine("\tenum: " + i);
            }
            foreach (bool b in spout.digital) 
            {
                Console.WriteLine("\tboolean: " + b);
            }
        }
    }
}
