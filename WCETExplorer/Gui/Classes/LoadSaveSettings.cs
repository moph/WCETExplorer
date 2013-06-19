using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using EvolutionAlgo;
using System.Collections;

namespace Gui.Classes
{
    enum Strategy
    {
        elitism = 0,
        fitnessProp = 1,
        rang = 2,
    }

    public class LoadSaveSettings
    {
        private static string xsdPath = "settings.xsd";

        private settings settings;
        private bool success;

        /// <summary>
        /// Loads saved settings from file.
        /// </summary>
        /// <param name="file">The file to load from.</param>
        /// <param name="dllconfigfile">Variable that holds the loaded configfile path.</param>
        /// <param name="functionname">Variable that holds the loaded function name.</param>
        /// <param name="sp">Variable that holds the loaded Paramters</param>
        /// <param name="sa">Variable that holds the loaded Algosettings</param>
        public void load(string file, out string dllconfigfile, out string functionname, out Parameter sp, out AlgoSettings sa)
        {
            XmlReaderSettings set = new XmlReaderSettings();
            set.Schemas.Add(null, xsdPath);
            set.ValidationType = ValidationType.Schema;
            set.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(settings_ValidationEventHandler);
            using (XmlReader reader = XmlReader.Create(file, set))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(settings));
                success = true;
                settings = serializer.Deserialize(reader) as settings;
                if (!success)
                {
                    throw new ArgumentException("Something was wrong with the supplied file / filename: " + file);
                }

                sp = toParam(settings.function.manual);
                sa = toAlgo(settings.function.algorithm);
                dllconfigfile = settings.dll;
                functionname = settings.function.name;
            }
        }

        /// <summary>
        /// Saves currently active settings to a specified file.
        /// </summary>
        /// <param name="file">The file it is saved to.</param>
        /// <param name="dllconfigfile">Path to dll config xml file.</param>
        /// <param name="functionname">Name of currently selected function.</param>
        /// <param name="sp">Parameter object for all manual settings.</param>
        /// <param name="sa">AlgoSettings object for all algorithm settings.</param>
        public void save(string file, string dllconfigfile, string functionname, Parameter sp, AlgoSettings sa)
        {
            settings set = new settings();
            set.dll = dllconfigfile;
            set.function = new settingsFunction();
            set.function.name = functionname;
            set.function.algorithm = fromAlgo(sa);
            set.function.manual = fromParam(sp);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;
            settings.IndentChars = "    ";

            using (XmlWriter writer = XmlWriter.Create(file, settings))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(settings));
                serializer.Serialize(writer, set);
            }
        }

        private settingsFunctionAlgorithm fromAlgo(AlgoSettings sa)
        {
            settingsFunctionAlgorithm a = new settingsFunctionAlgorithm();
            a.crossover = sa.crossoverCount;
            a.mutation = sa.mutationRate;
            a.population = sa.populationSize;

            if (sa.strategy is Elitismus)
            {
                a.selection = (int)Strategy.elitism;
            }
            else if (sa.strategy is FittPropSelection)
            {
                a.selection = (int)Strategy.fitnessProp;
            }
            else if (sa.strategy is RangSelection)
            {
                a.selection = (int)Strategy.rang;
            }
            else
            {
                throw new Exception("Invalid Strategy read from xml.");
            }

            a.runtime = new settingsFunctionAlgorithmRuntime();
            a.runtime.selected = false;
            a.generations = new settingsFunctionAlgorithmGenerations();
            a.generations.selected = false;
            a.fitness = new settingsFunctionAlgorithmFitness();
            a.fitness.selected = false;
            foreach (StopCriterion s in sa.stop)
            {
                if (s is Runtime)
                {
                    a.runtime.selected = true;
                    a.runtime.Value = ((Runtime) s).runtime;
                }
                else if (s is maxGeneration)
                {
                    a.generations.selected = true;
                    a.generations.Value = ((maxGeneration) s).maxGen;
                }
                else if (s is Fitness)
                {
                    a.fitness.selected = true;
                    a.fitness.Value = ((Fitness) s).fitness;
                }
            }
            return a;
        }

        private AlgoSettings toAlgo(settingsFunctionAlgorithm a)
        {
            AlgoSettings sa = new AlgoSettings();
            sa.crossoverCount = a.crossover;
            sa.mutationRate = a.mutation;
            sa.populationSize = a.population;
            ArrayList bla = new ArrayList();
            if (a.runtime.selected) {
                bla.Add(new Runtime(a.runtime.Value));
            }
            if (a.fitness.selected)
            {
                bla.Add(new Fitness(a.fitness.Value));
            }
            if (a.generations.selected) {
                bla.Add(new maxGeneration(a.generations.Value));
            }
            sa.stop = bla.ToArray(typeof(StopCriterion)) as StopCriterion[];
            switch (a.selection)
            {
                case (int) Strategy.elitism:
                    sa.strategy = new Elitismus();
                    break;
                case (int) Strategy.fitnessProp:
                    sa.strategy = new FittPropSelection();
                    break;
                case (int) Strategy.rang:
                    sa.strategy = new RangSelection();
                    break;
                default:
                    throw new Exception("unsupported selection Strategy");
            }
            return sa;
        }

        private Parameter toParam(settingsFunctionManual set)
        {
            Parameter sp = new Parameter(set.floats, set.binaries, set.enums);
            return sp;
        }

        private settingsFunctionManual fromParam(Parameter p)
        {
            settingsFunctionManual m = new settingsFunctionManual();
            m.binaries = p.digital;
            m.enums = p.enums;
            m.floats = p.analog;
            return m;
        }

        /// <summary>
        /// Sets success to false when an xsd violation happens.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settings_ValidationEventHandler(Object sender, ValidationEventArgs e)
        {
            success = false;
        }

    }
}
