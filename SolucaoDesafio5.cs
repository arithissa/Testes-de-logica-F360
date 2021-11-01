// Solução por Aríthissa Vitória

using System;
using System.Linq;
using System.IO;

// Classe principal
class Program {
  static string str;

  public static void Main (string[] args) {
    
    readInput();
    StringInvert strOp = new StringInvert(str);
    strOp.invert();

  }

  // Lê a string de entrada
  static void readInput(){

    Console.Write("String a ser invertida: ");
    str = Console.ReadLine();

  }
}

// Classe para inverter a string
class StringInvert{
  string str, reverseStr;

  public StringInvert(string str){
    this.str = str;
  }

  // Função chamada pela main;
  // Inverte a string e exibe o resultado
  public void invert(){
    reverseStr = new string(str.Reverse().ToArray());
    Console.WriteLine(addSpaces());
    
  }

  // Função específica da classe para remover espaços da string
  private string removeSpaces(){
    string newStr = reverseStr.Replace(" ", "");

    return newStr;
  }

  // Função específica da classe para adicionar espaços quando um caractere for vogal
  private string addSpaces(){
    char[] vogals = new char[5] {'a', 'e', 'i', 'o', 'u'};
    string newStr = removeSpaces();
    string result = "";

    foreach(var f in newStr){
      result += f;

      for(int i = 0; i < vogals.Length; i++){
        if(vogals[i].Equals(f))
          result += " ";
      } 
    }

    return result;
  }
}