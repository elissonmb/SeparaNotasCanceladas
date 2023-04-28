using System.Collections;
using System.Xml;

internal class Program
{
    private static void Main(string[] args)
    {
        ArrayList chaveEvento = new();
        ArrayList chaveNFe = new();
        ArrayList notasARetirar = new();
        int aux = 0;
        int aux1 = 0;
        //DEFINE CAMINHO DE ENTRADA
        string path = @"D:\Arquivos_modulos\Migracao\TESTE";
        //DEFINE CAMINHO DE SAIDA
        string path2 = @"D:\Arquivos_modulos\Migracao\TESTE\CANCELADAS\";

        string[] files = Directory.GetFiles(path, "*.xml", SearchOption.TopDirectoryOnly);

        //FLAG PARA VERIFICAR SE EXECUTOU AO MENOS 1 VEZ
        Boolean moveuNota = false;

        //COLETA EVENTO DE CANCELAMENTO
        foreach (string file in files)
        {
            FileInfo fileInfo = new FileInfo(file);
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            string xml = File.ReadAllText(file);
            XmlNodeList xnList = doc.GetElementsByTagName("retEvento");
            XmlNodeList xnList1 = doc.GetElementsByTagName("chNFe");
            //GARANTIR QUE LISTA ESTÁ PREENCHIDA COM O ARQUIVO CORRETO
            if (xnList.Count > 0)
            {
                chaveEvento.Add(xnList1[0].InnerXml.ToString());
            }
        }
        if (chaveEvento.Count == 0)
        {
            Console.WriteLine("Não existem notas canceladas");
            Console.ReadLine();
            //ENCERRAR FLUXO JÁ QUE NÃO ACHOU NOTAS CANCELADAS
            goto NotFound;
        }
        else
            //COLETA CHAVE DE ACESSO NFE
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                string xml = File.ReadAllText(file);

                XmlNodeList xList = doc.GetElementsByTagName("protNFe");
                XmlNodeList xList1 = doc.GetElementsByTagName("chNFe");
                //GARANTIR QUE LISTA ESTÁ PREENCHIDA COM O ARQUIVO CORRETO
                if (xList.Count > 0)
                {
                    chaveNFe.Add(xList1[0].InnerXml.ToString());
                    for (int i = 0; i < chaveEvento.Count; i++)
                    {
                        //VERIFICA SE O ARQUIVO ABERTO É IGUAL A ALGUMA CHAVE DE ACESSO DO EVENTO DO ARRAY CHAVEEVENTO
                        if (aux < chaveEvento.Count && chaveEvento[aux].Equals(chaveNFe[aux1]))
                        {
                            fileInfo.MoveTo(path2 + fileInfo.Name);
                            aux = 0;
                            moveuNota = true;
                            break;
                        }
                        else aux++;
                    }
                    aux = 0;
                    aux1++;
                }
            }
        NotFound:
        if (moveuNota == false && chaveEvento.Count > 0)
        {
            Console.WriteLine("Não há mais notas para serem movidas!");
            Console.ReadLine();
        }else
        {
            Console.WriteLine("Todas as notas foram movidas!");
            Console.ReadLine();
        }
    }

}


