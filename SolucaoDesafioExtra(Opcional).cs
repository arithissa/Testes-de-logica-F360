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
    string[] file = Directory.GetFiles(@"C:\Usuários");
    FileHandler fileAcess = new FileHandler(file);

    fileAcess.readFile();
    fileAcess.generateInitialFile();

    } catch(Exception e) {
      Console.WriteLine("\nNão foi possível abrir o arquivo.");
      Console.WriteLine("Exception: " + e.Message);
    } finally {
      Console.WriteLine("\nExecução finalizada.");
    }

  }
}

// Classe para manipulação do arquivo
class FileHandler{
    private string[] file;
    private List<data> userData;
    private double totalMB, meanMB;

  public FileHandler(string[] file){
    this.file = file;
  }

  // Método para leitura do arquivo
  public void readFile(){
    char[] delim = new char[] {' '};
    userData = new List<data>();

    // Percorre o arquivo e salva os dados de usuários em memória
    foreach(string f in file){
      string strBytes = "";
      string[] aux = f.Split(delim);
      
      foreach(var s in aux){
        if(s != ' '.ToString() && s != aux[0].ToString())
          strBytes += s;
      }

      // Armazena os dados na lista "userData"
      double nBytes = double.Parse(strBytes);
      userData.Add(new data(aux[0], nBytes));

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
  public void generateInitialFile(){
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