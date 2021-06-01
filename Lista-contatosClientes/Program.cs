using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Lista_contatosClientes
{
    class Program
    {
        [System.Serializable]
        struct cliente
        {
          public  string nome;
          public   string email;
            public string cpf;
        }
        static List<cliente> clientes = new List<cliente>();
        enum Menu { Listagem = 1, Adicionar = 2 ,Remover = 3, Sair = 4 }
        static void Main(string[] args)
        {
            Carregar();
            bool EscolheuSair = false;
            while (!EscolheuSair)
            {
                Console.WriteLine("Sistemas de clientes - Bem vindo"!);
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
                int intO = int.Parse(Console.ReadLine());
                Menu opçao = (Menu)intO;
                switch (opçao)
                {
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        EscolheuSair = true;
                        break;
                }
                Console.Clear();
            }

        }
        static void Adicionar()
        {
            cliente Cliente = new cliente();
            Console.WriteLine("Cadrastro de Clientes"!);
            Console.WriteLine("Nome do Cliente"!);
            Cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do Cliente"!);
            Cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do Cliente"!);
            Cliente.cpf = Console.ReadLine();

            clientes.Add(Cliente);
            Salvar();

            Console.WriteLine("Cadrastro concluido !!Aperte enter para sair"!);
            Console.ReadLine();
        }
        static void Listagem ()
        {
            if (clientes.Count > 0)
            {

                Console.WriteLine("Lista de clientes"!);
                int i = 0;
                foreach (cliente cliente in clientes)
                {
                    Console.WriteLine($" ID: {i}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"Email:{cliente.email}");
                    Console.WriteLine($"CPF:{cliente.cpf}");
                    Console.WriteLine("==========================");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Nenhum Cliente cadrastrado"!);
            }
            Console.WriteLine("Aperte enter para sair");
            Console.ReadLine();
        }
        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite o ID do cliente que você quer remover"!);
            int id = int.Parse(Console.ReadLine());
            if(id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();
            }
            else
            {
                Console.WriteLine("Id digitado é invalido! Tente novamente!");
                Console.ReadLine();
            }
        }
        static void Salvar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter();

            enconder.Serialize(stream, clientes);
            stream.Close();
          
        }
        static void Carregar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            try
            {
                
                BinaryFormatter enconder = new BinaryFormatter();

                clientes = (List<cliente>)enconder.Deserialize(stream);

                if (clientes == null)
                {
                    clientes = new List<cliente>();
                }

                
               
            }
            catch(Exception e)
            {
                clientes = new List<cliente>();
            }
            stream.Close();
        }
    }
}
