using System;
using System.Collections;
using System.Net;
using System.IO;
using System.Collections.Generic;

namespace SA
{
   
    class Program
    {

        private static List<string> listA { get; set; }
        private static List<Double> listB { get; set; }
        private static List<string> listC { get; set; }

        static void Main(string[] args)
        {
            readCSV();
            simulatedAnnealing();
        }

         public static void readCSV() {
            int ctr = 0;
            using (var reader = new StreamReader(@"../../../Downloads/museMonitor_test.csv"))
            {
                 listA = new List<string>();
                 listB = new List<Double>();
                 listC = new List<string>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    listA.Add(values[0]);
                    if ((ctr > 2) && (values[1]!=""))
                    {
                        listB.Add(Convert.ToDouble(values[1]));
                        listC.Add(values[2]);
                    }
                        ctr++;
                   
                }

                for (int i = 0; i < 10; i++)
                    Console.WriteLine(listB[i]);
 
            }// using

        }

        private static void simulatedAnnealing() {
            double min = 0.0;
            List<int> kAccept = new List<int>(); 
            Random newVal = new Random();
            double T = 1;
            double mu_k = 0.85;
            int index = 0; int curr = 0;
            double objFuncOld = listB[0];
            double objFunc = 0.0;
            int value = 0;
            for (int i = 0; i < listB.Count; i++)
            {

                  value = newVal.Next(0, listB.Count);

                if (kAccept.Contains(value)&&(listB[value]< objFunc)) {
                        min = objFunc;
            } else if (listB[value]<objFunc ) {
                    curr = i;
                    kAccept.Add(curr);
                } else 
                {
                    T = T / mu_k;
                      objFunc = listB[value];
                    double prob = Math.Exp((objFunc - objFuncOld) / T);

                    if (prob > newVal.NextDouble())
                    {
                        kAccept.Add(value); 
                        curr = value; 
                    }
                } // else
            } // for
            Console.WriteLine(" maximum " + min);
            }

        }

}
