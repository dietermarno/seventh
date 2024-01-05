# Seventh
Teste prático de desenvolvimento

Demonstração servidor para video monitoramento API RestFul com armazenamento de dados em banco de dados relacional Microsoft MS-SQL e de vídeos em sistema de arquivos.

## Instalação

Para executar é necessário ambiente com Docker Engine versão 24.0.2 or superior em execução.
Para iniciar basta executar docker compose a partir de um prompt PowerShell utilizando o seguinte comando na pasta raiz do projeto: 

```bash
docker compose up -d
```

## Utilização

No mesmo ambiente, inicie seu navegador internet e digite o seguinte endereço:

```bash
http://localhost:4200/
```

## Lista de pendências

▪ xxx. \
▪ xxx.

## Execução em ambiente de desenvolvimento

Em um esquipamento com Visual Studio 2022 instalado e Docker Engine versão 24.0.2 or superior em execução: \
▪ Clone este repositório. \
▪ Abra a solução seventhcorewebapi.sln. \
▪ Para executar o SQL Server, a partir de uma janela CMD ou PowerShell execute:

```bash
docker pull dietermarno/seventhmssql:data
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=R353t3282@" -p 1433:1433 -d dietermarno/seventhmssql:data
```

▪ Execute a aplicação seventhcorewebapi utilizando o perfil "Docker". \
