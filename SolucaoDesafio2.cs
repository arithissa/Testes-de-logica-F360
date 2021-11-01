// Solução por Aríthissa Vitória

using System;
using System.IO;
using System.Collections;

// Classe principal
class Program {
  public static void Main (string[] args) {

    try{
    StreamReader file = new StreamReader("filename.txt");
    fileHandler fileReading = new fileHandler(file);

    fileReading.readFile();
    fileReading.printValues();

    file.Close();

    } catch(Exception e) {
      Console.WriteLine("Não foi possível abrir o arquivo.");
      Console.WriteLine("Exception: " + e.Message);
    } finally {
      Console.WriteLine("Execução finalizada");
    }
    
  }
}

// Classe para manipulação de arquivos
class fileHandler{
  private StreamReader file;
  private int salePos, saleChars, installmmentsPos, installmmentsChars, totalInstallments;
  ArrayList saleValues;

  // Construtor
  public fileHandler(StreamReader file){
    this.file = file;
    salePos = 85;
    saleChars = 11;
    installmmentsPos = 173;
    installmmentsChars = 2;
  }

  // Função para leitura do arquivo
  public void readFile(){
    string line = file.ReadLine();
    char[] identifier = new char[2];
    char[] aux = new char[line.Length];
    int numeric;

    saleValues = new ArrayList();

    // Percorre o arquivo até o final
    while(line != null) {
      using (StringReader sr = new StringReader(line)){
        sr.Read(identifier, 0, 2);
      
        // Verifica se a linha inicia com 1 e armazena os valores
        if((numeric = (int)Char.GetNumericValue(identifier[0])) == 1){
          sr.Read(aux, 0, line.Length);
        
          saleValues.Add(findValues(aux, salePos, saleChars));
          totalInstallments = findValues(aux, installmmentsPos, installmmentsChars);

        }
      }

      line = file.ReadLine();
    }

  }

// Função específica da classe para encontrar os valores
  private int findValues(char[] aux, int pos, int charNum){
    char[] charValue = new char[charNum];
    int j = 0, value;

    for(int i = pos; i < pos + charNum; i++)
            charValue[j++] = aux[i];

    string stringValue = new string(charValue);
    value = Convert.ToInt32(stringValue);

    return value;
  }

  // Função para imprimir os valores encontrados
  public void printValues(){
    Console.Write("Todos os valores da venda: \n");
    
    for(int i = 0; i < saleValues.Count; i++)
      Console.Write(saleValues[i] + "\n");

    Console.Write("Quantidade de parcelas: ");
    Console.Write(totalInstallments + "\n");

      return;
  }

  // Getter da lista de todos os valores da venda
  public ArrayList getAllSaleValues(){
    return saleValues;
  }

}