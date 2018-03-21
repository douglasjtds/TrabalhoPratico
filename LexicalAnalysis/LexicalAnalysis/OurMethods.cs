using System;
using System.IO;
using System.Windows.Forms;
using System.Text;

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
            StringBuilder completeWord = new StringBuilder();

            Stream entrada = File.Open(codePath, FileMode.Open);
            StreamReader readText = new StreamReader(entrada);

            int countLine = 0;

            if (File.Exists(codePath))
            {
                do
                {
                    char currentCharacter = (char)readText.Read();

                    if(!char.IsWhiteSpace(currentCharacter)){  //Senão for um espaço em branco, adiciona na StringBiulder
                        completeWord.Append(currentCharacter);
                        countLine++;
                    }
                    else
                    {
                        MessageBox.Show(completeWord.ToString());
                        completeWord.Clear();
                    }

                } while (!readText.EndOfStream);


                readText.Close();
                entrada.Close();

            }
        }

    }
}

