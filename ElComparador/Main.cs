using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ElComparador
{
    class Main
    {
        public string Execute (string file1, string file2, int[] fields1, int[] fields2)
        {
            string result = "";

            string text1 = File.ReadAllText(file1);
            string[] lines1 = text1.Split('\n');
            string[] header1 = lines1[0].Split(';');

            string text2 = File.ReadAllText(file2);
            string[] lines2 = text2.Split('\n');
            string[] header2 = lines2[0].Split(';');

            if (lines1.Length != lines2.Length)
                return "Os CSV's não possuem a mesma quantidade de linhas";

            int compared = 0, error = 0;
            for (int i = 1; i<lines1.Length; i++)
            {
                string[] values1 = lines1[i].Split(';');
                string[] values2 = lines2[i].Split(';');

                if (values1.Length > 1)
                {
                    for (int j = 0; j < fields1.Length; j++)
                    {
                        if (values1[fields1[j]].Equals(values2[fields2[j]]))
                            compared++;
                        else
                        {
                            compared++;
                            error++;

                            string errorMessage = $"Campo {((Form1)Application.OpenForms[0]).GetSelectedFieldName(fields1[j], 1)} do primeiro arquivo diferente do campo {((Form1)Application.OpenForms[0]).GetSelectedFieldName(fields2[j], 2)} do segundo arquivo na linha {i + 1}";
                            result += errorMessage + "\n\n";
                        }
                    }
                }   

                ((Form1)Application.OpenForms[0]).UpdateProgressBar(lines1.Length, i+1);
            }

            return $"Comparado {compared}, {error} discrepâncias\n\nDiferenças encontradas:\n\n{result}";
        }
    }
}
