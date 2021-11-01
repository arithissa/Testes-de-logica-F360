// Solução por Aríthissa Vitória

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


// Classe principal
class Program {
  public static void Main (string[] args) {

    try{
      StreamReader file = new StreamReader("usuarios.txt");
      FileHandler fileAcess = new FileHandler(file);

      fileAcess.readFile();
      fileAcess.generateAllReport();


      // Opcionais
      fileAcess.generateSortedReport();
      
      Console.Write("Defina quantos usuários consultar: ");
      int n = int.Parse(Console.ReadLine());
      fileAcess.generateNFirstReport(n);
      fileAcess.generateHTML();


      file.Close();

    } catch(Exception e) {
        Console.WriteLine("\nNão foi possível abrir o arquivo.");
        Console.WriteLine("Exception: " + e.Message);
    } finally {
        Console.WriteLine("\nExecução finalizada\nRelatório gerado!\n");
    }

  }
}

// Classe para manipulação do arquivo
class FileHandler{
    private StreamReader file;
    private List<data> userData;
    private double totalMB, meanMB;

  public FileHandler(StreamReader file){
    this.file = file;
  }

  // Método para leitura do arquivo
  public void readFile(){
    char[] delim = new char[] {' '};
    string line = file.ReadLine();
    userData = new List<data>();

    // Percorre o arquivo e salva os dados de usuários em memória
    while(line != null){
      string strBytes = "";
      string[] aux = line.Split(delim);
      
      foreach(var s in aux){
        if(s != ' '.ToString() && s != aux[0].ToString())
          strBytes += s;
      }

      // Armazena os dados na lista "userData"
      double nBytes = double.Parse(strBytes);
      userData.Add(new data(aux[0], nBytes));

      line = file.ReadLine();
    }

    // Calcula e armazena a quantidade de MB utilizados
    for(int i = 0; i < userData.Count; i++){
        double MB = bytesToMB(userData[i].getBytesUsage());
        userData[i].setMBUsage(MB);
    }

    // Calcula e armazena a porcentagem de uso
    for(int i = 0; i < userData.Count; i++){
      double MB = userData[i].getMBUsage();
      userData[i].setPercentage(usagePercentage(MB));
    }

  }

  // Método para conversão dos bytes em MB
  public double bytesToMB(double nBytes){
      double KB = nBytes/1024;
      double MB = KB/1024;

      return MB;
  }

  // Método para calcular a porcentagem de uso 
  public float usagePercentage(double userMB){
      totalSpace();
      meanSpace();

      return (float)((userMB * 100) / totalMB);      
  }

  // Método para calcular o espaço total ocupado
  public void totalSpace(){
    totalMB = 0;

    for(int i = 0; i < userData.Count; i++)
        totalMB += userData[i].getMBUsage();
  }

  // Método para calcular o espaço médio ocupado
  public void meanSpace(){

    meanMB = (totalMB / userData.Count);

  }

  // Método para extrair relatórios (relatorio.txt)
  public void generateAllReport(){
    using StreamWriter file = new("relatorio.txt");

    string header = "ACME Inc.      Uso do espaço em disco pelos usuários\n------------------------------------------------------------------\nNr.   Usuário     Espaço Utilizado    % do uso\n\n";

    file.WriteLine(header);

    string allData = "";
    for(int i = 0; i < userData.Count; i++){
      allData = $"{i + 1}\t\t\t{userData[i].getUserName()}\t\t\t\t{userData[i].getMBUsage().ToString("0.00")} MB\t\t\t\t\t{userData[i].getPercentage().ToString("0.00")}%";
      file.WriteLine(allData);
    }

    string footer = $"\nEspaço total ocupado: {totalMB.ToString("0.00")} MB\nEspaço médio ocupado: {meanMB.ToString("0.00")} MB\n";

    file.WriteLine(footer);
    
  }

  // Método para extrair relatórios ordenados (opcional)
  public void generateSortedReport(){
    using StreamWriter file = new("relatorioOrdenado.txt");

    List<data> sortedData = userData.OrderBy(x => x.getPercentage()).ToList();

    string header = "ACME Inc.      Uso do espaço em disco pelos usuários\n------------------------------------------------------------------\nNr.   Usuário     Espaço Utilizado    % do uso\n\n";

    file.WriteLine(header);

    string allData = "";
    for(int i = 0; i < userData.Count; i++){
      allData = $"{i + 1}\t\t\t{sortedData[i].getUserName()}\t\t\t\t{sortedData[i].getMBUsage().ToString("0.00")} MB\t\t\t\t\t{sortedData[i].getPercentage().ToString("0.00")}%";
      file.WriteLine(allData);
    }

    string footer = $"\nEspaço total ocupado: {totalMB.ToString("0.00")} MB\nEspaço médio ocupado: {meanMB.ToString("0.00")} MB\n";

    file.WriteLine(footer);
    
  }

  // Método para extrair relatórios com n primeiros dados (opcional)
  public void generateNFirstReport(int nReports){
    using StreamWriter file = new("relatorioNPrimeiros.txt");

    string header = "ACME Inc.      Uso do espaço em disco pelos usuários\n------------------------------------------------------------------\nNr.   Usuário     Espaço Utilizado    % do uso\n\n";

    file.WriteLine(header);

    string allData = "";
    for(int i = 0; i < nReports; i++){
      allData = $"{i + 1}\t\t\t{userData[i].getUserName()}\t\t\t\t{userData[i].getMBUsage().ToString("0.00")} MB\t\t\t\t\t{userData[i].getPercentage().ToString("0.00")}%";
      file.WriteLine(allData);
    }

    string footer = $"\nEspaço total ocupado: {totalMB.ToString("0.00")} MB\nEspaço médio ocupado: {meanMB.ToString("0.00")} MB\n";

    file.WriteLine(footer);
    
  }

  // Método para gerar a saída em um arquivo HTML (opcional)
  public void generateHTML(){
    using StreamWriter file = new("relatorioHTML.HTML");

    string header = ("<!DOCTYPE html>\n<html>\n<title>RELATORIO HTML</title>\n<head>\nACME Inc.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Uso do espaço em disco pelos usuários<br>--------------------------------------------------------------------------Nr.&nbsp;&nbsp;   Usuário&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Espaço Utilizado&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;% do uso \n<style type=text/css>\n</style>\n</head>\n<body>\n<p>");

    file.WriteLine(header);

    string allData = "";
    for(int i = 0; i < userData.Count; i++){
      allData = $"{i + 1}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{userData[i].getUserName()}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{userData[i].getMBUsage().ToString("0.00")} MB &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{userData[i].getPercentage().ToString("0.00")}%<br>\n";
      file.WriteLine(allData);
    }

    string footer = $"</p>\n</body>\n<footer>\n<p>Espaço total ocupado: {totalMB.ToString("0.00")} MB<br>Espaço médio ocupado: {meanMB.ToString("0.00")} MB</p>\n</footer>\n</html>";

    file.WriteLine(footer);
    
  }

}

// Classe para estruturamento dos dados
class data{
  private string userName;
  private double bytesUsage;
  private double MBUsage;
  private float percentage;

  public data(string userName, double bytesUsage){
    this.userName = userName;
    this.bytesUsage = bytesUsage;
  }

  public string getUserName(){
    return userName;
  }

  public double getBytesUsage(){
    return bytesUsage;
  }

  public double getMBUsage(){
    return MBUsage;
  }

  public float getPercentage(){
    return percentage;
  }

  public void setPercentage(float percentage){
    this.percentage = percentage;
  }

  public void setMBUsage(double MB){
    this.MBUsage = MB;
  }

}