using System;
using System.IO;
using System.Windows.Forms;

namespace myExtension
{
    public class OurMethods
    {

        /// <summary>
        /// Método usado para ler o arquivo que seria o programa em PasC de acordo com o botão pressionado.      
        /// </summary>
        /// <param name="nomeArquivo">string com o nome do arquivo que será lido</param>
        /// <returns>Mensagem de teste com o conteúdo do arquivo.</returns>
        /// <remarks>Deve ser implementado quando for necessário ler um novo arquivo de programa PasC.</remarks>
        public static void lerArquivo(String nomeArquivo)
        {
            string codePath = Path.Combine(@Environment.CurrentDirectory, nomeArquivo);

            if (File.Exists(codePath))
            {
                Stream entrada = File.Open(codePath, FileMode.Open);
                StreamReader readText = new StreamReader(entrada);
                string str = readText.ReadToEnd().ToUpper();

                MessageBox.Show(str);

                readText.Close();
                entrada.Close();
            }
        }



    }
}

