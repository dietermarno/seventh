# Seventh
Teste prático de desenvolvimento

Demonstração servidor para video monitoramento API RestFul com armazenamento de dados em banco de dados relacional (Microsoft SQL Server) e arquivos binários de vídeos em sistema de arquivos local.

## Instalação

▪ Para executar é necessário ambiente com Docker Engine versão 24.0.2 or superior em execução em equipamento com Windows 10. \
▪ Realize a clonagem deste repositório para uma pasta em seu equipamento local. \
▪ Abra o arquivo docker-compose.yml, na raiz do projeto no editor de código de sua preferência. \
▪ Localize o grupo "web_api" e dentro dele outro chamado "volumes". \
▪ Na linha seguinte você irá encontrar dois caminhos utilizados na montagem de volume para armazenamento dos videos. O primeiro caminho (C:/temp/seventh), se refere ao sistema de arquivos local. O segundo caminho ao sistema de arquivos interno do container docker (/data). O segundo caminho nao deve ser alterado. O priemiro deve refletir uma pasta existente no sistema de arquivos local. Se não quiser alterar docker-compose.yml, você pode criar o caminho C:/temp/seventh em seu sistema de arquivos local. \
▪ Para iniciar a aplicação execute o comando docker compose a partir de um prompt PowerShell utilizando o seguinte comando na pasta raiz do projeto: 

```bash
docker compose up -d
```

## Utilização

No ambiente local, inicie seu navegador internet e digite o seguinte endereço:

```bash
https://localhost:5001/swagger/
```

## Lista de pendências

▪ Criação de novo container docker para hospedar serviço de mensageria (rabbitmq). \
▪ Elaboração da rota recycler/process/{days}. \
▪ Elaboração da rota recycler/status.

## Execução em ambiente de desenvolvimento

Em um esquipamento com Visual Studio 2022 instalado e Docker Engine versão 24.0.2 or superior em execução: \
▪ Clone este repositório. \
▪ Abra a solução seventhcorewebapi.sln. \
▪ Para executar o container do SQL Server, a partir de uma janela CMD ou PowerShell execute:

```bash
docker pull dietermarno/seventhmssql:data
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=R353t3282@" -p 1433:1433 -d dietermarno/seventhmssql:data
```

▪ Execute a aplicação seventhcorewebapi utilizando o perfil "Docker". \
