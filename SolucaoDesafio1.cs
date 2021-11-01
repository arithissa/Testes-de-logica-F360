// Solução por Aríthissa Vitória

using System;
using System.Text;

class Program {
  public static void Main (string[] args) {
    string[] payment = new string[] {"DINHEIRO.", ".A VISTA,", "PARCELADO-", "DBT%", "CRÉDITO A VISTA"};

    charRemoval(payment);

  }

  // Função para remover caracteres especiais
  public static void charRemoval(string[] payment){
    string[] delim = new string[] {"!", "@", "#", "$", "%", "¨", "&", "*", "(', ')", "-", "_", "=", "+", "§", "[", "]", ",", ".", ";", ":", "|", "/", "*", "¬", "^", "~" };

    for(int i = 0; i < payment.Length; i++){
      for(int j = 0; j < delim.Length; j++){
        payment[i] = string.Join("", payment[i].Split(delim[j]));
      }
      print(payment[i]);
    }

  }

  // Função para impressão do método de pagamento
  public static void print(string payment){

    Console.WriteLine(payment);

  }

}