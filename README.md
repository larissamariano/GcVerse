# GcVerse (New Universe) Project

GcVerse é um projeto desenvolvido por mim como entrega final de um dos módulos do curso de pós graduação. Esse projeto tem como objetivo se tornar um lugar seguro e divertido na internet onde os jovens possam encontrar notícias fresquinhas, quizzes divertidos, listas e recomendações de conteúdo dos mais variados assuntos. A ideia é a construção de uma nova rede social que concentre entretenimento, informação com foco na cultura geek e asiatica que está em alta atualmente, principalemente entre a geração mais nova.

## Contribute

[Larissa Caroline de Andrade Mariano](https://github.com/larissamariano)

## Repositories
* SQL [link](./sqldump)
* API [link](./api)
* Admin [link](./admin)


## Desenvolvimento 

Para rodar esse projeto você precisa seguir os passos abaixo:

* Rode o script dentro da pasta SQL para criar o banco de dados e inserir os dados necessários.
* Rode o projeto da API, as configurações do localhost estão disponíveis no [lauchSettings.json (Profile: GcVerse.Application))](GcVerse/GcVerse.Application/Properties/launchSettings.json)

* Run de ADM project

## Banco de Dados

Esse projeto está usando SQL Server.
Se seu ambiente estiver OK, você precisa rodar esse script [link](./sqldump/dump.sql).

## API

Essa API foi desenvolvida em .NET 6 e apresenta uma simples arquitetura de camadas. Para rodar esse projeto use VS Code ou VS 2022. [link](./api)

## Autenticação

O projeto da api principal apresenta as rotas de autenticação. Nesse processo foi utilizado JWT. 

Credentials:
Email: lari_carolline96@hotmail.com
Password: Amorinha@230796


## Admin

This is a core project, here you can manager your data like cut videos, audio, upload video from your desktop, create your campain...etc

Login

![Cut video](./images/login.png)

Channels
![List Channels](./images/channels.png)


Edit Content
In this page you can cut videos, download thumb from video and share your content
![Cut video](./images/cut_video.png)


Campain
![Campain](./images/campain.png)

Social Media
![Manager Social Media](./images/socialMedia.png)
