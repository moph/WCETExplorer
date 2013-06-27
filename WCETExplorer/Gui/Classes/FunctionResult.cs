using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvolutionAlgo;

namespace Gui.Classes
{
    class FunctionResult
    {
        public void Save_Result(String filepath, List<Genom> gwcetList, Genom manual_genom)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filepath, true))
            {
                int i = 0;

                if (manual_genom == null)
                {

                    foreach (Genom tmp in gwcetList)
                    {
                        file.WriteLine();
                        file.WriteLine(i + ". Genom");
                        file.WriteLine();
                        file.Write("WCET :");
                        file.Write(" " + tmp.fittness);

                        file.WriteLine(" ");
                        file.WriteLine("Parameter:");
                        file.WriteLine(" ");

                        file.Write("Analog :");

                        for (int j = 0; j < tmp._param.analog.Length; j++)
                        {
                            file.Write(tmp._param.analog[j] + ", ");
                        }

                        file.WriteLine();
                        file.Write("Digital :");
                        for (int j = 0; j < tmp._param.digital.Length; j++)
                        {
                            file.Write(tmp._param.digital[j] + ", ");
                        }

                        file.WriteLine();

                        file.Write("Enum :");
                        for (int j = 0; j < tmp._param.enums.Length; j++)
                        {
                            file.Write(tmp._param.enums[j] + ", ");
                        }
                        file.WriteLine();
                        file.WriteLine();

                        i++;
                    }
                }
                else if(gwcetList == null)
                {
                        file.WriteLine("<<Manual>>");
                        file.WriteLine();
                        file.Write("WCET :");
                        file.Write(" " + manual_genom.fittness);

                        file.WriteLine(" ");
                        file.WriteLine("Parameter:");
                        file.WriteLine(" ");

                        file.Write("Analog :");

                        for (int j = 0; j < manual_genom._param.analog.Length; j++)
                        {
                            file.Write(manual_genom._param.analog[j] + ", ");
                        }

                        file.WriteLine();
                        file.Write("Digital :");
                        for (int j = 0; j < manual_genom._param.digital.Length; j++)
                        {
                            file.Write(manual_genom._param.digital[j] + ", ");
                        }

                        file.WriteLine();

                        file.Write("Enum :");
                        for (int j = 0; j < manual_genom._param.enums.Length; j++)
                        {
                            file.Write(manual_genom._param.enums[j] + ", ");
                        }
                        file.WriteLine();
                        file.WriteLine();
                    }
                }

            }
        }
 }
