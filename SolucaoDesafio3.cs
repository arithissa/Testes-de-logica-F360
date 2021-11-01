// Solução por Aríthissa Vitória

using System;

// Classe principal
class Program {
  public static void Main (string[] args) {

    Console.Write("Digite o valor da venda: ");
    float saleValue = float.Parse(Console.ReadLine());
    Console.Write("Digite o valor da taxa: ");
    float rateValue = float.Parse(Console.ReadLine());

    rateCalc percent = new rateCalc(saleValue, rateValue);
    percent.printResult(percent.ratePercent());

  }

}

// Classe para o cálculo do percentual
class rateCalc{
  float saleValue, rateValue;

  public rateCalc(float saleValue, float rateValue){
    this.saleValue = saleValue;
    this.rateValue = rateValue;
  }

  public float ratePercent(){
    float result = ((rateValue * 100) / saleValue);

    return result;
  }

  public void printResult(float result){
    Console.WriteLine($"Percentual da taxa cobrada: {result}%");
  }

}