using System;
using System.IO;

namespace myExtension
{
    public static class OurMethods
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lerArquivo"></param>
        /// <param name="nomeArquivo">Método usado para ler o arquivo de acordo com o </param>
        public static void lerArquivo(String nomeArquivo)
        {
            string codePath = Path.Combine(@Environment.CurrentDirectory, nomeArquivo);

            if (File.Exists(codePath))
            {
                Stream entrada = File.Open(codePath, FileMode.Open);
                StreamReader readText = new StreamReader(entrada);
                string str = readText.ReadToEnd();

                MessageBox.Show(str);

                readText.Close();
                entrada.Close();
            }
        }



    }
}

