// Solução por Aríthissa Vitória

using System;
using System.Collections;

// Classe principal
class Program {
  static int[] numbersArray;

  public static void Main (string[] args) {

  readInput();

  Calculations calc = new Calculations(numbersArray);
  calc.meanCalc();
  calc.medianCalc();
  calc.printResults();
    
  }

  // Lê os valores de entrada
  static void readInput(){
    Console.Write("Digite a quantidade de números para o cálculo: ");
    int n = int.Parse(Console.ReadLine());
    
    numbersArray = new int[n];
    char[] charArray = new char[n+1];

    Console.Write("Digite os números para o cálculo: ");

    for(int i = 0; i < n; i++)
      numbersArray[i] = int.Parse(Console.ReadLine());
    
  }

}

// Classe para o cálculo
class Calculations{
  private float meanResult = 0, medianResult;
  private int[] numbersArray;

  public Calculations(int[] numbersArray){
    this.numbersArray = new int[numbersArray.Length];
    this.numbersArray = numbersArray;
  }

  // Cálculo da média
  public void meanCalc(){

    for(int i = 0; i < numbersArray.Length; i++)
      meanResult += numbersArray[i];
    
    meanResult = (meanResult / numbersArray.Length);

  }

  // Cálculo da mediana
  public void medianCalc(){
    int[] aux = new int[numbersArray.Length];
    aux = numbersArray;

    InsertionSort ord = new InsertionSort();
    aux = ord.sortArray(aux);

    if(aux.Length % 2 == 0){
      medianResult = ((aux[aux.Length/2] + aux[(aux.Length/2) + 1]) / 2);
    } else{
      medianResult = aux[aux.Length/2];
    }

  }

  // Impressão dos resultados
  public void printResults(){
    Console.WriteLine($"Média = {meanResult}");
    Console.WriteLine($"Mediana = {medianResult}");
  }

}

// Classe para ordenação do array
class InsertionSort {

  public InsertionSort(){}
 
  public int[] sortArray(int[] values){
    int n = values.Length;

    for(int i = 1; i < n; i++) {
      int key = values[i];
      int j = i - 1;
 
      while(j >= 0 && values[j] > key) {
        values[j + 1] = values[j];
        j -= 1;
      }
      values[j + 1] = key;
    }

    return values;
  }
}